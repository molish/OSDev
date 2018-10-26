using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSDev.Model
{
    static class StaticData
    {

        public static String USERDATAPATH = @"root/SYSTEM/users/users.txt";
        public static string[] RESTRICTEDTODELETEANDRENAME= {
            "SYSTEM",
            "common",
            "admin",
            "bin",
            "users",
            "processes" 
            };
        public static string[] RESTRICTEDTODELETEANDRENAMEINSIDE =
        {
            "SYSTEM"
        };
        public static Form FormLogin;
        public static Form MainWorkSpace;

    }
}
