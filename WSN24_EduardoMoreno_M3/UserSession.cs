using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSN24_EduardoMoreno_M3
{
    public class UserSession
    {
         public static User CurrentUser { get; set; }

         public static void Logout()
         {
            CurrentUser = null;
         }

        public class User
        {
            public int UserID { get; set; }
            public string Username { get; set; }
        }
    }
}
