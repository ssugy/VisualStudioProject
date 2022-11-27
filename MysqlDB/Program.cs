using MySql.Data.MySqlClient;

namespace MysqlDB
{
    internal class Program
    {
        static void _Main(string[] args)
        {
            string connectionInfo = "server=localhost;port=3306;Database=login;Uid=root;Pwd=1234";
            using (MySqlConnection sqlcon = new MySqlConnection(connectionInfo))
            {
                sqlcon.Open();
                // index가 3인 이름을 "제르딘"으로 변경
                string sql = "update `user` set name = '자르딘' where iduser = 3";
                MySqlCommand cmd = new MySqlCommand(sql, sqlcon);
                string sql2 = "select * from user";
                MySqlCommand cmd2 = new MySqlCommand(sql2, sqlcon);

                MySqlDataReader r = cmd2.ExecuteReader();
                while (r.Read())
                {
                    int id = (int)r["iduser"];
                    string name = (string)r["name"];
                    string address = (string)r["adress"];
                    // DB에서 tinyint로 정의한 자료형 - 읽기 위해서는 GetByte로 사용한다.
                    // byte data = r.GetByte("gender");
                    Console.WriteLine($"{id} // {name} // {address}");
                }
                r.Close();
                sqlcon.Close();
            }
        }
    }
}