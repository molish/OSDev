using OSDev.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDev.Controllers
{
    class UserManagerImpl : UserManager
    {

        private static UserManagerImpl instance;

        private DataProvider dataProvider;
        private SecurityManager securityManager;

        private UserManagerImpl(DataProvider dataManager, SecurityManager securityManager)
        {
            dataProvider = dataManager;
            this.securityManager = securityManager;
        }

        public static UserManagerImpl getInstance() {
            if (instance == null)
                instance = new UserManagerImpl(DataPRoviderImpl.getInstance(), SecurityManagerImpl.getInstance());
            return instance;
        }

        public string addUser(string login, string password, Roles role)
        {
            string result = "User successfully added";
            if (userExist(login))
                return "User already exist!";
            if (!securityManager.loginIsCorrect(login))
                return "User login is incorrect!";
            if (!securityManager.passwordIsCorrect(password))
                return "User password is incorrect!";
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(dataProvider.getUserDataPath()))
            {
                while(!sr.EndOfStream)
                    sb.AppendLine(sr.ReadLine());
            }
            using (StreamWriter sw = new StreamWriter(dataProvider.getUserDataPath()))
            {
                sb.Append(login);
                sb.Append(":");
                sb.Append(password);
                sb.Append(":");
                switch (role)
                {
                    case Roles.ADMIN: sb.Append("admin");
                        break;
                    default: sb.Append("user");
                        break;
                }
                sw.WriteLine(sb.ToString());
                sw.Flush();
            }
            return result;

        }

        public string removeUser(string login)
        {
            string result = "User successfully delete";
            StringBuilder stringBuilder = new StringBuilder();
            if (userExist(login))
            {
                if (dataProvider.getCurrentUser().Name.Equals(login))
                    return "Нельзя удалить свою учетную запись";

                string line = String.Empty;
                using (StreamReader sr = new StreamReader(dataProvider.getUserDataPath()))
                {
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line.Split(':')[0].Equals(login))
                            continue;
                        stringBuilder.Append(line + "\n");
                    }
                }
                File.Delete(dataProvider.getUserDataPath());
                File.Create(dataProvider.getUserDataPath()).Close();
                using (StreamWriter sw = new StreamWriter(dataProvider.getUserDataPath()))
                {
                    sw.Write(stringBuilder.ToString());
                    sw.Flush();
                }
            }
            else result = "User don't exist";
            return result;
        }

        public User tryLogin(String login, String password)
        {
            User result = null;
            string line = String.Empty;
            using (StreamReader sr = new StreamReader(dataProvider.getUserDataPath()))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Split(':')[0].Equals(login))
                        if(line.Split(':')[0].Equals(password))
                    { 
                        result = new User(line.Split(':')[0], line.Split(':')[2]);
                        break;
                    }
                }
            }

            return result;
        }

        public bool userExist(String login)
        {
            bool result = false;
            string line = String.Empty;
            using (StreamReader sr = new StreamReader(dataProvider.getUserDataPath()))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Split(':')[0].Equals(login))
                    {
                        result = true;
                        break;
                    }
                }
            }

                return result;
        }

    }
}
