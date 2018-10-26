using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSDev.Controllers
{
    interface CommandController
    {
        void executeCommand(string command, RichTextBox output, TextBox input);
    }
}
