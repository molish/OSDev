using OSDev.Controllers.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDev.Model.Exception;

namespace OSDev.Controllers.Implementations
{
    class FileControllerImpl : FileController
    {
        private DataProvider dataProvider;

        private static FileControllerImpl instance;

        private FileControllerImpl()
        {
            dataProvider = DataPRoviderImpl.getInstance();
        }

        public static FileControllerImpl getInstance()
        {
            if (instance == null)
                instance = new FileControllerImpl();
            return instance;
        }

        public void createFile(string name)
        {
            if ((dataProvider.getCurrentDirectory() + '\\' + name).Equals(dataProvider.getCurrentDirectory() + '\\') || File.Exists(dataProvider.getCurrentDirectory() + '\\' + name))
                throw new FileOrDirectoryOlreadyExistException();
            if (!SecurityManagerImpl.getInstance().isFileNameCorrect(name))
                throw new WrongFileOrDirectoryNameException();
            File.Create(dataProvider.getCurrentDirectory() + '\\' + name);
        }

        public void deleteFile(string name)
        {
            File.Delete(dataProvider.getCurrentDirectory() + '\\' + name);
        }

        public void openFile(string name, RichTextBox output)
        {
            using (StreamReader sr = new StreamReader(dataProvider.getCurrentDirectory() + '\\' + name))
            {
                while (!sr.EndOfStream)
                {
                    output.AppendText(sr.ReadLine() + "\n");
                }
                output.AppendText("\n.\n");
            }
        }

        public void renameFile(string oldName, string newName)
        {
            createFile(newName);
            using (StreamReader sr = new StreamReader(dataProvider.getCurrentDirectory() + '\\' + oldName))
            {
                using (StreamWriter sw = new StreamWriter(dataProvider.getCurrentDirectory() + '\\' + newName))
                {
                    while (!sr.EndOfStream)
                    {
                        sw.WriteLine(sr.ReadLine());
                        sw.Flush();
                    }
                }
            }
        }

        public void writeFile(string name, string text)
        {
            using (StreamWriter sw = new StreamWriter(dataProvider.getCurrentDirectory() + '\\' + name))
            {
                sw.Write(text);
                sw.Flush();
            }
        }

    }
}
