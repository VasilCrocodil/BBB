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
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password are not correct!");
                }

                string query1 = "Select BookID, BookName, author, pubdate, publisher,genre from Books";
                SqlCommand cmd = new SqlCommand(query1, sqlCon);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                Librarian_Access a = new Librarian_Access();


                a.DataGrid_.ItemsSource = dt.DefaultView;
                adapter.Update(dt);


                


                string query2 = "Select BookID, donatorID, donatorName, donationdate from BooksDonated";
                SqlCommand cmd1 = new SqlCommand(query2, sqlCon);
                cmd1.ExecuteNonQuery();
                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                adapter1.Fill(dt1);

                
                a.DataGrid_2.ItemsSource = dt1.DefaultView;
                adapter1.Update(dt1);


                sqlCon.Close();
                a.Show();
                this.Close();


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