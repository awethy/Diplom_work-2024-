using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Diplom_work
{
    public class DBcon
    {
        SqlConnection sqlCon = new SqlConnection(@"data source=DESKTOP-01A825I;initial catalog=Diplomdb; integrated security =True"); 
        
        public void openCon()
        {
            if (sqlCon.State == System.Data.ConnectionState.Closed)
            {
                sqlCon.Open();
            }
        }

        public void closeCon()
        {
            if (sqlCon.State == System.Data.ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }

        public SqlConnection getCon()
        {
            return sqlCon;
        }
    }
}
