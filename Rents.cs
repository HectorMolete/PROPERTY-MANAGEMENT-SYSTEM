using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Property_rental
{
    public partial class Rents : Form
    {
        public Rents()
        {
            InitializeComponent();
            GetTenant();
            GetApartment();
            ShowRent();
        }
        //Establish the connection
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\OLEBOGENG\Documents\propertyRental.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetCos()
        {
            conn.Open();
            string query = "select * from ApartmentDb where ApartmentId =" + AprtBox.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

           foreach (DataRow dr in dt.Rows)
            {
                AmntBox.Text = dr["ApartmentCost"].ToString();

            }
            conn.Close();

        }
        private void GetTenant()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select TenantId from TenantsDb", conn);
            SqlDataReader sqrd;
            sqrd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TenantId", typeof(int));
            dt.Load(sqrd);
            TenatBox.ValueMember = "TenantId";
            TenatBox.DataSource = dt;
            conn.Close();

        }

        private void GetApartment()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select ApartmentId from ApartmentDb", conn);
            SqlDataReader sqrd;
            sqrd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ApartmentId", typeof(int));
            dt.Load(sqrd);
            AprtBox.ValueMember = "ApartmentId";
            AprtBox.DataSource = dt;
            conn.Close();

        }

        private void ShowRent()
        {
            //open the connection to acess the data base
            conn.Open();
            string query = "Select * from RentDb";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RentDataGrid.DataSource = ds.Tables[0];

            //close the connection
            conn.Close();
        }
        private void resetData()
        {
            //initialize the components
            AprtBox.SelectedItem = -1;
            AmntBox.Text = "";
            TenatBox.SelectedItem = -1;
            Rentdate.Value = DateTime.Today.AddDays(-1);
        }



        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (AmntBox.Text == "" || TenatBox.SelectedIndex == -1 || AprtBox.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information!!");

            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("insert into RentDb(Apartment,Tenant,Period,amount)values(@Apt,@Tnt,@Prt,@amnt)", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@Apt", AprtBox.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Tnt", TenatBox.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Prt", Rentdate.Text);
                    cmd.Parameters.AddWithValue("@amnt", AmntBox.Text);

                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Added! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowRent();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }
        int Key = 0;
        private void TenantGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //insert data in the grid view box
            int columnIndex = 0;
            int rowindex = RentDataGrid.CurrentCell.RowIndex;
            MessageBox.Show(rowindex.ToString());

            SelectedBox.Text = RentDataGrid.Rows[rowindex].Cells[columnIndex].Value.ToString();

            if (SelectedBox.Text == "")
            {

                Key = 0;

            }
            else
            {
                Key = Convert.ToInt32(SelectedBox.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Landlord");
            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("delete from RentDb where RentCode = @TKey", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@TKey", Key);


                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Deleted! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowRent(); 


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void UpdateBt_Click(object sender, EventArgs e)
        {

            if (AmntBox.Text == "" || TenatBox.SelectedIndex == -1 || AprtBox.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information!!");


            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("update TenantsDb set Apartment = @Aprt,Tenant = @Tnt,Period = @P,amount = @Amnt whereRentCode = @key", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@Aprt", AprtBox.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Tnt", TenatBox.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@P", Rentdate.Text);
                    cmd.Parameters.AddWithValue("@Amnt", AmntBox.Text);
                    cmd.Parameters.AddWithValue("@key", Key);



                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rent table Updated! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowRent();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void AprtBox_SelectionChangeCommitted(object sender, EventArgs e)
        {

            GetCos();
        }
    }
}



