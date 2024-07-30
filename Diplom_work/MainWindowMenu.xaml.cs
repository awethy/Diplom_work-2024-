using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для MainWindowMenu.xaml
    /// </summary>
    public partial class MainWindowMenu : Window
    {
        private readonly checkUser _Role;
        private int otdelid;
        private int id_sot;
        DBcon dBcon = new DBcon();

        public MainWindowMenu(checkUser Role, int _otdelid, int _id_sot)
        {
            id_sot = _id_sot;
            otdelid = _otdelid;
            _Role = Role;
            InitializeComponent();
            LoadForm();
        }

        private void isAdmin()
        {
            btn_Members.IsEnabled = _Role.IsAdmin;
            btn_Otdels.IsEnabled = _Role.IsAdmin;
        }

        public void LoadForm()
        {
            txt_User.Content = $"{_Role.Status}";
            isAdmin();
        }

        private void Btn_Zdch_Click(object sender, RoutedEventArgs e)
        {
            DataGridView dataGridView = new DataGridView(_Role, otdelid, id_sot);
            dataGridView.Show();
            this.Close();
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Exit", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {            }
        }

        private void Btn_Members_Click(object sender, RoutedEventArgs e)
        {
            GridMem gridMem = new GridMem(_Role, otdelid, id_sot);
            gridMem.Show();
            this.Close();
        }
    }
}
