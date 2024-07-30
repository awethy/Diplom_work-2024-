using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom_work
{
    public class checkUser
    {

        public string Login { get; set; }
        public bool IsAdmin { get; }
        public bool IsRuk { get; }
        public string Status;

        public checkUser(bool Admin, bool Ruk, bool Sot)
        {
            if (Admin == true)
            {
                IsAdmin = Admin;
                IsRuk = Ruk;
                Status = "Admin";
            }
            else if (Ruk == true & Admin == false)
            {
                IsRuk = Ruk;
                Status = "Руководитель";
            }
            else
            {
                Status = "Сотрудник";
            }
        }
    }
}
