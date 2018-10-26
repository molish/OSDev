using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSDev.Controllers;
using OSDev.Model;
using OSDev.View;

namespace OSDev
{
    public partial class FormLogin : Form
    {
        private Form mainWorkSpace;

        DataProvider dataProv;

        public FormLogin()
        {
            dataProv = DataPRoviderImpl.getInstance();
            checkFileSystem();
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            UserManager userManager = UserManagerImpl.getInstance();
            User user = userManager.tryLogin(txtBoxLogin.Text, textBoxPassword.Text);
            if (user != null)
            {
                this.Visible = false;
                dataProv.setCurrentUser(user);
                dataProv.setCurrentDirectory("root");

                mainWorkSpace = new MainWorkSpace();
                MainWorkSpace mws = (MainWorkSpace)mainWorkSpace;
                mws.reload();
                dataProv.setMainWorkSpace(mainWorkSpace);

                mainWorkSpace.Show();

                txtBoxLogin.Text = String.Empty;
                textBoxPassword.Text = String.Empty;
                ErrorLabel.Text = String.Empty;
            }
            ErrorLabel.Text = "Не удалось войти всистему";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (mainWorkSpace != null)
                mainWorkSpace.Close();
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            dataProv.setFormLogin(this);
        }

        private void checkFileSystem()
        {
            if (!Directory.Exists("root")) 
            {
                Directory.CreateDirectory("root");
            }
            if (!Directory.Exists("root\\SYSTEM")) 
            {
                Directory.CreateDirectory("root\\SYSTEM");
            }
            if (!Directory.Exists("root\\common")) 
            {
                Directory.CreateDirectory("root\\common");
            }
            if (!Directory.Exists("root\\SYSTEM\\bin")) 
            {
                Directory.CreateDirectory("root\\SYSTEM\\bin");
            }
            if (!Directory.Exists("root\\SYSTEM\\processes"))
            {
                Directory.CreateDirectory("root\\SYSTEM\\processes");
            }
            if (!Directory.Exists("root\\SYSTEM\\users"))
            {
                Directory.CreateDirectory("root\\SYSTEM\\users");
            }
            if (!File.Exists("root\\SYSTEM\\users\\users.txt"))
            {
                File.Create("root\\SYSTEM\\users\\users.txt");
                using (StreamWriter sw = new StreamWriter("root\\SYSTEM\\users\\users.txt"))
                {
                    sw.WriteLine("admin:admin:admin");
                    if (!Directory.Exists("root\\admin"))
                    {
                        Directory.CreateDirectory("root\\admin");
                    }
                }
            }
            else
            {
                bool needAdmin = true;
                string buf = String.Empty;
                using(StreamReader sr = new StreamReader("root\\SYSTEM\\users\\users.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        buf = sr.ReadLine();
                        if (buf == null)
                        {
                            break;
                        }
                        if(!buf.Equals(String.Empty) && buf.Split(':')[2] != null && buf.Split(':')[2].Equals("admin"))
                        {
                            needAdmin = false;
                        }
                    }
                }
                if (needAdmin)
                {
                    using (StreamWriter sw = new StreamWriter("root\\SYSTEM\\users\\users.txt"))
                    {
                        sw.WriteLine("admin:admin:admin");
                        if (!Directory.Exists("root\\admin"))
                        {
                            Directory.CreateDirectory("root\\admin");
                        }
                    }
                }
            }
        }
    }
}
