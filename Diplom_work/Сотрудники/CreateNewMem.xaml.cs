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

namespace Diplom_work.Задачи
{
    /// <summary>
    /// Логика взаимодействия для CreateNewMem.xaml
    /// </summary>
    public partial class CreateNewMem : Window
    {
        bool Vlu = false;
        public string id_cb;
        DBcon dBcon = new DBcon();

        public CreateNewMem()
        {
            InitializeComponent();
            cBI_id();
        }

        void cBI_id()
        {
            dBcon.openCon();

            String query = $"SELECT id, Название_отдела FROM Отделы_Предприятия";
            SqlCommand sqlCommand = new SqlCommand(query, dBcon.getCon());
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                string Name_id = dr.GetString(1);
                CB_id.Items.Add(Name_id);
            }
        
            dBcon.closeCon();
        }

            private void Btn_next_Click(object sender, RoutedEventArgs e)
        {
            if  (Vlu == false)
            {
                Vlu = true;
                lbl_FIO.Visibility = Visibility.Collapsed;
                lbl_ktkd.Visibility = Visibility.Collapsed;
                ФИО.Visibility = Visibility.Collapsed;
                Контактные_данные.Visibility = Visibility.Collapsed;
                lbl_dol.Visibility = Visibility.Visible;
                lbl_zarp.Visibility = Visibility.Visible;
                Должность.Visibility = Visibility.Visible;
                Зарплата.Visibility = Visibility.Visible;
                lbl_otdel.Visibility = Visibility.Visible;
                CB_id.Visibility = Visibility.Visible;
                btn_next.Content = "Принять и сохранить";
            }
            else
            {
                dBcon.openCon();
                try
                {
                    string name_id = CB_id.SelectedItem.ToString();
                    string query = $"SELECT id FROM Отделы_Предприятия WHERE Название_отдела = '{name_id}'";
                    SqlCommand SqlCmd = new SqlCommand(query, dBcon.getCon());
                    id_cb = SqlCmd.ExecuteScalar().ToString();
                    int.Parse(id_cb);
                    var FIO = ФИО.Text;
                    var ktkd = Контактные_данные.Text;
                    var dol = Должность.Text;
                    var zarp = Зарплата.Text;

                    String query1 = $"INSERT INTO Сотрудники_отделов(id_отдела, ФИО, Контактные_данные, Должность, Зарплата) VALUES ('{id_cb}', '{FIO}', '{ktkd}', '{dol}', '{zarp}')";

                    SqlCommand SqlCmd1 = new SqlCommand(query1, dBcon.getCon());
                    SqlCmd1.ExecuteNonQuery();

                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Заполните все данные!");
                }
                dBcon.closeCon();
            }
            
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            if (Vlu == true)
            {
                Vlu = false;
                lbl_FIO.Visibility = Visibility.Visible;
                lbl_ktkd.Visibility = Visibility.Visible;
                ФИО.Visibility = Visibility.Visible;
                Контактные_данные.Visibility = Visibility.Visible;
                lbl_dol.Visibility = Visibility.Collapsed;
                lbl_zarp.Visibility = Visibility.Collapsed;
                Должность.Visibility = Visibility.Collapsed;
                Зарплата.Visibility = Visibility.Collapsed;
                lbl_otdel.Visibility = Visibility.Collapsed;
                CB_id.Visibility = Visibility.Collapsed;
                btn_next.Content = "Вперед";
            }
            else
            {
                this.Close();
            }           
        }
    }
}
