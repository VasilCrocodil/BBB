using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace BBB
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC11\LOCALHOST; Initial Catalog =Vasil_Login; Integrated Security = True");

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                string query = "Insert into SignUp ([First Name], [Last Name], [Email], [Username], [Password]) values('" + this.txtFirstName.Text + "','" +
                  this.txtLastName.Text + "', '" + this.txtEmail.Text + "','" + this.txtUsername.Text + "','" + this.txtPassword.Password + "')";

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Saved!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }

}

