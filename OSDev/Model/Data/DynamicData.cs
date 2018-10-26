using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDev.Model
{
    class DynamicData
    {
        private static DynamicData instance;
        private DynamicData() {
            currentDir = "root";
            buffer = "";
        }

        public static DynamicData getInstance()
        {
            if (instance == null)
                instance = new DynamicData();
            return instance;
        }

        public User currentUser;
        public string currentDir;
        public string buffer;
        public Operation operation;

    }
}
