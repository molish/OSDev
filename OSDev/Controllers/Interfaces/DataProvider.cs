using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDev.Model;

namespace OSDev.Controllers
{
    interface DataProvider
    {
        Operation getOperation();
        void setOperation(Operation op);
        void setBuffer(string content);
        string getBuffer();
        void resetBuffer();
        string[] getRestrictedToDeleteAndRename();
        string[] getRestrictedToDeleteAndRenameInside();
        void goToHigherDirectoryLevel();
        string getUserDataPath();
        User getCurrentUser();
        string getCurrentDirectory();
        void setCurrentUser(User user);
        void setCurrentDirectory(string path);
        void setMainWorkSpace(Form form);
        void setFormLogin(Form form);
        Form getMainWorkSpace();
        Form getFormLogin();

    }
}
