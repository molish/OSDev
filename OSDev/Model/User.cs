using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDev.Model
{
    class User
    {

        private string name;

        public string Name
        {
            get
            {
                return name;
            }

        }
        private Roles role;

        public Roles Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }


        public User(string _name, string _role)
        {
            name = _name;
            switch (_role)
            {
                case "admin": role = Roles.ADMIN;
                    break;
                case "user": role = Roles.USER;
                    break;
                default : role = Roles.USER;
                    break;
            }
        }

        public void sdsfs() { }

    }
}
