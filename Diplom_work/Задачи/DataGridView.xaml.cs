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
    public partial class DataGridView : Window
    {
        private readonly checkUser _Role;
        private int otdelid;
        private int _id_sot;
        public int Id_zadachi;
        public int Id_otdela;
        public string Status_zadachi;
        public string Zadacha;
        public string Sot;

        DBcon dBcon = new DBcon();

        public DataGridView(checkUser Role, int _otdelid, int id_sot)
        {
            _id_sot = id_sot;
            _Role = Role;
            otdelid = _otdelid;
            InitializeComponent();
            txt_User.Content = $"{_Role.Status}";
            BindDataGrid1();
            BindDataGrid2();
            BindDataGrid3();
            BindDataGrid4();
        }

        private void isAdmin()
        {
            btn_NewTxt.IsEnabled = _Role.IsRuk;
            btn_Delete.IsEnabled = _Role.IsRuk;
        }

        public void BindDataGrid1()
        {           
            dBcon.openCon();

            if (_Role.Status != "Admin")
            {
                string query3 = $"SELECT id, id_Отдела, Задача, Статус_задачи, Сотрудник FROM Задачи_отделов WHERE id_отдела = @Отдел AND Статус_задачи NOT IN ('Подтверждение', 'Успешно', 'Неуспешно', 'В процессе')";
                SqlCommand command3 = new SqlCommand(query3, dBcon.getCon());
                command3.Parameters.AddWithValue("@Отдел", otdelid);
                SqlDataAdapter da3 = new SqlDataAdapter(command3);
                DataTable dt3 = new DataTable("Задачи_отделов");
                da3.Fill(dt3);
                dataGrid1.ItemsSource = dt3.DefaultView;
                dBcon.closeCon();
            }
            else
            {
                string query3 = $"SELECT id, id_Отдела, Задача, Статус_задачи, Сотрудник FROM Задачи_отделов WHERE Статус_задачи NOT IN ('Подтверждение', 'Успешно', 'Неуспешно', 'В процессе')";
                SqlCommand command3 = new SqlCommand(query3, dBcon.getCon());
                SqlDataAdapter da3 = new SqlDataAdapter(command3);
                DataTable dt3 = new DataTable("Задачи_отделов");
                da3.Fill(dt3);
                dataGrid1.ItemsSource = dt3.DefaultView;
                dBcon.closeCon();
            }             
        }

        public void BindDataGrid2()
        {
            dBcon.openCon();
            if (_Role.Status == "Сотрудник")
            {
                string query_1 = $"SELECT id, id_Отдела, Задача, Статус_задачи, Сотрудник FROM Задачи_отделов WHERE Сотрудник = @Сотрудник AND Статус_задачи NOT IN ('Подтверждение', 'Успешно', 'Неуспешно', 'Свободно')";
                SqlCommand command_1 = new SqlCommand(query_1, dBcon.getCon());
                command_1.Parameters.AddWithValue("@Сотрудник", _id_sot);
                SqlDataAdapter da_1 = new SqlDataAdapter(command_1);
                DataTable dt_1 = new DataTable("Задачи_отделов");
                da_1.Fill(dt_1);
                dataGrid2.ItemsSource = dt_1.DefaultView;
                dBcon.closeCon();
            }
            else if (_Role.Status == "Руководитель")
            {
                string query_1 = $"SELECT id, id_Отдела, Задача, Статус_задачи, Сотрудник FROM Задачи_отделов WHERE id_отдела = @Отдел AND Статус_задачи NOT IN ('Подтверждение', 'Успешно', 'Неуспешно', 'Свободно')";
                SqlCommand command_1 = new SqlCommand(query_1, dBcon.getCon());
                command_1.Parameters.AddWithValue("@Отдел", otdelid);
                SqlDataAdapter da_1 = new SqlDataAdapter(command_1);
                DataTable dt_1 = new DataTable("Задачи_отделов");
                da_1.Fill(dt_1);
                dataGrid2.ItemsSource = dt_1.DefaultView;
                dBcon.closeCon();
            }
            else
            {
                string query_1 = $"SELECT id, id_Отдела, Задача, Статус_задачи, Сотрудник FROM Задачи_отделов WHERE Статус_задачи NOT IN ('Подтверждение', 'Успешно', 'Неуспешно', 'Свободно')";
                SqlCommand command_1 = new SqlCommand(query_1, dBcon.getCon());
                SqlDataAdapter da_1 = new SqlDataAdapter(command_1);
                DataTable dt_1 = new DataTable("Задачи_отделов");
                da_1.Fill(dt_1);
                dataGrid2.ItemsSource = dt_1.DefaultView;
                dBcon.closeCon();
            }
        }

        public void BindDataGrid3()
        {
            dBcon.openCon();
            string query_2 = $"SELECT id, id_Отдела, Задача, Статус_задачи, Сотрудник FROM Задачи_отделов WHERE Статус_задачи NOT IN ('В процессе', 'Успешно', 'Неуспешно', 'Свободно')";
            SqlCommand command_2 = new SqlCommand(query_2, dBcon.getCon());
            SqlDataAdapter da_2 = new SqlDataAdapter(command_2);
            DataTable dt_2 = new DataTable("Задачи_отделов");
            da_2.Fill(dt_2);
            dataGrid3.ItemsSource = dt_2.DefaultView;
            dBcon.closeCon();
        }

        public void BindDataGrid4()
        {
            dBcon.openCon();
            string query_3 = $"SELECT id, id_Отдела, Задача, Статус_задачи, Сотрудник FROM Задачи_отделов WHERE Статус_задачи NOT IN ('В процессе', 'Подтверждение', 'Свободно')";
            SqlCommand command_3 = new SqlCommand(query_3, dBcon.getCon());
            SqlDataAdapter da_3 = new SqlDataAdapter(command_3);
            DataTable dt_3 = new DataTable("Задачи_отделов");
            da_3.Fill(dt_3);
            dataGrid4.ItemsSource = dt_3.DefaultView;
            dBcon.closeCon();
        }

        private void Btn_NewTxt_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTxtZIOO createNewTxtZIOO = new CreateNewTxtZIOO(_Role, otdelid);
            createNewTxtZIOO.Owner = this;
            createNewTxtZIOO.Show();
        }

        private void Btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            BindDataGrid1();
            BindDataGrid2();
            BindDataGrid3();
            BindDataGrid4();
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (TC_zad.SelectedItem == TI_Free)
                {
                    DataRowView drv = (DataRowView)dataGrid1.SelectedItem;
                    String result = (drv["id"]).ToString();
                    Convert.ToInt32(result);

                    dBcon.openCon();
                    string query = $"DELETE FROM Задачи_отделов WHERE id = @id";
                    SqlCommand sqlCom = new SqlCommand(query, dBcon.getCon());
                    sqlCom.Parameters.AddWithValue("@id", result);
                    sqlCom.ExecuteNonQuery();
                    dBcon.closeCon();
                }
                else if(TC_zad.SelectedItem == TI_Zan)
                {
                    DataRowView drv = (DataRowView)dataGrid2.SelectedItem;
                    String result = (drv["id"]).ToString();
                    Convert.ToInt32(result);

                    dBcon.openCon();
                    string query = $"DELETE FROM Задачи_отделов WHERE id = @id";
                    SqlCommand sqlCom = new SqlCommand(query, dBcon.getCon());
                    sqlCom.Parameters.AddWithValue("@id", result);
                    sqlCom.ExecuteNonQuery();
                    dBcon.closeCon();
                }
                else if(TC_zad.SelectedItem == TI_ozh)
                {
                    DataRowView drv = (DataRowView)dataGrid3.SelectedItem;
                    String result = (drv["id"]).ToString();
                    Convert.ToInt32(result);

                    dBcon.openCon();
                    string query = $"DELETE FROM Задачи_отделов WHERE id = @id";
                    SqlCommand sqlCom = new SqlCommand(query, dBcon.getCon());
                    sqlCom.Parameters.AddWithValue("@id", result);
                    sqlCom.ExecuteNonQuery();
                    dBcon.closeCon();
                }
                else if(TC_zad.SelectedItem == TI_pod)
                {
                    DataRowView drv = (DataRowView)dataGrid4.SelectedItem;
                    String result = (drv["id"]).ToString();
                    Convert.ToInt32(result);

                    dBcon.openCon();
                    string query = $"DELETE FROM Задачи_отделов WHERE id = @id";
                    SqlCommand sqlCom = new SqlCommand(query, dBcon.getCon());
                    sqlCom.Parameters.AddWithValue("@id", result);
                    sqlCom.ExecuteNonQuery();
                    dBcon.closeCon();
                }

                BindDataGrid1();
                BindDataGrid2();
                BindDataGrid3();
                BindDataGrid4();
            }
            catch
            {
                MessageBox.Show("Объект не выбран.");
            }

        }

        private void Btn_Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TC_zad.SelectedItem == TI_Free)
                {
                    DataRowView drv = (DataRowView)dataGrid1.SelectedItem;
                    String result = (drv["id"]).ToString();
                    String zad_st_result = (drv["Статус_Задачи"]).ToString();
                    String zad_result = (drv["Задача"]).ToString();
                    String id_otdela_result = (drv["id_отдела"]).ToString();
                    String sot_result = (drv["Сотрудник"]).ToString();

                    Id_zadachi = Convert.ToInt32(result);
                    Id_otdela = Convert.ToInt32(id_otdela_result);
                    Status_zadachi = zad_st_result;
                    Zadacha = zad_result;
                    Sot = sot_result;

                    ChangeWindow changeWindow = new ChangeWindow(_Role, Id_zadachi, Id_otdela, Status_zadachi, Zadacha, Sot);
                    changeWindow.Owner = this;
                    changeWindow.Show();
                }
                else if (TC_zad.SelectedItem == TI_Zan)
                {
                    DataRowView drv = (DataRowView)dataGrid2.SelectedItem;
                    String result = (drv["id"]).ToString();
                    String zad_st_result = (drv["Статус_Задачи"]).ToString();
                    String zad_result = (drv["Задача"]).ToString();
                    String id_otdela_result = (drv["id_отдела"]).ToString();
                    String sot_result = (drv["Сотрудник"]).ToString();

                    Id_zadachi = Convert.ToInt32(result);
                    Id_otdela = Convert.ToInt32(id_otdela_result);
                    Status_zadachi = zad_st_result;
                    Zadacha = zad_result;
                    Sot = sot_result;

                    ChangeWindow changeWindow = new ChangeWindow(_Role, Id_zadachi, Id_otdela, Status_zadachi, Zadacha, Sot);
                    changeWindow.Owner = this;
                    changeWindow.Show();
                }
                else if (TC_zad.SelectedItem == TI_ozh)
                {
                    DataRowView drv = (DataRowView)dataGrid3.SelectedItem;
                    String result = (drv["id"]).ToString();
                    String zad_st_result = (drv["Статус_Задачи"]).ToString();
                    String zad_result = (drv["Задача"]).ToString();
                    String id_otdela_result = (drv["id_отдела"]).ToString();
                    String sot_result = (drv["Сотрудник"]).ToString();

                    Id_zadachi = Convert.ToInt32(result);
                    Id_otdela = Convert.ToInt32(id_otdela_result);
                    Status_zadachi = zad_st_result;
                    Zadacha = zad_result;
                    Sot = sot_result;

                    ChangeWindow changeWindow = new ChangeWindow(_Role, Id_zadachi, Id_otdela, Status_zadachi, Zadacha, Sot);
                    changeWindow.Owner = this;
                    changeWindow.Show();
                }
                else if (TC_zad.SelectedItem == TI_pod)
                {
                    DataRowView drv = (DataRowView)dataGrid4.SelectedItem;
                    String result = (drv["id"]).ToString();
                    String zad_st_result = (drv["Статус_Задачи"]).ToString();
                    String zad_result = (drv["Задача"]).ToString();
                    String id_otdela_result = (drv["id_отдела"]).ToString();
                    String sot_result = (drv["Сотрудник"]).ToString();

                    Id_zadachi = Convert.ToInt32(result);
                    Id_otdela = Convert.ToInt32(id_otdela_result);
                    Status_zadachi = zad_st_result;
                    Zadacha = zad_result;
                    Sot = sot_result;

                    ChangeWindow changeWindow = new ChangeWindow(_Role, Id_zadachi, Id_otdela, Status_zadachi, Zadacha, Sot);
                    changeWindow.Owner = this;
                    changeWindow.Show();
                }   
            }
            catch
            {
                MessageBox.Show("Объект не выбран.");
            }
        }

        private void Btn_MainMenu_Click(object sender, RoutedEventArgs e)
        {            
            MainWindowMenu mainWindowMenu = new MainWindowMenu(_Role, otdelid, _id_sot);
            mainWindowMenu.Show();
            this.Close();
        }

        private void Btn_Complete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid2.SelectedItem;
                String result = (drv["id"]).ToString();
                Convert.ToInt32(result);

                dBcon.openCon();
                string query_3 = $"UPDATE Задачи_отделов SET Статус_задачи = 'Подтверждение' WHERE id = @id";
                SqlCommand sqlCommand_3 = new SqlCommand(query_3, dBcon.getCon());
                sqlCommand_3.Parameters.AddWithValue("@id", result);
                sqlCommand_3.ExecuteNonQuery();

                TC_zad.SelectedItem = TI_ozh;

                BindDataGrid1();
                BindDataGrid2();
                BindDataGrid3();
                BindDataGrid4();
            }
            catch
            {
                MessageBox.Show("Объект не выбран");
            }
        }

        private void Btn_Confirm_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid3.SelectedItem;
                String result = (drv["id"]).ToString();
                Convert.ToInt32(result);

                MessageBoxResult result_YesNO = MessageBox.Show("Задача выполнена успешно?", "Подтверждение", MessageBoxButton.YesNo);
                if (result_YesNO == MessageBoxResult.Yes)
                {
                    dBcon.openCon();
                    string query_3 = $"UPDATE Задачи_отделов SET Статус_задачи = 'Успешно' WHERE id = @id";
                    SqlCommand sqlCommand_3 = new SqlCommand(query_3, dBcon.getCon());
                    sqlCommand_3.Parameters.AddWithValue("@id", result);
                    sqlCommand_3.ExecuteNonQuery();
                    dBcon.closeCon();
                }
                else if (result_YesNO == MessageBoxResult.No)
                {
                    dBcon.openCon();
                    string query_3 = $"UPDATE Задачи_отделов SET Статус_задачи = 'Неуспешно' WHERE id = @id";
                    SqlCommand sqlCommand_3 = new SqlCommand(query_3, dBcon.getCon());
                    sqlCommand_3.Parameters.AddWithValue("@id", result);
                    sqlCommand_3.ExecuteNonQuery();
                    dBcon.closeCon();
                }

                TC_zad.SelectedItem = TI_pod;

                BindDataGrid1();
                BindDataGrid2();
                BindDataGrid3();
                BindDataGrid4();
            }
            catch
            {
                MessageBox.Show("Объект не выбран");
            }
        }

        private void Btn_Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid1.SelectedItem;
                String result = (drv["id"]).ToString();
                Convert.ToInt32(result);

                dBcon.openCon();
                string query_3 = $"UPDATE Задачи_отделов SET Статус_задачи = 'В процессе', Сотрудник = @Сотрудник WHERE id = @id";
                SqlCommand sqlCommand_3 = new SqlCommand(query_3, dBcon.getCon());
                sqlCommand_3.Parameters.AddWithValue("@Сотрудник", _id_sot);
                sqlCommand_3.Parameters.AddWithValue("@id", result);
                sqlCommand_3.ExecuteNonQuery();

                TC_zad.SelectedItem = TI_Zan;

                BindDataGrid1();
                BindDataGrid2();
                BindDataGrid3();
                BindDataGrid4();
            }
            catch
            {
                MessageBox.Show("Объект не выбран");
            }
        }

        private void Btn_Change3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid3.SelectedItem;
                String result = (drv["id"]).ToString();
                String zad_st_result = (drv["Статус_Задачи"]).ToString();
                String zad_result = (drv["Задача"]).ToString();
                String id_otdela_result = (drv["id_отдела"]).ToString();
                String sot_result = (drv["Сотрудник"]).ToString();

                Id_zadachi = Convert.ToInt32(result);
                Id_otdela = Convert.ToInt32(id_otdela_result);
                Status_zadachi = zad_st_result;
                Zadacha = zad_result;
                Sot = sot_result;

                ChangeWindow changeWindow = new ChangeWindow(_Role, Id_zadachi, Id_otdela, Status_zadachi, Zadacha, Sot);
                changeWindow.Owner = this;
                changeWindow.Show();
            }
            catch
            {
                MessageBox.Show("Объект не выбран.");
            }
        }

        private void Btn_Change4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid4.SelectedItem;
                String result = (drv["id"]).ToString();
                String zad_st_result = (drv["Статус_Задачи"]).ToString();
                String zad_result = (drv["Задача"]).ToString();
                String id_otdela_result = (drv["id_отдела"]).ToString();
                String sot_result = (drv["Сотрудник"]).ToString();

                Id_zadachi = Convert.ToInt32(result);
                Id_otdela = Convert.ToInt32(id_otdela_result);
                Status_zadachi = zad_st_result;
                Zadacha = zad_result;
                Sot = sot_result;

                ChangeWindow changeWindow = new ChangeWindow(_Role, Id_zadachi, Id_otdela, Status_zadachi, Zadacha, Sot);
                changeWindow.Owner = this;
                changeWindow.Show();
            }
            catch
            {
                MessageBox.Show("Объект не выбран.");
            }
        }
    }
}
