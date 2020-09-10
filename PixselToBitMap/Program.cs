using System;
using System.Collections.Generic;
using System.Linq;
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
            

            form1 = new Form1();
            Application.Run(form1);
        }
    }
}
