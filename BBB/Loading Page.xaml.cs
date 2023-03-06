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
    /// Interaction logic for Loading_Page.xaml
    /// </summary>
    public partial class Loading_Page : Window
    {
        public Loading_Page()
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

                string query = "SELECT COUNT(1) FROM Students Where StudentID=@StudentID and password_=@Password";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
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

                Books bk = new Books();

                bk.DataGrid_.ItemsSource = dt.DefaultView;
                adapter.Update(dt);


                sqlCon.Close();
                bk.Show();
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
