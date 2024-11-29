using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGetter.View
{
    public class Ui
    {
        private RichTextBox _msgBox;
        public RichTextBox MsgBox { get { return _msgBox; } set { _msgBox = value; } }

        public void SetSysMessage(string message)
        {
            MsgBox.Text += ((MsgBox.Text == null || MsgBox.Text.Length == 0) ? "" : "\n") + message + " - " + DateTime.Now;
        }
        public void SetSysMessage(RichTextBox box, string message)
        {
            box.Text += ((box.Text == null || box.Text.Length == 0) ? "" : "\n") + message + " - " + DateTime.Now;
        }
        
    }
}
