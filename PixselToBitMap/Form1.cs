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

namespace PixselToBitMap
{
    public partial class Form1 : Form
    {
        Label[,] labels = new Label[64, 64];
        int BitMapSize = 15;
        public int Width
        {
            get { return _Width; }
            set {
                WidthBox.Value = value;
              _Width = value; }
        }
        public int Height
        {
            get { return _Height; }
            set
            {
                HeightBox.Value = value; _Height = value; }
        }

        int _Width = 8;
        int _Height = 8;

        FontBitMap fontBitMap = new FontBitMap();

        public char[] FontChars = new char[256];



        public Form1()
        {
            InitializeComponent();
            CreateBitMap();

            FontChars[0] = '¿';
            FontChars[1] = '¿';
            FontChars[2] = '¿';
            FontChars[3] = '¿';
            FontChars[4] = '¿';
            FontChars[5] = '¿';
            FontChars[6] = '¿';
            FontChars[7] = '¿';
            FontChars[8] = '¿';
            FontChars[9] = '¿';
            FontChars[10] = '¿';
            FontChars[11] = '¿';
            FontChars[12] = '¿';
            FontChars[13] = '¿';
            FontChars[14] = '¿';
            FontChars[15] = '¿';
            FontChars[16] = '¿';
            FontChars[17] = '¿';
            FontChars[18] = '¿';
            FontChars[19] = '¿';
            FontChars[20] = '¿';
            FontChars[21] = '¿';
            FontChars[22] = '¿';
            FontChars[23] = '¿';
            FontChars[24] = '¿';
            FontChars[25] = '¿';
            FontChars[26] = '¿';
            FontChars[27] = '¿';
            FontChars[28] = '¿';
            FontChars[29] = '¿';
            FontChars[30] = '¿';
            FontChars[31] = '¿';
            FontChars[32] = ' ';
            FontChars[33] = '!';
            FontChars[34] = '"';
            FontChars[35] = '#';
            FontChars[36] = '$';
            FontChars[37] = '%';
            FontChars[38] = '&';
            FontChars[39] = '¿';
            FontChars[40] = '(';
            FontChars[41] = ')';
            FontChars[42] = '*';
            FontChars[43] = '+';
            FontChars[44] = ',';
            FontChars[45] = '-';
            FontChars[46] = '.';
            FontChars[47] = '/';
            FontChars[48] = '0';
            FontChars[49] = '1';
            FontChars[50] = '2';
            FontChars[51] = '3';
            FontChars[52] = '4';
            FontChars[53] = '5';
            FontChars[54] = '6';
            FontChars[55] = '7';
            FontChars[56] = '8';
            FontChars[57] = '9';
            FontChars[58] = ':';
            FontChars[59] = ';';
            FontChars[60] = '<';
            FontChars[61] = '=';
            FontChars[62] = '>';
            FontChars[63] = '?';
            FontChars[64] = '@';
            FontChars[65] = 'A';
            FontChars[66] = 'B';
            FontChars[67] = 'C';
            FontChars[68] = 'D';
            FontChars[69] = 'E';
            FontChars[70] = 'F';
            FontChars[71] = 'G';
            FontChars[72] = 'H';
            FontChars[73] = 'I';
            FontChars[74] = 'J';
            FontChars[75] = 'K';
            FontChars[76] = 'L';
            FontChars[77] = 'M';
            FontChars[78] = 'N';
            FontChars[79] = 'O';
            FontChars[80] = 'P';
            FontChars[81] = 'Q';
            FontChars[82] = 'R';
            FontChars[83] = 'S';
            FontChars[84] = 'T';
            FontChars[85] = 'U';
            FontChars[86] = 'V';
            FontChars[87] = 'W';
            FontChars[88] = 'X';
            FontChars[89] = 'Y';
            FontChars[90] = 'Z';
            FontChars[91] = '[';
            FontChars[92] = '\\';
            FontChars[93] = ']';
            FontChars[94] = '^';
            FontChars[95] = '_';
            FontChars[96] = '`';
            FontChars[97] = '{';
            FontChars[98] = '|';
            FontChars[99] = '}';
            FontChars[100] = '~';
            FontChars[101] = 'a';
            FontChars[102] = 'b';
            FontChars[103] = 'c';
            FontChars[104] = 'd';
            FontChars[105] = 'e';
            FontChars[106] = 'f';
            FontChars[107] = 'g';
            FontChars[108] = 'h';
            FontChars[109] = 'i';
            FontChars[110] = 'j';
            FontChars[111] = 'k';
            FontChars[112] = 'l';
            FontChars[113] = 'm';
            FontChars[114] = 'n';
            FontChars[115] = 'o';
            FontChars[116] = 'p';
            FontChars[117] = 'q';
            FontChars[118] = 'r';
            FontChars[119] = 's';
            FontChars[120] = 't';
            FontChars[121] = 'u';
            FontChars[122] = 'v';
            FontChars[123] = 'w';
            FontChars[124] = 'x';
            FontChars[125] = 'y';
            FontChars[126] = 'z';
            FontChars[127] = '¿';
            FontChars[128] = '¿';
            FontChars[129] = '¿';
            FontChars[130] = '¿';
            FontChars[131] = '¿';
            FontChars[132] = '¿';
            FontChars[133] = '¿';
            FontChars[134] = '¿';
            FontChars[135] = '¿';
            FontChars[136] = '¿';
            FontChars[137] = '¿';
            FontChars[138] = '¿';
            FontChars[139] = '¿';
            FontChars[140] = '¿';
            FontChars[141] = '¿';
            FontChars[142] = '¿';
            FontChars[143] = '¿';
            FontChars[144] = '¿';
            FontChars[145] = '¿';
            FontChars[146] = '¿';
            FontChars[147] = '¿';
            FontChars[148] = '¿';
            FontChars[149] = '¿';
            FontChars[150] = '¿';
            FontChars[151] = '¿';
            FontChars[152] = '¿';
            FontChars[153] = '¿';
            FontChars[154] = '¿';
            FontChars[155] = '¿';
            FontChars[156] = '¿';
            FontChars[157] = '¿';
            FontChars[158] = '¿';
            FontChars[159] = '¿';
            FontChars[160] = '¿';
            FontChars[161] = '¿';
            FontChars[162] = '¿';
            FontChars[163] = '¿';
            FontChars[164] = '¿';
            FontChars[165] = '¿';
            FontChars[166] = '¿';
            FontChars[167] = '¿';
            FontChars[168] = 'Ё';
            FontChars[169] = '¿';
            FontChars[170] = '¿';
            FontChars[171] = '¿';
            FontChars[172] = '¿';
            FontChars[173] = '¿';
            FontChars[174] = '¿';
            FontChars[175] = '¿';
            FontChars[176] = '°';
            FontChars[177] = '¿';
            FontChars[178] = '¿';
            FontChars[179] = '¿';
            FontChars[180] = '¿';
            FontChars[181] = '¿';
            FontChars[182] = '¿';
            FontChars[183] = '¿';
            FontChars[184] = 'ё';
            FontChars[185] = '№';
            FontChars[186] = '¿';
            FontChars[187] = '¿';
            FontChars[188] = '¿';
            FontChars[189] = '¿';
            FontChars[190] = '¿';
            FontChars[191] = '¿';
            FontChars[192] = 'А';
            FontChars[193] = 'Б';
            FontChars[194] = 'В';
            FontChars[195] = 'Г';
            FontChars[196] = 'Д';
            FontChars[197] = 'Е';
            FontChars[198] = 'Ж';
            FontChars[199] = 'З';
            FontChars[200] = 'И';
            FontChars[201] = 'Й';
            FontChars[202] = 'К';
            FontChars[203] = 'Л';
            FontChars[204] = 'М';
            FontChars[205] = 'Н';
            FontChars[206] = 'О';
            FontChars[207] = 'П';
            FontChars[208] = 'Р';
            FontChars[209] = 'С';
            FontChars[210] = 'Т';
            FontChars[211] = 'У';
            FontChars[212] = 'Ф';
            FontChars[213] = 'Х';
            FontChars[214] = 'Ц';
            FontChars[215] = 'Ч';
            FontChars[216] = 'Ш';
            FontChars[217] = 'Щ';
            FontChars[218] = 'Ъ';
            FontChars[219] = 'Ы';
            FontChars[220] = 'Ь';
            FontChars[221] = 'Э';
            FontChars[222] = 'Ю';
            FontChars[223] = 'Я';
            FontChars[224] = 'а';
            FontChars[225] = 'б';
            FontChars[226] = 'в';
            FontChars[227] = 'г';
            FontChars[228] = 'д';
            FontChars[229] = 'е';
            FontChars[230] = 'ж';
            FontChars[231] = 'з';
            FontChars[232] = 'и';
            FontChars[233] = 'й';
            FontChars[234] = 'к';
            FontChars[235] = 'л';
            FontChars[236] = 'м';
            FontChars[237] = 'н';
            FontChars[238] = 'о';
            FontChars[239] = 'п';
            FontChars[240] = 'р';
            FontChars[241] = 'с';
            FontChars[242] = 'т';
            FontChars[243] = 'у';
            FontChars[244] = 'ф';
            FontChars[245] = 'х';
            FontChars[246] = 'ц';
            FontChars[247] = 'ч';
            FontChars[248] = 'ш';
            FontChars[249] = 'щ';
            FontChars[250] = 'ъ';
            FontChars[251] = 'ы';
            FontChars[252] = 'ь';
            FontChars[253] = 'э';
            FontChars[254] = 'ю';
            FontChars[255] = 'я';


        }
        public void CreateBitMap()
        {
            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    if (y < Height && x < Width)
                        CreateMyLabel(x, y);
                    else
                        panel1.Controls.Remove(labels[x, y]);
                }
            }
        }

        public void CreateMyLabel(int x, int y)
        {
            if (panel1.Controls.Contains(labels[x, y])) return;

            // Create an instance of a Label.
            labels[x, y] = new Label();
            labels[x, y].AutoSize = false;
            labels[x, y].BackColor = System.Drawing.Color.White;
            labels[x, y].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labels[x, y].Location = new System.Drawing.Point((BitMapSize - 1) * x, (BitMapSize - 1) * y);
            labels[x, y].Name = "label" + x + y;
            labels[x, y].Size = new System.Drawing.Size(BitMapSize, BitMapSize);
            labels[x, y].TabIndex = 1;
            labels[x, y].Text = "";
            labels[x, y].Click += new System.EventHandler(this.ChColor);
            panel1.Controls.Add(labels[x, y]);

        }

        private void ChColor(object sender, EventArgs e)
        {

            Label Lab = (Label)sender;

            if (Lab.BackColor == Color.White)
                Lab.BackColor = Color.Black;
            else
                Lab.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Width = (int)WidthBox.Value;
            Height = (int)HeightBox.Value;
            CreateBitMap();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Height_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            int bytes = (int)Math.Ceiling(((decimal)Width * Height / 8));

            byte[] BitMap = new byte[bytes];

            int CurRow = 0;
            int x = 0;
            int y = 0;
            for (int b = 0; b < bytes; b++)
            {
                for (byte bt = 0; bt < 8; bt++)
                {
                    if (y >= Height) continue;
                    if (GetBit(x, y))
                        BitMap[b] |= (byte)(0x80 >> bt);
                    x++;
                    if (x >= Width)
                    {
                        x = 0;
                        y++;
                    }
                }
            }

            string FileStringByte = "0x" + BitConverter.ToString(BitMap).Replace("-", ", 0x");
            textBox1.Text = FileStringByte;

        }
        private bool GetBit(int x, int y)
        {
            int z = x;
            int a = y;
            return labels[x, y].BackColor == Color.Black;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearLabels();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = FontChars[(int)numericUpDown1.Value].ToString();
            SelFont.Text = label4.Font.Name;
            fontBitMap.FontName = textBox2.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label4.Text = FontChars[(int)numericUpDown1.Value].ToString();
            GetBitmaxToLabels((int)numericUpDown1.Value);
        }

        private void GetBitmaxSymbol(int Symbol)
        {

            var bitmap = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bitmap);

            RectangleF rectf = new RectangleF(0, 0, Width, Height);
            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(FontChars[Symbol].ToString(), new Font(SelFont.Text, Height - 1), Brushes.Black, rectf, format);

            g.Flush();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {

                    Color pixel = bitmap.GetPixel(x, y);
                    String bb = pixel.ToString();
                    int zz = pixel.ToArgb();
                    
                    labels[x, y].BackColor = (pixel.ToArgb() == 0) ? Color.White : Color.Black;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetBitmaxSymbol((int)numericUpDown1.Value);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();

            fontDialog1.ShowColor = true;

            Font font = new Font(SelFont.Text, Height, fontDialog1.Font.Style, fontDialog1.Font.Unit);

            fontDialog1.Font = font;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                SelFont.Text = fontDialog1.Font.Name;
                label4.Font = new Font(SelFont.Text, label4.Font.Size, label4.Font.Style, label4.Font.Unit);
                Width = (int)fontDialog1.Font.Size;
                Height = (int)fontDialog1.Font.Size;
                WidthBox.Value = (int)fontDialog1.Font.Size;
                HeightBox.Value = (int)fontDialog1.Font.Size;

                CreateBitMap();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int bytes = (int)Math.Ceiling(((decimal)Width * Height / 8));

            byte[] BitMap = new byte[bytes];

            int x = 0;
            int y = 0;
            for (int b = 0; b < bytes; b++)
            {
                for (byte bt = 0; bt < 8; bt++)
                {
                    if (y >= Height) continue;
                    if (GetBit(x, y))
                        BitMap[b] |= (byte)(0x80 >> bt);
                    x++;
                    if (x >= Width)
                    {
                        x = 0;
                        y++;
                    }
                }
            }

            fontBitMap[(int)numericUpDown1.Value].Width = Width;
            fontBitMap[(int)numericUpDown1.Value].Height = Height;
            fontBitMap[(int)numericUpDown1.Value].Size = Width + 1;
            fontBitMap[(int)numericUpDown1.Value].Offset = (int)Offset.Value;
            fontBitMap[(int)numericUpDown1.Value].BitMap = BitMap;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GetBitmaxToLabels((int)numericUpDown1.Value);
        }

        private void GetBitmaxToLabels(int Symbol)
        {

            byte[] BitMap = fontBitMap[Symbol].BitMap;
            
            if (BitMap == null)
            {
                ClearLabels();
                return;
            }
            int x = 0;
            int y = 0;

            Width = fontBitMap[Symbol].Width;
            Height = fontBitMap[Symbol].Height;
            CreateBitMap();

            for (int b = 0; b < fontBitMap[Symbol].Size; b++)
            {
                for (byte bt = 0; bt < 8; bt++)
                {
                    if (y >= Height) continue;

                    if ((BitMap[b] & (byte)(0x80 >> bt)) == (byte)(0x80 >> bt))
                        labels[x, y].BackColor = Color.Black;
                    else labels[x, y].BackColor = Color.White;
                    x++;
                    if (x >= Width)
                    {
                        x = 0;
                        y++;
                    }
                }
            }
        }

        private void ClearLabels()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    labels[x, y].BackColor = Color.White;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //string Text = fontBitMap.GetTextFile();

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "C/C++ Header File (*.h)|*.h|Plain Text File (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, fontBitMap.GetTextFile());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            fontBitMap.FontName = textBox2.Text;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            fontBitMap.Advance = (int)numericUpDown2.Value;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GetAllBitmaxSymbol();
            GetBitmaxToLabels((int)numericUpDown1.Value);
        }

        private void GetAllBitmaxSymbol()
        {
            for (int s = (int)FirstSymbol.Value; s <= (int)LastSymbol.Value; s++)
            {
                Bitmap bitmap = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(bitmap);


                string ss = FontChars[s].ToString();
                g.DrawString(ss,
                    new Font(SelFont.Text, Height - 1),
                    Brushes.Black,
                    new RectangleF(0, 0, Width, Height),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                g.Flush();

                int bytes = (int)Math.Ceiling(((decimal)Width * Height / 8));

                byte[] BitMap = new byte[bytes];

                int x = 0;
                int y = 0;
                for (int b = 0; b < bytes; b++)
                {
                    for (byte bt = 0; bt < 8; bt++)
                    {
                        if (y >= Height) continue;
                        Color pixel = bitmap.GetPixel(x, y);
                        if ((pixel.ToArgb() != 0))
                            BitMap[b] |= (byte)(0x80 >> bt);
                        x++;
                        if (x >= Width)
                        {
                            x = 0;
                            y++;
                        }
                    }
                }

                fontBitMap[s].Width = Width;
                fontBitMap[s].Height = Height;
                fontBitMap[s].Size = Width + 1;
                fontBitMap[s].Offset = (int)Offset.Value;
                fontBitMap[s].BitMap = BitMap;
            }
        }

        private void panel1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Update the drawing based upon the mouse wheel scrolling.

            int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            int numberOfPixelsToMove = numberOfTextLinesToMove /3;

            if (numberOfPixelsToMove != 0)
            {
                BitMapSize += numberOfPixelsToMove;

                if (BitMapSize < 5) BitMapSize = 5;
                ResizeBitMap();
            }
            panel1.Invalidate();
        }

        public void ResizeBitMap()
        {
            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    if (!panel1.Controls.Contains(labels[x, y])) continue;
                    labels[x, y].Location = new System.Drawing.Point((BitMapSize - 1) * x, (BitMapSize - 1) * y);
                    labels[x, y].Size = new System.Drawing.Size(BitMapSize, BitMapSize);
                }
            }
        }
    }
}
