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
    public partial class Categorys : Form
    {
        public Categorys()
        {
            InitializeComponent();
            ShowCategory();
        }

        //Establish the connection
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\OLEBOGENG\Documents\propertyRental.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowCategory()
        {

            //open the connection to acess the data base
            conn.Open();
            string query = "Select * from CategoryDb";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CategoryDV.DataSource = ds.Tables[0];

            //close the connection
            conn.Close();
        }
        private void resetData()
        {
            //initialize the components
            NameBox.Text = "";
            Remark.Text = "";
            
        }

        private void AddBt_Click(object sender, EventArgs e)
        {
            if (NameBox.Text == "" || Remark.Text == "" )
            {
                MessageBox.Show("Missing information!!");

            }
            else
            {

                try
                {
                    conn.Open();

                    //create sql statement that will communicate with the data base
                    SqlCommand cmd = new SqlCommand("insert into CategoryDb(Category,Remarks)values(@CT,@RM)", conn);

                    //pass the text in TentName to value in cmd variable named @TN
                    cmd.Parameters.AddWithValue("@CT", NameBox.Text);
                    cmd.Parameters.AddWithValue("@RM", Remark.Text);

                    // execute the Query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Remark  Added! Thank you");
                    conn.Close();

                    //reset the text boxes to null value
                    resetData();

                    //show added data 
                    ShowCategory();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }
            }
        }
    }
}
