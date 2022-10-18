using System.Data.SqlClient;

namespace exam
{
    class DB
    {
        static public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Шарага\C#\02.01\Shop\Trade.mdf;Integrated Security=True;Connect Timeout=30";

        static public SqlConnection sqlConnection = new SqlConnection(connectionString);

        static public SqlCommand sqlCommand = null;
    }
}
