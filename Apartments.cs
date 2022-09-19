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
    public partial class Apartments : Form
    {
        public Apartments()
        {
            InitializeComponent();
            GetOwnership();
            GetCategory();
            ShowApartments();
        }

        //Establish the connection
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\OLEBOGENG\Documents\propertyRental.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetCategory()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select CategoryNum from CategoryDb", conn);
            SqlDataReader sqrd;
            sqrd =cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CategoryNum", typeof(int));
            dt.Load(sqrd);
            ApartType.ValueMember = "CategoryNum";
            ApartType.DataSource = dt;
            conn.Close();


        }
        private void GetOwnership()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select landloardId from LandloardDb", conn);
            SqlDataReader sqrd;
            sqrd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("landloardId", typeof(int));
            dt.Load(sqrd);
            OwnerCB.ValueMember = "landloardId";
            OwnerCB.DataSource = dt;
            conn.Close();
        }


        private void ShowApartments()
        {
            //open the connection to acess the data base
            conn.Open();
            string query = "Select * from ApartmentDb";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AprtDataGrid.DataSource = ds.Tables[0];

            //close the connection
            conn.Close();
        }
        private void resetData()
        {
            //initialize the components
            ApartName.Text = "";
            ApartAdress.Text = "";
            ApartType.SelectedItem = -1;
            ApaetCost.Text = "";
            OwnerCB.SelectedItem = -1;
            selectedAprt.Text = "";



        }



        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (ApartName.Text == "" || ApartAdress.Text == "" || ApartType.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information!!");

            }
            else
            {

                try
                {
                    conn.Open();
                   
                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("insert into ApartmentDb(ApartmentName,ApartmentAddress,ApartmentType,ApartmentCost,ApartmentOwner)values(@AN,@AD,@AT,@AC,@AO)", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@AN", ApartName.Text);
                    cmd.Parameters.AddWithValue("@AD", ApartAdress.Text);
                    cmd.Parameters.AddWithValue("@AT", ApartType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AC", ApaetCost.Text);
                    cmd.Parameters.AddWithValue("@AO", OwnerCB.SelectedValue.ToString());


                    MessageBox.Show("im here");
                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Added! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowApartments();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }
        int Key = 0;
        private void AprtDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //insert data in the grid view box

            int columnIndex = 0;
            int rowindex = AprtDataGrid.CurrentCell.RowIndex;
            MessageBox.Show(rowindex.ToString());

            selectedAprt.Text = AprtDataGrid.Rows[rowindex].Cells[columnIndex].Value.ToString();

            if (selectedAprt.Text == "")
            {

                Key = 0;

            }
            else
            {
                Key = Convert.ToInt32(selectedAprt.Text);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Apartment");
            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("delete from ApartmentDb where ApartmentId =@LLKey", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@LLKey", Key);


                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Deleted! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowApartments();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (ApartName.Text == "" || ApartAdress.Text == "" || ApartType.SelectedIndex == -1 )
            {
                MessageBox.Show("Missing information!!");

            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("update ApartmentDb set ApartmentName = @APN,ApartmentAddress = @APD,ApartmentType = @APT ,ApartmentCost = @APC,ApartmentOwner = @APO where ApartmentId = @LKey  ", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@APN", ApartName.Text);
                    cmd.Parameters.AddWithValue("@APD", ApartAdress.Text);
                    cmd.Parameters.AddWithValue("@APT", ApartType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@APC", ApaetCost.Text);
                    cmd.Parameters.AddWithValue("@APO", OwnerCB.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@LKey", Key);
                    
                    


                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landloard table Updated! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowApartments();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

     
    }
}

