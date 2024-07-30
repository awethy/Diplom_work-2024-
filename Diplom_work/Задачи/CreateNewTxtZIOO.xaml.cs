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
using Diplom_work.Задачи;

namespace Diplom_work
{
    /// <summary>
    /// Логика взаимодействия для CreateNewTxtZIOO.xaml
    /// </summary>
    public partial class CreateNewTxtZIOO : Window
    {
        DBcon dBcon = new DBcon();
        public int id;
        public string id_cb;
        private readonly checkUser Role;
        private int _otdelid;

        public CreateNewTxtZIOO(checkUser _Role, int otdelid)
        {
            Role = _Role;
            _otdelid = otdelid;
            InitializeComponent();
            cBI_id();
            isAdmin();
        }

        private void isAdmin()
        {
            CBI_id.IsEnabled = Role.IsAdmin;
            CBI_id.IsEnabled = Role.IsRuk;
        }

        void cBI_id()
        {
            dBcon.openCon();

            if(Role.Status != "Admin")
            {
                String query2 = $"SELECT id, Название_отдела FROM Отделы_Предприятия WHERE id = @Отдел";
                SqlCommand sqlCommand1 = new SqlCommand(query2, dBcon.getCon());
                sqlCommand1.Parameters.AddWithValue("@Отдел", _otdelid);
                SqlDataReader dr = sqlCommand1.ExecuteReader();
                while (dr.Read())
                {
                    id = dr.GetInt32(0);
                    string Name_id = dr.GetString(1);
                    CBI_id.Items.Add(Name_id);
                }
                CBI_id.SelectedIndex = 0;
            }
            else
            {
                String query3 = $"SELECT id, Название_отдела FROM Отделы_Предприятия";
                SqlCommand sqlCommand = new SqlCommand(query3, dBcon.getCon());
                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    id = dr.GetInt32(0);
                    string Name_id = dr.GetString(1);
                    CBI_id.Items.Add(Name_id);
                }
            }
            dBcon.closeCon();           
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            dBcon.openCon();
            try
            {
                string name_id = CBI_id.SelectedItem.ToString();
                string query4 = $"SELECT id FROM Отделы_Предприятия WHERE Название_отдела = @Название_от";
                SqlCommand SqlCmd3 = new SqlCommand(query4, dBcon.getCon());
                SqlCmd3.Parameters.AddWithValue("@Название_от", name_id);
                id_cb = SqlCmd3.ExecuteScalar().ToString();

                string id_cbi = CBI_id.SelectedItem.ToString();

                var Zadacha = Задачи.Text;

                String query = $"INSERT INTO Задачи_отделов(id_отдела, Задача, Статус_задачи) VALUES (@id_cb, @Задача, 'Свободно')";
                SqlCommand SqlCmd = new SqlCommand(query, dBcon.getCon());
                SqlCmd.Parameters.AddWithValue("@id_cb", id_cb);
                SqlCmd.Parameters.AddWithValue("@Задача", Zadacha);
                SqlCmd.ExecuteNonQuery();

                MessageBox.Show("Задача добавлена, для получения актуальной информации нажмите кнопку обновить!");

                this.Close();
            }
            catch
            {
                MessageBox.Show("Выберите номер отдела");
            }
            dBcon.closeCon();
        }
    }
}
