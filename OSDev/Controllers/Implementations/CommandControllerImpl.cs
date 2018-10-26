using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDev.Controllers.Implementations;
using OSDev.Controllers.Interfaces;
using OSDev.Model.Exception;
using OSDev.View;
using OSDev.Model;

namespace OSDev.Controllers
{
    class CommandControllerImpl : CommandController
    {
        private DataProvider dataProvider;
        private DirectoryController directoryController;
        private FileController fileController;
        private SecurityManager securityManager;
        private UserManager userManager;

        private static CommandControllerImpl instance;

        private CommandControllerImpl()
        {
            dataProvider = DataPRoviderImpl.getInstance();
            directoryController = DirectoryControllerImpl.getInstance();
            fileController = FileControllerImpl.getInstance();
            securityManager = SecurityManagerImpl.getInstance();
            userManager = UserManagerImpl.getInstance();
        }

        public static CommandControllerImpl getInstance()
        {
            if (instance == null)
                instance = new CommandControllerImpl();
            return instance;
        }

        public void executeCommand(string command, RichTextBox output, TextBox input)
        {
            if (command.Equals(String.Empty))
            {
                output.AppendText("Не введена команда!\n.\n.\n.\n");
                return;
            }

            output.AppendText(command);
            input.Text = String.Empty;
            string commandName = command.Split('\"')[0].Split(' ')[0];

            switch (commandName)
            {
                case "help" :
                    executeHelp();
                    break;
                case "clear":
                    executeClear(output);
                    break;
                case "adduser":
                    executeAddUser(command, output);
                    break;
                case "rmuser":
                    executeRmUser(command, output);
                    break;
                case "cd":
                    executeCd(command, output);
                    break;
                case "rmdir":
                    executeRmDir(command, output);
                    break;
                case "renamedir":
                    executeRenameDir(command, output);
                    break;
                case "mkdir":
                    executeMkDir(command, output);
                    break;
                case "mkfile":
                    executeMkFile(command, output);
                    break;
                case "rmfile":
                    executeRmFile(command, output);
                    break;
                case "openfile":
                    executeOpenFile(command, output);
                    break;
                case "renamefile":
                    executeRenameFile(command, output);
                    break;
                case "writetofile":
                    executeWriteToFile(command, output);
                    break;
                default :
                    executeDefault(output);
                    break;
            }
        }

        private void executeHelp()
        {
            
        }

        private void executeClear(RichTextBox output)
        {
            output.Text = "";
        }

        private void executeDefault(RichTextBox output)
        {
            output.AppendText("Неизвестная команда!");
            outCommas(output);
        }

        private void executeAddUser(string command, RichTextBox output)
        {
            if(!(dataProvider.getCurrentUser().Role == Roles.ADMIN))
            {
                output.AppendText("у вас нет права на использование этой команды");
                outCommas(output);
            }
            if(!(command.Split(' ').Length < 4))
            {
                Roles role;
                switch(command.Split(' ')[3])
                {
                    case "admin" :
                        role = Roles.ADMIN;
                        break;
                    case "user":
                        role = Roles.USER;
                        break;
                    default:
                        output.AppendText("НЕ удалоь считать роль пользователя");
                        outCommas(output);
                        return;
                }
                string[] args = command.Split(' ');
                string res = userManager.addUser(args[1], args[2], role);
                if (res.Equals("User successfully added"))
                {
                    output.AppendText(res);
                    Directory.CreateDirectory("root\\" + args[1]);
                    outCommas(output);
                }
                else
                {
                    output.AppendText(res);
                    outCommas(output);
                }
            }
            else
            {
                output.AppendText(" не введены новый логин пароль роль пользователя");
                outCommas(output);
            }
            MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
            mws.refreshListBox();
        }

        private void executeRmUser(string command, RichTextBox output)
        {
            if (!(dataProvider.getCurrentUser().Role == Roles.ADMIN))
            {
                output.AppendText("у вас нет права на использование этой команды");
                outCommas(output);
            }
            if (!(command.Split(' ').Length < 2))
            {
                
                string res = userManager.removeUser(command.Split(' ')[1]);
                if (res.Equals("User successfully delete"))
                {
                    output.AppendText(res);
                    Directory.Delete("root\\" + command.Split(' ')[1]);
                    outCommas(output);
                }
                else
                {
                    output.AppendText(res);
                    outCommas(output);
                }
            }
            else
            {
                output.AppendText("Не введено имя пользователя");
                outCommas(output);
            }
            MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
            mws.refreshListBox();
        }

