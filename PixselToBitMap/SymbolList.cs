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
    public partial class SymbolList : Form
    {
        static Panel[] panels = new Panel[256];
        Bitmap[] bitmap = new Bitmap[256];


        public SymbolList()
        {
            InitializeComponent();
        }

        private void SymbolList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void SymbolList_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                Label labelC = new Label();
                labelC.AutoSize = false;
                labelC.Location = new System.Drawing.Point(0, 0);
                labelC.Size = new System.Drawing.Size(32, 14);
                labelC.Text = i.ToString("X");
                labelC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                labelC.Margin = new System.Windows.Forms.Padding(0);

                labelC.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                tableLayoutPanel1.Controls.Add(labelC, i+1, 0);

                Label labelR = new Label();
                labelR.AutoSize = labelC.AutoSize;
                labelR.Location = new System.Drawing.Point(0, 0);
                labelR.Text = labelC.Text;
                labelR.Margin = new System.Windows.Forms.Padding(0);
                labelR.Size = new System.Drawing.Size(14, 32);
                labelR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(labelR, 0, i + 1);

            }
            int r = 1;
            int c = 1;
            for (int i = 0; i < 256; i++)
            {
                if (Program.fontBitMap[i].Width > 0 || Program.fontBitMap[i].Height > 0)
                {
                    panels[i] = new Panel();
                    panels[i].Location = new System.Drawing.Point(0, 0);
                    panels[i].Margin = new System.Windows.Forms.Padding(0);
                    panels[i].Size = new System.Drawing.Size(32, 32);
                    panels[i].Name = "Symbol" + i.ToString();
                    panels[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                    panels[i].Click += new System.EventHandler(this.CopySymbel);

                    tableLayoutPanel1.Controls.Add(panels[i], c, r);


                    int SizeX = 32 / Program.fontBitMap.GetMaxWidth();
                    int SizeY = 32 / Program.fontBitMap.GetMaxHeight();

                    int Size = (SizeX > SizeY) ? SizeY : SizeX;
                    bitmap[i] = new Bitmap(Program.fontBitMap.GetMaxWidth() * Size, Program.fontBitMap.GetMaxHeight() * Size);
                    Graphics g = Graphics.FromImage(bitmap[i]);

                    int x = 0;
                    int y = 0;
                    

                    int Pixel = 0;
                    for (int b = 0; b < Program.fontBitMap[i].BitMap.Length; b++)
                    {
                        for (byte bt = 0; bt < 8; bt++)
                        {
                            if (y >= Height || y >= Program.fontBitMap[i].Height || b >= Program.fontBitMap[i].BitMap.Length) continue;
                            int xp = x * Size;
                            int yp = y * Size;

                            if ((Program.fontBitMap[i].BitMap[b] & (byte)(0x80 >> bt)) == (byte)(0x80 >> bt))
                                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(xp, yp, Size, Size));
                            else
                                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(xp, yp, Size, Size));

                            Pixel++;

                            //e.DrawImage(bmp, new Point(10, 10));

                            x++;
                            if (x >= Width || x >= Program.fontBitMap[i].Width)
                            {
                                x = 0;
                                y++;
                            }
                        }
                    }

                    g.Flush();

                    panels[i].BackgroundImage = bitmap[i];

                }

                c++;
                if (c > 16)
                {
                    c = 1;
                    r++;
                }

            }
        }


        private void CopySymbel(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            int Sym = Convert.ToInt32(panel.Name.Replace("Symbol", ""));
            Program.form1.CopySymbel(Sym);
            this.Close();
        }
    }
}
