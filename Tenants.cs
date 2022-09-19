using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Property_rental
{
    public partial class Tenants : Form
    {
        public Tenants()
        {
            InitializeComponent();
            ShowTenants();



        }

        //Establish the connection
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\OLEBOGENG\Documents\propertyRental.mdf;Integrated Security=True;Connect Timeout=30");

        
        private void ShowTenants()
        {
            //open the connection to acess the data base
            conn.Open();
            string query = "Select * from TenantsDb";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TenantGrid.DataSource = ds.Tables[0];

            //close the connection
            conn.Close();
        }
        private void resetData()
        {
            //initialize the components
            TentName.Text = "";
            TentPhone.Text = "";
            TentGenBox.SelectedItem = -1;
            TentGenBox.Text = "";
        }



        private void TenentSaveBt_Click(object sender, EventArgs e)
        {
            if (TentName.Text == "" || TentPhone.Text == "" || TentGenBox.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information!!");

            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("insert into TenantsDb(TenantName,TenantPhone,TenantGender)values(@TN,@TP,@TG)", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@TN", TentName.Text);
                    cmd.Parameters.AddWithValue("@TP", TentPhone.Text);
                    cmd.Parameters.AddWithValue("@TG", TentGenBox.SelectedItem.ToString());

                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Added! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowTenants();

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
            int rowindex = TenantGrid.CurrentCell.RowIndex;
            MessageBox.Show(rowindex.ToString());

            TenantTeXt.Text = TenantGrid.Rows[rowindex].Cells[columnIndex].Value.ToString();

            if (TenantTeXt.Text == "")
            {

                Key = 0;

            }
            else
            {
                Key = Convert.ToInt32(TenantTeXt.Text);
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
                    SqlCommand cmd = new SqlCommand("delete from TenantsDb where TenantId =@TKey", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@TKey", Key);


                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Deleted! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowTenants();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void UpdateBt_Click(object sender, EventArgs e)
        {

            if (TentName.Text == "" || TentPhone.Text == "" || TentGenBox.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information!!");
                

            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("update TenantsDb set TenantName = @TNN,TenantPhone = @TPP,TenantGender = @TGG where TenantId = @key", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@TNN", TentName.Text);
                    cmd.Parameters.AddWithValue("@TPP", TentPhone.Text);
                    cmd.Parameters.AddWithValue("@TGG", TentGenBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@key", Key);



                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant table Updated! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowTenants();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
