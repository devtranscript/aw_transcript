using System.Data.SqlClient;

namespace aw_transcript
{
    public class conn_svr
    {
        public static string strconn_sql
        {
            get
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "localhost";
                builder.InitialCatalog = "db_transcript";
                builder.UserID = "u_transcript";
                builder.Password = "7r4n5Cr1p7";
                builder.ConnectTimeout = 0;

                //MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                //conn_string.in = "";
                //conn_string.UserID = "";
                //conn_string.Password = "";
                //conn_string.Database = "";
                //conn_string.Port = 0;
                ////conn_string.ConnectionTimeout = 0;

                return builder.ConnectionString;
            }
        }
    }
}