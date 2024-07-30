using Diplom_work.Задачи;
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
    /// Логика взаимодействия для GridMem.xaml
    /// </summary>
    public partial class GridMem : Window
    {
        private readonly checkUser _user;
        private int otdelid;
        private int _id_sot;
        public int Id_zadachi;
        public int Id_otdela;
        public string Status_zadachi;
        public string Zadacha;

        DBcon dBcon = new DBcon();

        public GridMem(checkUser user, int _otdelid, int id_sot)
        {
            _id_sot = id_sot;
            _user = user;
            otdelid = _otdelid;
            InitializeComponent();
            BindDataGrid();
        }

        public void BindDataGrid()
        {
            txt_User.Content = $"{_user.Status}";

            dBcon.openCon();

            string query = $"SELECT id, id_отдела, ФИО, Должность, Контактные_данные, Зарплата FROM Сотрудники_отделов";
            SqlCommand command = new SqlCommand(query, dBcon.getCon());

            dBcon.openCon();

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Сотрудники");
            da.Fill(dt);
            dataGrid1.ItemsSource = dt.DefaultView;
            dBcon.closeCon();          
        }

        private void Btn_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindowMenu mainWindowMenu = new MainWindowMenu(_user, otdelid, _id_sot);
            mainWindowMenu.Show();
            this.Close();
        }

        private void Btn_NewTxt_Click(object sender, RoutedEventArgs e)
        {
            CreateNewMem createNewMem = new CreateNewMem();
            createNewMem.Owner = this;
            createNewMem.Show();
        }

        private void Btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            BindDataGrid();
        }
    }
}
