using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDev.Controllers;
using OSDev.Controllers.Implementations;
using OSDev.Controllers.Interfaces;
using OSDev.Model;
using OSDev.Model.Exception;

namespace OSDev.View
{
    public partial class InputNameDialog : Form
    {
        public InputNameDialog()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

            DataProvider dp = DataPRoviderImpl.getInstance();
            DirectoryController dc = DirectoryControllerImpl.getInstance();
            FileController fc = FileControllerImpl.getInstance(); 
            try
            {
                switch (dp.getOperation())
                {
                    case Operation.CREATEDIR:
                        dc.createDirectory(textBoxName.Text);
                        break;
                    case Operation.CREATEFILE:
                        fc.createFile(textBoxName.Text);
                        break;
                    case Operation.RENAMEDIR:
                        dc.renameDirectory(dp.getBuffer(), textBoxName.Text);
                        break;
                    case Operation.RENAMEFILE:
                        fc.renameFile(dp.getBuffer(), textBoxName.Text);
                        break;
                        
                }
            }
            catch (WrongFileOrDirectoryNameException ex)
            {
                MessageBox.Show("Неверное имя новой папки!");
            }
            catch (FileOrDirectoryOlreadyExistException ex)
            {
                MessageBox.Show("Такая папка уже существует!");
            }
            finally
            {

                MainWorkSpace mws = (MainWorkSpace)dp.getMainWorkSpace();
                mws.refreshListBox();

                dp.resetBuffer();
                StaticData.MainWorkSpace.Enabled = true;
                this.Close();
            }
        }

        private void InputNameDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            StaticData.MainWorkSpace.Enabled = true;
            this.Dispose();
        }
    }
}