        private void executeCd(string command, RichTextBox output)
        {
            if(command.Split(' ')[1] != null)
            {
                if (command.Split(' ')[1].Equals('.'))
                {
                    dataProvider.setCurrentDirectory("root");
                    MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
                    mws.refreshListBox();
                }
                else
                if (command.Split(' ')[1].Equals("\\\\") && !dataProvider.getCurrentDirectory().Equals("root"))
                {
                    dataProvider.goToHigherDirectoryLevel();
                    MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
                    mws.refreshListBox();
                }
                else
                {
                    string path = "";
                    if (isFullOrRelativePAth(command.Split(' ')[1]))
                    {
                        if (!File.Exists(command.Split(' ')[1]) && !Directory.Exists(command.Split(' ')[1]))
                        {
                            fileOrDirDoesnotExists(output);
                            return;
                        }
                        path = command.Split(' ')[1];
                    }
                    else
                    {
                        if (!File.Exists(dataProvider.getCurrentDirectory() + '\\' + command.Split(' ')[1]) && !Directory.Exists(dataProvider.getCurrentDirectory() + '\\' + command.Split(' ')[1]))
                        {
                            fileOrDirDoesnotExists(output);
                            return;
                        }
                        path = dataProvider.getCurrentDirectory() + '\\' + command.Split(' ')[1];
                    }
                    dataProvider.setCurrentDirectory(path);
                    MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
                    mws.refreshListBox();
                }
            }
            else
            {
                output.AppendText("не введено имя папки");
            }
            outCommas(output);
        }

        private void executeRmDir(string command, RichTextBox output)
        {
            string path = getPath(command, output);
            if (path.Equals("")) return;
            if (!checkRights(path, output)) return;
            directoryController.deleteDirectory(path);
            MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
            mws.refreshListBox();
            outCommas(output);
        }

        private void executeRenameDir(string command, RichTextBox output)
        {
            string path = getPath(command, output);
            if (path.Equals("")) return;
            if (command.Split(' ')[2] == null)
            {
                output.AppendText("Введите новое имя папки!");
            }
            if (!checkRights(path, output)) return;
            try
            {
                directoryController.renameDirectory(path, command.Split(' ')[2]);
                MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
                mws.refreshListBox();
            }
            catch (WrongFileOrDirectoryNameException ex)
            {
                output.AppendText("Неверное новое имя папки!");
            }
            catch (FileOrDirectoryOlreadyExistException ex)
            {
                output.AppendText("Папка с таким именем уже существует!");
            }
            finally
            {
                outCommas(output);
            }
        }

        private void executeMkDir(string command, RichTextBox output)
        {
            if (command.Split(' ')[1] == null)
            {
                output.AppendText("Не введен путь к папке либо имя папки!");
                return;
            }
            string path = command.Split(' ')[1];
            if (path.Equals("")) return;
            if (!checkRights(path, output)) return;
            try
            {
                directoryController.createDirectory(path);
                MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
                mws.refreshListBox();
            }
            catch (WrongFileOrDirectoryNameException ex)
            {
                output.AppendText("Неверное имя папки!");
            }
            catch (FileOrDirectoryOlreadyExistException ex)
            {
                output.AppendText("Папка с таким именем уже существует!");
            }
            finally
            {
                outCommas(output);
            }
        }

        private void executeMkFile(string command, RichTextBox output)
        {
            if (command.Split(' ')[1] == null)
            {
                output.AppendText("Не введен путь к файлу либо имя файла!");
                return;
            }
            string path = command.Split(' ')[1];
            if (path.Equals("")) return;
            if (!checkRights(path, output)) return;
            try
            {
                fileController.createFile(path);
                MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
                mws.refreshListBox();
            }
            catch (WrongFileOrDirectoryNameException ex)
            {
                output.AppendText("Неверное имя файла!");
            }
            catch (FileOrDirectoryOlreadyExistException ex)
            {
                output.AppendText("Файл с таким именем уже существует!");
            }
            finally
            {
                outCommas(output);
            }
        }

