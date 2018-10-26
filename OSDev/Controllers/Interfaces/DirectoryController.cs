using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDev.Controllers
{
    interface DirectoryController
    {
        void deleteDirectory(string name);
        void renameDirectory(string oldName, string newName);
        void createDirectory(string name);
        List<string> getDirectoryContent(string path);
    }
}
