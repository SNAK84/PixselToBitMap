using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixselToBitMap
{
    static class Program
    {
        public static Form1 form1;
        public static FontBitMap fontBitMap;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.AddMessageFilter(new TestMessageFilter());

            form1 = new Form1();
            Application.Run(form1);
        }
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class TestMessageFilter : IMessageFilter
    {
        int i = 0;
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 513)
            {
                Program.form1.CurrentMouseButtons = MouseButtons.Left;
                if(Program.form1.CurrentMouseLabel!=null)
                Program.form1.SetColor(Program.form1.CurrentMouseLabel, true);
            }
            if (m.Msg == 514)
            {
                Program.form1.CurrentMouseButtons = MouseButtons.None;
            }

            if (m.Msg == 516)
            {
                Program.form1.CurrentMouseButtons = MouseButtons.Right;
                if (Program.form1.CurrentMouseLabel != null)
                    Program.form1.SetColor(Program.form1.CurrentMouseLabel, false);
            }
            if (m.Msg == 517)
            {
                Program.form1.CurrentMouseButtons = MouseButtons.None;
            }
            // Blocks all the messages relating to the left mouse button.
            if (m.Msg >= 513 && m.Msg <= 518)
            {
                if(Program.form1.MouseOverLabels) return true;
                return false;
            }
            return false;
        }
    }
}
