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
    public partial class Landlords : Form
    {
        public Landlords()
        {
            InitializeComponent();
            ShowLords();
        }

        //Establish the connection
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\OLEBOGENG\Documents\propertyRental.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowLords()
        {
            //open the connection to acess the data base
            conn.Open();
            string query = "Select * from LandloardDb";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            LandGrid.DataSource = ds.Tables[0];

            //close the connection
            conn.Close();
        }
        private void resetData()
        {
            //initialize the components
            LandLName.Text = "";
            LandLPhone.Text = "";
            LandBox.SelectedItem = -1;
            LandBox.Text = "";
            
        }



        private void landlordSave_Click(object sender, EventArgs e)
        {
            if (LandLName.Text == "" || LandLPhone.Text == "" || LandBox.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information!!");

            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("insert into LandloardDb(LandlordName,LandlordPhone,LandloardGender)values(@LN,@LP,@LG)", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@LN", LandLName.Text);
                    cmd.Parameters.AddWithValue("@LP", LandLPhone.Text);
                    cmd.Parameters.AddWithValue("@LG", LandBox.SelectedItem.ToString());

                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Added! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowLords();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }
        int Key = 0;
        private void LandGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //insert data in the grid view box

            int columnIndex = 0;
            int rowindex = LandGrid.CurrentCell.RowIndex;
            MessageBox.Show(rowindex.ToString());
       
            landlordTextB.Text = LandGrid.Rows[rowindex].Cells[columnIndex].Value.ToString();

            if (landlordTextB.Text == "")
            {

                Key = 0;

            }
            else
            {
                Key = Convert.ToInt32(landlordTextB.Text);
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
                    SqlCommand cmd = new SqlCommand("delete from LandloardDb where LandloardId =@LLKey", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@LLKey", Key);


                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landloard Deleted! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowLords();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LandLName.Text == "" || LandLPhone.Text == "" || LandBox.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information!!");

            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("update LandloardDb set LandlordName = @LLN,LandlordPhone = @LLP,LandloardGender = @LLG where landloardId = @lKey", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@LLN", LandLName.Text);
                    cmd.Parameters.AddWithValue("@LLP", LandLPhone.Text);
                    cmd.Parameters.AddWithValue("@LLG", LandBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@lKey", Key);


                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landloard table Updated! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowLords();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}


