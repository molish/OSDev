using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSDev.Model;

namespace OSDev.Controllers
{
    interface SecurityManager
    {
        bool isFileNameCorrect(string name);
        bool isDirNameCorrect(string name);
        bool canBeDeletedOrRenamed(string path);
        bool havePermission(string path, User user);
        bool loginIsCorrect(string login);
        bool passwordIsCorrect(string password);
    }
}
