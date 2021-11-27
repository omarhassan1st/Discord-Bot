using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord
{
  public  class SqlMain
    {
        public string Sender = "";
        public string Content = "";
        public string Sendtime = "";
        public string LastSendTime = "";
        public string LastContant = "";
        //SR_UniqueKills====================
        public string KillerName = "";
        public string UniqueName = "";
        public string KilledTime = "";
        public string LastUniqueKilled = "";
        public string LastKilledTime = "";
        //Shard_Char========================
        public string Connection = @"Data Source='"+Program.Sql_Server+ "'; Initial Catalog='"+Program.Sql_DB+"'; User ID='"+Program.Sql_ID+"'; Password='"+Program.Sql_PW+"'";
        public void SR__UniqueKills()
        {
            try
            {
                using (DataTable table = new DataTable())
                {
                    using (SqlConnection conn = new SqlConnection(Connection))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT TOP 1* FROM _LogUniqueKills ORDER BY KilledTime DESC",conn);

                        conn.Open();
                        table.Load(cmd.ExecuteReader());
                        KillerName = table.Rows[0][0].ToString();
                        UniqueName = table.Rows[0][1].ToString();
                        KilledTime = table.Rows[0][2].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SR__Global()
        {
            try
            {
                using (DataTable table = new DataTable())
                {
                    using (SqlConnection conn = new SqlConnection(Connection))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT TOP 1* FROM _LogGlobalChat ORDER BY TimeSent DESC ", conn);

                        conn.Open();
                        table.Load(cmd.ExecuteReader());
                        Sender = table.Rows[0][0].ToString();
                        Content = table.Rows[0][1].ToString();
                        Sendtime = table.Rows[0][2].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
