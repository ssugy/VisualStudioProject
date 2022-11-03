using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBasic_Enum
{
    internal class User
    {
        private int userNum = 0;

        public void SetUserNum(int num)
        {
            userNum = num;
        }

        public int GetUserNum() => userNum;
    }
}
