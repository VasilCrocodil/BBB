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
    /// Interaction logic for LogIn_Lib.xaml
    /// </summary>
    public partial class LogIn_Lib : Window
    {
        public LogIn_Lib()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC11\LOCALHOST; Initial Catalog =library_project; Integrated Security = True ");


            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT COUNT(1) FROM Librarians Where LibID=@LibID and password_=@Password";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@LibID", txtLibID.Text);
                sqlCommand.Parameters.AddWithValue("@Password", txtPassword.Password);

                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.label.Content = $"Welcome";
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password are not correct!");
                }
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