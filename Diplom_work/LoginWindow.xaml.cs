using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Diplom_work
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        DBcon dBcon = new DBcon();
        public int _otdelid;
        public int _id_sot;

        public LoginWindow()
        {
            InitializeComponent();
        }
        
        public void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var loginUser = txtLogin.Text;  
            var passUser = txtPassword.Password;

            dBcon.openCon();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            String query = $"SELECT id, id_отдела, Login, Password, Admin, Ruk, Sot FROM Сотрудники_отделов WHERE Login = '{loginUser}' AND Password = '{passUser}'";

            SqlCommand SqlCmd = new SqlCommand(query, dBcon.getCon());

            adapter.SelectCommand = SqlCmd;
            adapter.Fill(table);

            dBcon.closeCon();

            if (table.Rows.Count == 1)
            {
                _otdelid = Convert.ToInt32(table.Rows[0].ItemArray[1]);

                //var isAdmin = Convert.ToString(table.Rows[0].ItemArray[4]);
                var Role = new checkUser(Convert.ToBoolean(table.Rows[0].ItemArray[4]), Convert.ToBoolean(table.Rows[0].ItemArray[5]), Convert.ToBoolean(table.Rows[0].ItemArray[6]));
                _id_sot = Convert.ToInt32(table.Rows[0].ItemArray[0]);

                    MainWindowMenu mainWindowMenu = new MainWindowMenu(Role, _otdelid, _id_sot);
                    mainWindowMenu.Show();
                    this.Close();
            }
            else
            {
                MessageBox.Show("Логин или пароль введены неверно.");
            }
        }
    }
}
