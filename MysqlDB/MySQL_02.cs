using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;


namespace MysqlDB
{
    internal class MySQL_02
    {
        public static void Main()
        {
            string connURL = "server=localhost;database=game;uid=root;pwd=1234;";
            using (MySqlConnection conn = new MySqlConnection(connURL))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from `game`.`quest`";

                // 1. 연결방식의 데이터 읽기 - MySqlDataReader
                //MySqlDataReader mdr = cmd.ExecuteReader();
                //while (mdr.Read())
                //{
                //    Console.WriteLine(mdr["questindex"]);   // 이렇게 한줄씩 해야 한다.
                //    Console.WriteLine(mdr["startspeech"]);
                //    Console.WriteLine(mdr["endspeech"]);
                //}

                // 2. 비연결 방식의 데이터 읽기 - MySqlDataAdapter
                DataSet ds = new DataSet(); // system.data에서 사용
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);   // ds에 모두 입력

                DataTable dt = ds.Tables[0];
                //Console.WriteLine(format(dt));    // 찾아보니까 따로 만든 것이였네.

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Console.WriteLine(item["questindex"]);
                }

            }
        }
    }
}
