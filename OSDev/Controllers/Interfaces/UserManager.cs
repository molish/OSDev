using OSDev.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDev.Controllers
{
    interface UserManager
    {
        string addUser(string login, string password, Roles role);

        string removeUser(string login);

        User tryLogin(String login, String password);

    }
}
