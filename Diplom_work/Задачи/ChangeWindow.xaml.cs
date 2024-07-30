using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        DBcon dBcon = new DBcon();

        public int id;
        public string id_cb;
        private readonly checkUser Role;
        int _Id_zadachi;
        int _Id_otdela;
        string _Status_zadachi;
        string _Zadacha;
        string _Sot;

        public ChangeWindow(checkUser _Role, int Id_zadachi, int Id_otdela, string Status_zadachi, string Zadacha, string Sot)
        {
            _Sot = Sot;
            _Id_zadachi = Id_zadachi;
            _Id_otdela = Id_otdela;
            _Status_zadachi = Status_zadachi;
            _Zadacha = Zadacha;
            Role = _Role;
            InitializeComponent();
            BindChange();
            isAdmin();
        }

        private void isAdmin()
        {
            CBI_id.IsEnabled = Role.IsAdmin;
            Задачи.IsEnabled = Role.IsAdmin;
            CBI_id.IsEnabled = Role.IsRuk;
            Задачи.IsEnabled = Role.IsRuk;
        }

        public void query_status()
        {
            dBcon.openCon();
            string query = $"SELECT Статус FROM Статус_готовности";
            SqlCommand sqlCommand = new SqlCommand(query, dBcon.getCon());
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                string Name = dr.GetString(0);
                CBI_status.Items.Add(Name);
            }
            dBcon.closeCon();
        }

        public void BindChange()
        {
            Задачи.Text = _Zadacha;

            dBcon.openCon();
            if (Role.Status != "Admin")
            {
                string query1 = $"SELECT id, Название_отдела FROM Отделы_Предприятия WHERE id = @id_от";
                SqlCommand sqlCommand1 = new SqlCommand(query1, dBcon.getCon());
                sqlCommand1.Parameters.AddWithValue("@id_от", _Id_otdela);
                SqlDataReader dr1 = sqlCommand1.ExecuteReader();
                while (dr1.Read())
                {
                    string Name = dr1.GetString(1);
                    CBI_id.Items.Add(Name);
                }
                CBI_id.SelectedIndex = 0;
                dBcon.closeCon();
            }
            else
            {
                string query2 = $"SELECT id, Название_отдела FROM Отделы_Предприятия";
                SqlCommand sqlCommand2 = new SqlCommand(query2, dBcon.getCon());
                SqlDataReader dr2 = sqlCommand2.ExecuteReader();
                while (dr2.Read())
                {
                    string Name = dr2.GetString(1);
                    CBI_id.Items.Add(Name);
                }
                CBI_id.SelectedIndex = 0;
                dBcon.closeCon();
            }

            query_status();
            CBI_status.SelectedValue = _Status_zadachi;

            dBcon.openCon();
            string query3 = $"SELECT id, ФИО FROM Сотрудники_отделов";
            SqlCommand sqlCommand3 = new SqlCommand(query3, dBcon.getCon());
            SqlDataReader dr3 = sqlCommand3.ExecuteReader();
            while(dr3.Read())
            {
                string Name_sot = dr3.GetString(1);
                string Id_sot = Convert.ToString(dr3.GetInt32(0));
                CBI_sot.Items.Add(Name_sot);
            }
            dBcon.closeCon();

            dBcon.openCon();
            string query4 = $"SELECT ФИО FROM Сотрудники_отделов WHERE id = @Сотрудник";
            SqlCommand sqlCommand4 = new SqlCommand(query4, dBcon.getCon());
            sqlCommand4.Parameters.AddWithValue("@Сотрудник", _Sot);
            string name_sot = sqlCommand4.ExecuteScalar().ToString();
            CBI_sot.SelectedValue = name_sot;
            dBcon.closeCon();
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            dBcon.openCon();
            try
            {
                string name_sot = CBI_sot.SelectedItem.ToString();
                string query4 = $"SELECT id FROM Сотрудники_отделов WHERE ФИО = @Имя_сот";
                SqlCommand sqlCommand3 = new SqlCommand(query4, dBcon.getCon());
                sqlCommand3.Parameters.AddWithValue("@Имя_сот", name_sot);
                string id_sot = sqlCommand3.ExecuteScalar().ToString();

                String query = $"UPDATE Задачи_отделов SET Задача = @Задача, Статус_задачи = @Статус, Сотрудник = @Сотрудник WHERE id = @id";
                SqlCommand sqlCommand = new SqlCommand(query, dBcon.getCon());
                sqlCommand.Parameters.AddWithValue("@Задача", Задачи.Text);
                sqlCommand.Parameters.AddWithValue("@Статус", Convert.ToString(CBI_status.SelectedItem));
                sqlCommand.Parameters.AddWithValue("@Сотрудник", id_sot);
                sqlCommand.Parameters.AddWithValue("@id", _Id_zadachi);
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Задача изменена, для получения актуальной информации нажмите кнопку обновить!");

                this.Close();
            }
            catch
            {
                MessageBox.Show("Заполните все данные.");
            }
            dBcon.closeCon();
        }
    }
}
