using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moxbot
{
    internal class SqlManager
    {
        public static int CountOfLines(SqlConnection sqlConnection, string request)
        {
            string sqlRequest = request;

            SqlCommand command = new SqlCommand(request, sqlConnection);

            int confirmMessage = Convert.ToInt32(command.ExecuteScalar());

            return confirmMessage;
        }

        public static string GetString(SqlConnection sqlConnection, string request)
        {
            string sqlRequest = request;

            SqlCommand command = new SqlCommand(request, sqlConnection);

            string confirmMessage = command.ExecuteScalar().ToString();

            return confirmMessage;
        }
    }
}
