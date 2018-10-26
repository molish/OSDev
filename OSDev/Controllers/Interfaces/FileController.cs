using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSDev.Controllers.Interfaces
{
    interface FileController
    {
        void openFile(string name, RichTextBox output);
        void createFile(string name);
        void deleteFile(string name);
        void renameFile(string oldName, string newName);
        void writeFile(string name, string text);
    }
}