        private void executeRmFile(string command, RichTextBox output)
        {
            string path = getPath(command, output);
            if (path.Equals("")) return;
            if (!checkRights(path, output)) return;
            fileController.deleteFile(path);
            MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
            mws.refreshListBox();
            outCommas(output);
        }

        private void executeOpenFile(string command, RichTextBox output)
        {
            string path = getPath(command, output);
            if (path.Equals("")) return;
            fileController.openFile(path, output);
            outCommas(output);
        }

        private void executeRenameFile(string command, RichTextBox output)
        {
            string path = getPath(command, output);
            if (path.Equals("")) return;
            if (command.Split(' ')[2] == null) {
                output.AppendText("Введите новое имя файла!");
            }
            if (!checkRights(path, output)) return;
            try
            {
                fileController.renameFile(path, command.Split(' ')[2]);
                MainWorkSpace mws = (MainWorkSpace)dataProvider.getMainWorkSpace();
                mws.refreshListBox();
            }
            catch (WrongFileOrDirectoryNameException ex)
            {
                output.AppendText("Неверное новое имя файла!");
            }
            catch (FileOrDirectoryOlreadyExistException ex)
            {
                output.AppendText("Файл с таким именем уже существует!");
            }
            finally
            {
                outCommas(output);
            }
        }

        private void executeWriteToFile(string command, RichTextBox output)
        {
            string path = getPath(command, output);
            if (path.Equals("")) return;
            if (!checkRights(path, output)) return;
            if(command.Split(' ')[2] == null)
            {
                output.AppendText("Не введен текст!");
                return;
            }
            StringBuilder msg = new StringBuilder();
            string[] message = command.Split(' ');
            for(int i = 2; i < message.Length; i++)
            {
                msg.Append(message[i] + " ");
            }
            fileController.writeFile(path, msg.ToString());
            outCommas(output);
        }

        private bool isFullOrRelativePAth(string path)
        {
            if (path.Split('\\')[0].Equals("root")) return true;
            return false;
        }

        private void outCommas(RichTextBox output)
        {
            output.AppendText("\n.\n");
            output.ScrollToCaret();
        }

        private void fileOrDirDoesnotExists(RichTextBox output)
        {
            output.AppendText("Файл или папка не существует!");
            outCommas(output);
        }

        private void needRelativeFileName(RichTextBox output)
        {
            output.AppendText("Введите имя папки относительно той в которой вы сейчас находитесь!");
            outCommas(output);
        }

        private bool checkNameArgument(string command, RichTextBox output)
        {
            if (isFullOrRelativePAth(command.Split(' ')[1]))
            {
                if (!File.Exists(command.Split(' ')[1]) && !Directory.Exists(command.Split(' ')[1]))
                {
                    fileOrDirDoesnotExists(output);
                    return false;
                }
                needRelativeFileName(output);
                return false;
            }
            else
            {
                if (!File.Exists(dataProvider.getCurrentDirectory() + '\\' + command.Split(' ')[1]) && !Directory.Exists(dataProvider.getCurrentDirectory() + '\\' + command.Split(' ')[1]))
                {
                    fileOrDirDoesnotExists(output);
                    return false;
                }
                return true;
            }
        }

        private bool checkRights(string name, RichTextBox output)
        {
            if (!securityManager.havePermission(dataProvider.getCurrentDirectory() + '\\' + name, dataProvider.getCurrentUser()))
            {
                output.AppendText("У вас нет разрешения на редактирование этой папки!");
                outCommas(output);
                return false;
            }
            if (!securityManager.canBeDeletedOrRenamed(dataProvider.getCurrentDirectory() + '\\' + name))
            {
                output.AppendText("Нельзя модифицировать эту директорию!");
                outCommas(output);
                return false;
            }
            return true;
        }

        private string getPath(string command, RichTextBox output)
        {
            if (command.Split(' ')[1] == null)
            {
                output.AppendText("Не введен путь к файлу либо имя файла!");
                return "";
            }
            if (!checkNameArgument(command, output))
                return "";
            return command.Split(' ')[1];
        }
    }
}
