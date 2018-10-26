using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using OSDev.Controllers;
using OSDev.Model.Exception;
using OSDev.Controllers.Interfaces;
using OSDev.Controllers.Implementations;

namespace OSDev.View
{
    public partial class MainWorkSpace : Form
    {
        private DataProvider dataProvider;
        private DirectoryController directoryController;
        private FileController fileController;
        private SecurityManager securityManager;
        private CommandController commandController; //TODO: добавить реализацию

        public MainWorkSpace()
        {
            securityManager = SecurityManagerImpl.getInstance();
            dataProvider = DataPRoviderImpl.getInstance();
            directoryController = DirectoryControllerImpl.getInstance();
            fileController = FileControllerImpl.getInstance();
            commandController = CommandControllerImpl.getInstance();
            InitializeComponent();
        }

        private void MainWorkSpace_Load(object sender, EventArgs e)
        {
            
        }

        private void listBoxDirectoryContent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openDirectoryOrFile();
        }

        private void buttonChangeUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.richTextBoxCommandResult.Text = "";
            dataProvider.getFormLogin().Show();
            dataProvider.setCurrentUser(null);
            dataProvider.setCurrentDirectory(null);
        }


        public void reload()
        {
            labelCurrentDirectory.Text = dataProvider.getCurrentDirectory();
            refreshListBox();
        }

        public void refreshListBox()
        {
            listBoxDirectoryContent.Items.Clear();
            listBoxDirectoryContent.Items.AddRange(directoryController.getDirectoryContent(dataProvider.getCurrentDirectory()).ToArray());
            labelCurrentDirectory.Text = dataProvider.getCurrentDirectory();
            if (listBoxDirectoryContent.Items.Count > 0)
            {
                listBoxDirectoryContent.SelectedIndex = 0;
            }
        }

        public void openDirectoryOrFile()
        {
            if (listBoxDirectoryContent.SelectedItem != null)
            {
                if (Directory.Exists(dataProvider.getCurrentDirectory() + "\\" + listBoxDirectoryContent.SelectedItem.ToString()))
                {
                    dataProvider.setCurrentDirectory(dataProvider.getCurrentDirectory() + "\\" + listBoxDirectoryContent.SelectedItem.ToString());
                    refreshListBox();
                }else if(File.Exists(dataProvider.getCurrentDirectory() + "\\" + listBoxDirectoryContent.SelectedItem.ToString()))
                {
                    fileController.openFile(listBoxDirectoryContent.Text.ToString(), richTextBoxCommandResult);
                    richTextBoxCommandResult.ScrollToCaret();
                }
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            dataProvider.getFormLogin().Close();
            this.Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (!dataProvider.getCurrentDirectory().Equals("root"))
            {
                if (!dataProvider.getCurrentDirectory().Equals("root")) {
                    dataProvider.goToHigherDirectoryLevel();
                    refreshListBox();
                }
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            openDirectoryOrFile();
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            if (listBoxDirectoryContent.SelectedItem != null)
            {
                if (checkRights(listBoxDirectoryContent.SelectedItem.ToString()))
                {
                    if (File.Exists(dataProvider.getCurrentDirectory() + '\\' + listBoxDirectoryContent.SelectedItem.ToString()))
                        fileController.deleteFile(listBoxDirectoryContent.SelectedItem.ToString());
                    else
                        directoryController.deleteDirectory(listBoxDirectoryContent.SelectedItem.ToString());
                    refreshListBox();
                }
            }
        }

        private void menuItemRename_Click(object sender, EventArgs e)
        {
            if (listBoxDirectoryContent.SelectedItem != null)
            {
                if (checkRights(dataProvider.getCurrentDirectory() + '\\' + listBoxDirectoryContent.SelectedItem.ToString()))
                {
                    Form input = new InputNameDialog();
                    this.Enabled = false;
                    if (File.Exists(dataProvider.getCurrentDirectory() + '\\' + listBoxDirectoryContent.SelectedItem.ToString()))
                        dataProvider.setOperation(Model.Operation.RENAMEFILE);
                    else
                        dataProvider.setOperation(Model.Operation.RENAMEDIR);
                    dataProvider.setBuffer(listBoxDirectoryContent.SelectedItem.ToString());
                    input.Show();
                }
            }
        }

        private void menuItemAddDirectory_Click(object sender, EventArgs e)
        {
            
            if (checkRights(dataProvider.getCurrentDirectory()))
            {
                Form input = new InputNameDialog();
                dataProvider.setOperation(Model.Operation.CREATEDIR);
                this.Enabled = false;
                input.Show();
            }
            
        }

        private bool checkRights(string name)
        {
            if (!securityManager.havePermission(dataProvider.getCurrentDirectory() + '\\' +  name, dataProvider.getCurrentUser()))
            {
                MessageBox.Show("У вас нет разрешения на редактирование этой папки!");
                return false;
            }
            if (!securityManager.canBeDeletedOrRenamed(dataProvider.getCurrentDirectory() + '\\' + name))
            {
                MessageBox.Show("Нельзя модифицировать эту директорию!");
                return false;
            }
            return true;
        }

        private void MainWorkSpace_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataProvider.getFormLogin().Close();
            this.Dispose();
        }

        private void menuItemAddFile_Click(object sender, EventArgs e)
        {
            if (checkRights(dataProvider.getCurrentDirectory()))
            {
                Form input = new InputNameDialog();
                dataProvider.setOperation(Model.Operation.CREATEFILE);
                this.Enabled = false;
                input.Show();
            }
        }

        private void buttonComplete_Click(object sender, EventArgs e)
        {
            commandController.executeCommand(textBoxCommand.Text, richTextBoxCommandResult, textBoxCommand);
        }
    }
}
