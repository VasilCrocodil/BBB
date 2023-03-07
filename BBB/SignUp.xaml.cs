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
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC11\LOCALHOST; Initial Catalog =library_project; Integrated Security = True");

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                string query = "Insert into Students (StudentID,[Sname], [class_], [password_]) values('"+ this.txtStudentID.Text + "','" + this.txtFirstName.Text + "','" + this.txtClass.Text +
                    "','" + this.txtPassword.Password + "')";

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.ExecuteNonQuery();


                string query1 = "Select BookID, BookName, author, pubdate, publisher,genre from Books";
                SqlCommand cmd1 = new SqlCommand(query1, sqlCon);
                cmd1.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
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

