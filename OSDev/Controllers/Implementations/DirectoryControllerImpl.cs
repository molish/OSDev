using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSDev.Model.Exception;

namespace OSDev.Controllers
{
    class DirectoryControllerImpl : DirectoryController
    {
        private DataProvider dataProvider;

        private static DirectoryControllerImpl instance;

        private DirectoryControllerImpl()
        {
            dataProvider = DataPRoviderImpl.getInstance();
        }

        public static DirectoryControllerImpl getInstance()
        {
            if (instance == null)
                instance = new DirectoryControllerImpl();
            return instance;
        }

        public List<string> getDirectoryContent(string path)
        {
            List<string> result = new List<string>();
            string[] buf;

            buf = Directory.GetDirectories(path);
            clearPathName(buf);
            result.AddRange(buf);
            buf = Directory.GetFiles(path);
            clearPathName(buf);
            result.AddRange(buf);
            result.Sort();
            
            return result;
        }

        public void deleteDirectory(string name)
        {
            Directory.Delete(dataProvider.getCurrentDirectory() + "\\" + name, true);
            //Directory.Delete(name, true);
        }

        public void renameDirectory(string oldName, string newName)
        {
            
            createDirectory(newName);
            foreach(string str in Directory.GetDirectories(dataProvider.getCurrentDirectory() + '\\' + oldName))
            {
                Directory.Move(dataProvider.getCurrentDirectory() + '\\' + oldName, dataProvider.getCurrentDirectory() + '\\' + newName);
            }
            foreach (string str in Directory.GetFiles(dataProvider.getCurrentDirectory() + '\\' + oldName))
            {
                File.Move(dataProvider.getCurrentDirectory() + '\\' + oldName, dataProvider.getCurrentDirectory() + '\\' + newName);
            }
            Directory.Delete(dataProvider.getCurrentDirectory() + '\\' + oldName);
        }

        public void createDirectory(string name)
        {
            if ((dataProvider.getCurrentDirectory() + '\\' + name).Equals(dataProvider.getCurrentDirectory() + '\\') || Directory.Exists(dataProvider.getCurrentDirectory() + '\\' + name))
                throw new FileOrDirectoryOlreadyExistException();
            if (!SecurityManagerImpl.getInstance().isDirNameCorrect(name))
                throw new WrongFileOrDirectoryNameException();
            Directory.CreateDirectory(dataProvider.getCurrentDirectory() + '\\' + name);
            //Directory.CreateDirectory(name);
        }

        private void clearPathName(string[] strArr)
        {
            for(int i = 0; i < strArr.Length; i++)
            {
                strArr[i] = strArr[i].Split('\\').Last();
            }
        }

    }
}
