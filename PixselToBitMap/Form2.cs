using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixselToBitMap
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int s = Program.fontBitMap.FirstSymbol; s <= Program.fontBitMap.LastSymbol; s++)
            {
                if (Program.fontBitMap.Is(s))
                {
                    Program.fontBitMap[s].Offset += (int)ChOffset.Value;
                }
            }

            
        }
    }
}
