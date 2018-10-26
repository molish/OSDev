using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSDev.Model;
using OSDev.Controllers;

namespace OSDev.Controllers
{
    class SecurityManagerImpl : SecurityManager
    {
        DataProvider dataProvider;

        private static SecurityManagerImpl instance;

        private SecurityManagerImpl() {

            dataProvider = DataPRoviderImpl.getInstance();
            
        }

        public static SecurityManagerImpl getInstance()
        {
            if (instance == null)
                instance = new SecurityManagerImpl();
            return instance;

        }

        public bool canBeDeletedOrRenamed(string path)
        {
            if (path.Equals("root")) return false;
            string pathSecondLevel = path.Split('\\')[1];
            if (dataProvider.getCurrentDirectory().Equals("root")) return false;
            foreach (string str in dataProvider.getRestrictedToDeleteAndRename())
                if (str.Equals(pathSecondLevel)) return false;
            foreach (string str in dataProvider.getRestrictedToDeleteAndRenameInside())
                if (str.Equals(pathSecondLevel)) return false;
            return true;
        }

        public bool havePermission(string path, User user)
        {
            if (user.Role == Roles.ADMIN) return true;
            if ((path.Split('\\').Length > 1) && (path.Split('\\')[1].Equals(user.Name))) return true;
            if ((path.Split('\\').Length > 1) && (path.Split('\\')[1].Equals("common"))) return true;
                return false;
        }

        public bool loginIsCorrect(string login)
        {
            return !containsRestrictedSymbols(login);
        }

        public bool passwordIsCorrect(string password)
        {
            return !containsRestrictedSymbols(password);
        }

        private bool containsRestrictedSymbols(string str)
        {
            if (str.Length <= 0) return true;
            foreach (char ch in str)
            {
                if (ch.Equals(':')) return true;
                if (ch.Equals(' ')) return true;
                if (ch.Equals('\"')) return true;
                if (ch.Equals('\\')) return true;
            }
            return false;
        }

        public bool isDirNameCorrect(string name)
        {
            return !containsRestrictedSymbols(name);
        }

        public bool isFileNameCorrect(string name)
        {
            return !containsRestrictedSymbols(name);
        }
        
    }
}
