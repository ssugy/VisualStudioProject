using MySql.Data.MySqlClient;

namespace MysqlDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionInfo = "server=localhost;port=3306;Database=login;Uid=root;Pwd=1234";
            using (MySqlConnection sqlcon = new MySqlConnection(connectionInfo))
            {
                sqlcon.Open();
                string sql = "select * from user";
                MySqlCommand cmd = new MySqlCommand(sql, sqlcon);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int id = (int)r["iduser"];
                    string name = (string)r["name"];
                    string address = (string)r["adress"];
                    Console.WriteLine($"{id} // {name} // {address}");
                }
                r.Close();
                sqlcon.Close();
            }
        }
    }
}