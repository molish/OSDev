using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDev.Model;

namespace OSDev.Controllers
{
    class DataPRoviderImpl : DataProvider
    {

        private DynamicData dynamicData;

        private static DataPRoviderImpl instance;

        private DataPRoviderImpl() {
            dynamicData = DynamicData.getInstance();
        }

        public static DataPRoviderImpl getInstance()
        {
            if (instance == null)
                instance = new DataPRoviderImpl();
            return instance;
        }

        public string getUserDataPath()
        {
            return StaticData.USERDATAPATH;
        }

        public User getCurrentUser()
        {
            return dynamicData.currentUser;
        }

        public string getCurrentDirectory()
        {
            return dynamicData.currentDir;
        }

        public void setCurrentUser(User user)
        {
            dynamicData.currentUser = user;
        }

        public void setCurrentDirectory(string path)
        {
            dynamicData.currentDir = path;
        }

        public Form getFormLogin()
        {
            return StaticData.FormLogin;
        }

        public Form getMainWorkSpace()
        {
            return StaticData.MainWorkSpace;
        }

        public void setFormLogin(Form form)
        {
            StaticData.FormLogin = form;
        }
        public void setMainWorkSpace(Form form)
        {
            StaticData.MainWorkSpace = form;
        }
        public void goToHigherDirectoryLevel()
        {

            string path = dynamicData.currentDir;

            dynamicData.currentDir = path.Remove(path.Length - (path.Split('\\').Last().Length + 1));

        }

        public string[] getRestrictedToDeleteAndRename()
        {
            return StaticData.RESTRICTEDTODELETEANDRENAME;
        }

        public string[] getRestrictedToDeleteAndRenameInside()
        {
            return StaticData.RESTRICTEDTODELETEANDRENAMEINSIDE;
        }

        public void setBuffer(string content)
        {
            dynamicData.buffer = content;
        }

        public string getBuffer()
        {
            return dynamicData.buffer;
        }

        public void resetBuffer()
        {
            dynamicData.buffer = "";
        }

        public void setOperation(Operation op)
        {
            dynamicData.operation = op;
        }

        public Operation getOperation()
        {
            return dynamicData.operation;
        }
    }
}
