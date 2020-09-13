using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PixselToBitMap
{
    public partial class Form1 : Form
    {
        public MouseButtons CurrentMouseButtons;
        public Label CurrentMouseLabel;
        Label[,] labels = new Label[64, 64];
        int BitMapSize = 15;
        public new int Width
        {
            get { return _Width; }
            set {
                //PreviewSizeReCalc();
                WidthBox.Value = value;
              _Width = value; }
        }
        public new int Height
        {
            get { return _Height; }
            set
            {
                //PreviewSizeReCalc();
                HeightBox.Value = value; _Height = value; }
        }

        int _Width = 8;
        int _Height = 8;

        public string FontChars = "¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿ !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~¿ЂЃ‚ѓ„…†‡€‰Љ‹ЊЌЋЏђ‘’“”•–— ™љ›њќћџ ЎўЈ¤Ґ¦§Ё©Є«¬ ®Ї°±Ііґµ¶·ё№є»јЅѕїАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзийклмнопрстуфхцчшщъыьэюя";

        bool NotInsertToBitMap = false;


        public Form1()
        {
            InitializeComponent();
            CreateBitMap();
        }
        public void CreateBitMap()
        {
            panel1.Visible = false;
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
            panel1.Visible = true;
            PreviewPrint();
        }
        public bool MouseOverLabels;
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
            /*labels[x, y].MouseEnter += new System.EventHandler(this.labels_MouseEnter);*/
            labels[x, y].MouseLeave += new System.EventHandler(this.OnMouseOverLabels);
            labels[x, y].MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);

            panel1.Controls.Add(labels[x, y]);
        }

        private void OnMouseMove(object sender, EventArgs e)
        {
            MouseOverLabels = true;
            CurrentMouseLabel = (Label)sender;
            if (CurrentMouseButtons == MouseButtons.Left)
            {
                SetColor((Label)sender, true);
            }
            if (CurrentMouseButtons == MouseButtons.Right)
            {
                SetColor((Label)sender, false);
            }
        }
        private void OnMouseOverLabels(object sender, EventArgs e)
        {
            CurrentMouseLabel = (Label)sender;
            MouseOverLabels = true;
        }
        private void ChColor(object sender, EventArgs e)
        {
            if (CurrentMouseButtons == MouseButtons.Left)
            {
                SetColor((Label)sender, true);
            }
            if (CurrentMouseButtons == MouseButtons.Right)
            {
                SetColor((Label)sender, false);
            }
            //ChColor((Label)sender);
        }
        private void ChColor(Label Lab)
        {
            //Label Lab = (Label)sender;

            if (Lab.BackColor == Color.White)
                SetColor(Lab, true);
                //Lab.BackColor = Color.Black;
            else
                SetColor(Lab, false);
            //Lab.BackColor = Color.White;

            
        }

        public void SetColor(Label Lab, bool black)
        {
            if (black)
                Lab.BackColor = Color.Black;
            else
                Lab.BackColor = Color.White;

            NotInsertToBitMap = false;

            if (checkBox3.Checked)
                InsertToBitMap();

            PreviewPrint();
        }
        private void labels_MouseEnter(object sender, EventArgs e)
        {
            if (CurrentMouseButtons == MouseButtons.Left)
                ChColor((Label)sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Width = (int)WidthBox.Value;
            Height = (int)HeightBox.Value;
            CreateBitMap();

            if (checkBox3.Checked)
                InsertToBitMap();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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
            Program.fontBitMap = new FontBitMap((int)FirstSymbol.Value, (int)LastSymbol.Value);

            label4.Text = FontChars[(int)numericUpDown1.Value].ToString();
            SelFont.Text = label4.Font.Name;
            Program.fontBitMap.FontName = textBox2.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NotInsertToBitMap = true;
            label4.Text = FontChars[(int)numericUpDown1.Value].ToString();
            GetBitmaxToLabels((int)numericUpDown1.Value);

            timer1.Enabled = true;
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

            g.DrawString(FontChars[Symbol].ToString(), new Font(SelFont.Text, Height), Brushes.Black, rectf, format);

            g.Flush();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {

                    Color pixel = bitmap.GetPixel(x, y);
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
            InsertToBitMap();
        }

        public void InsertToBitMap()
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

            Program.fontBitMap[(int)numericUpDown1.Value].Width = Width;
            Program.fontBitMap[(int)numericUpDown1.Value].Height = Height;
            Program.fontBitMap[(int)numericUpDown1.Value].Size = Width + 1;
            Program.fontBitMap[(int)numericUpDown1.Value].Offset = (int)Offset.Value;
            Program.fontBitMap[(int)numericUpDown1.Value].BitMap = BitMap;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            GetBitmaxToLabels((int)numericUpDown1.Value);
        }

        private void GetBitmaxToLabels(int Symbol)
        {

            byte[] BitMap = Program.fontBitMap[Symbol].BitMap;

            if (BitMap == null)
            {
                ClearLabels();
                return;
            }
            int x = 0;
            int y = 0;

            if (!checkBox1.Checked)
            {
                Width = Program.fontBitMap[Symbol].Width;
                Height = Program.fontBitMap[Symbol].Height;
            }
            if (!checkBox2.Checked)
            {
                Offset.Value = Program.fontBitMap[Symbol].Offset;
            }
            CreateBitMap();
            ClearLabels();

            for (int b = 0; b < Program.fontBitMap[Symbol].BitMap.Length; b++)
            {
                for (byte bt = 0; bt < 8; bt++)
                {
                    if (y >= Height|| y >= Program.fontBitMap[Symbol].Height || b>= BitMap.Length) continue;

                    if ((BitMap[b] & (byte)(0x80 >> bt)) == (byte)(0x80 >> bt))
                        labels[x, y].BackColor = Color.Black;
                    else labels[x, y].BackColor = Color.White;
                    x++;
                    if (x >= Width || x >= Program.fontBitMap[Symbol].Width)
                    {
                        x = 0;
                        y++;
                    }
                }
            }
            PreviewPrint();
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

            saveFileDialog.FileName = textBox2.Text;
            saveFileDialog.Filter = "C/C++ Header File (*.h)|*.h|Plain Text File (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, Program.fontBitMap.GetTextFile());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Program.fontBitMap.FontName = textBox2.Text;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Program.fontBitMap.Advance = (int)numericUpDown2.Value;
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
                Program.fontBitMap[s].BitMap = new byte[bytes];

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

                Program.fontBitMap[s].Width = Width;
                Program.fontBitMap[s].Height = Height;
                Program.fontBitMap[s].Size = Width + 1;
                Program.fontBitMap[s].Offset = (int)Offset.Value;
                Program.fontBitMap[s].BitMap = BitMap;
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

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "C/C++ Header File (*.h)|*.h|Plain Text File (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string BitmapText = File.ReadAllText(openFileDialog.FileName);
                parseBitmapText(BitmapText);
                GetBitmaxToLabels((int)numericUpDown1.Value);
            }
        }
        private void parseBitmapText(string BitmapText)
        {
            #region Удаление Коментариев
            Regex regex = new Regex(@"//(.+)\n");
            BitmapText = regex.Replace(BitmapText, "");
            BitmapText = BitmapText.Replace("\t", "").Replace("\r","");
            /*
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    BitmapText = BitmapText.Replace(match.Value, "");
                }
            }*/
            #endregion

                BitmapText = BitmapText.Replace(" ", "");
                BitmapText = BitmapText.Replace("\n", "");
                int StartIndex1 = BitmapText.IndexOf("={")+2;
                int EndIndex1 = BitmapText.IndexOf("};", StartIndex1);
                int StartIndex2 = BitmapText.IndexOf("={", EndIndex1) + 2;
                int EndIndex2 = BitmapText.IndexOf("};", StartIndex2);
                int StartIndex3 = BitmapText.IndexOf("={", EndIndex2) + 2;
                int EndIndex3 = BitmapText.IndexOf("};", StartIndex3);

                string bt = BitmapText.Substring(StartIndex1, EndIndex1 - StartIndex1).Replace("0x", "").Replace(",", "");
                string sm = BitmapText.Substring(StartIndex2, EndIndex2 - StartIndex2);
                string fn = BitmapText.Substring(StartIndex3, EndIndex3 - StartIndex3);

                Dictionary<string, byte> hexindex = new Dictionary<string, byte>();
                for (int i = 0; i <= 255; i++)
                    hexindex.Add(i.ToString("X2"), (byte)i);
                List<byte> hexres = new List<byte>();
                for (int i = 0; i < bt.Length; i += 2)
                    hexres.Add(hexindex[bt.Substring(i, 2)]);
                byte[] BitMaps = hexres.ToArray();

                string[] fns = fn.Split(',');
                int First = hexindex[fns[2].Replace("0x", "")];
                int Last = hexindex[fns[3].Replace("0x", "")];
                int Offset = Convert.ToInt32(fns[4]);
            FirstSymbol.Value = First;
            LastSymbol.Value = Last;
            Program.fontBitMap = new FontBitMap(First, Last);
            numericUpDown2.Value = Offset;
            textBox2.Text = fns[0].Replace("(uint8_t*)", "").Replace("Bitmaps", "");
                string[] stringSeparators = new string[] { "},{" };
                string[] sms = sm.Split(stringSeparators, StringSplitOptions.None);

                for (int i = 0; i< sms.Length; i++)
                {
                    sms[i] = sms[i].Replace("{", "").Replace("}", "");
                    string[] smsp = sms[i].Split(',');
                    Program.fontBitMap[i + First].CursorBitMap = Convert.ToInt32(smsp[0]);
                    Program.fontBitMap[i + First].Width = Convert.ToInt32(smsp[1]);
                    Program.fontBitMap[i + First].Height = Convert.ToInt32(smsp[2]);
                    Program.fontBitMap[i + First].Size = Convert.ToInt32(smsp[3]);
                    Program.fontBitMap[i + First].Offset = Convert.ToInt32(smsp[5]) * -1;

                    int CountByte = (int)Math.Ceiling((decimal)Program.fontBitMap[i + First].Width * (decimal)Program.fontBitMap[i + First].Height / 8);
                    Program.fontBitMap[i + First].BitMap = new byte[CountByte];
                    Array.Copy(BitMaps, Program.fontBitMap[i + First].CursorBitMap, Program.fontBitMap[i + First].BitMap, 0, CountByte);
                    

                }
        }

        private void Offset_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked && !NotInsertToBitMap)
                InsertToBitMap();

            PreviewPrint();
        }

        private void WidthBox_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked && !NotInsertToBitMap)
                InsertToBitMap();
        }

        private void Height_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked && !NotInsertToBitMap)
                InsertToBitMap();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SymbolList symbolList = new SymbolList();
            symbolList.ShowDialog();
        }

        public void CopySymbel(int Sym)
        {
            NotInsertToBitMap = true;
            timer1.Enabled = true;

            Program.fontBitMap[(int)numericUpDown1.Value].BitMap = Program.fontBitMap[Sym].BitMap;
            Program.fontBitMap[(int)numericUpDown1.Value].CursorBitMap = Program.fontBitMap[Sym].CursorBitMap;
            Program.fontBitMap[(int)numericUpDown1.Value].Width = Program.fontBitMap[Sym].Width;
            Program.fontBitMap[(int)numericUpDown1.Value].Height = Program.fontBitMap[Sym].Height;
            Program.fontBitMap[(int)numericUpDown1.Value].Offset = Program.fontBitMap[Sym].Offset;
            Program.fontBitMap[(int)numericUpDown1.Value].Size = Program.fontBitMap[Sym].Size;
            GetBitmaxToLabels((int)numericUpDown1.Value);
        }

        private void OffsetX_ValueChanged(object sender, EventArgs e)
        {
            Program.fontBitMap.Offset = (int)OffsetX.Value;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentMouseLabel = null;
            MouseOverLabels = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentMouseLabel = null;
            MouseOverLabels = false;
        }

        private void LastSymbol_ValueChanged(object sender, EventArgs e)
        {
            Program.fontBitMap.LastSymbol = (int)LastSymbol.Value;
        }

        private void FirstSymbol_ValueChanged(object sender, EventArgs e)
        {
            Program.fontBitMap.FirstSymbol = (int)FirstSymbol.Value;
        }

        private void button12_Click(object sender, EventArgs e)
        {

            int Moving = 0;
            bool FullNullX = true;
            for (int y = 0; y < Height; y++)
            {
                if (!FullNullX) continue;
                for (int x = 0; x < Width; x++)
                {
                    if (GetBit(x, y))
                    {
                        FullNullX = false;
                    }
                }
                if (FullNullX)
                {
                    Moving++;
                }
            }
            if (Moving > 0)
            {
                for (int i = 0; i < Moving; i++)
                    MoveUp();
                HeightBox.Value -= Moving;
                Width = (int)WidthBox.Value;
                Height = (int)HeightBox.Value;
                CreateBitMap();
            }

            for (int y = 0; y < Height; y++)
            {
                FullNullX = true;
                for (int x = 0; x < Width; x++)
                {
                    if (GetBit(x, y))
                    {
                        FullNullX = false;
                    }
                }
                if (FullNullX)
                {
                    HeightBox.Value--;
                }
            }
            if((int)HeightBox.Value != Height)
            {
                Width = (int)WidthBox.Value;
                Height = (int)HeightBox.Value;
                CreateBitMap();
            }





            Moving = 0;
            bool FullNullY = true;
            for (int x = 0; x < Width; x++)
            {
                if (!FullNullY) continue;
                for (int y = 0; y < Height; y++)
                {
                    if (GetBit(x, y))
                    {
                        FullNullY = false;
                    }
                }
                if (FullNullY)
                {
                    Moving++;
                }
            }

            if (Moving > 0)
            {
                for (int i = 0; i < Moving; i++)
                    MoveLeft();
                WidthBox.Value -= Moving;
                Width = (int)WidthBox.Value;
                Height = (int)HeightBox.Value;
                CreateBitMap();
            }

            for (int x = 0; x < Width; x++)
            {
             FullNullY = true;
                for (int y = 0; y < Height; y++)
                {
                    if (GetBit(x, y))
                    {
                        FullNullY = false;
                    }
                }
                if (FullNullY)
                {
                    WidthBox.Value--;
                }
            }
            if ((int)WidthBox.Value != Width)
            {
                Width = (int)WidthBox.Value;
                Height = (int)HeightBox.Value;
                CreateBitMap();
            }

            if (checkBox3.Checked)
                InsertToBitMap();
        }
        private void MoveUp()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (y == Height - 1)
                        labels[x, y].BackColor = Color.White;
                    else
                        labels[x, y].BackColor = labels[x, y+1].BackColor;
                }
            }
        }
        private void MoveDown()
        {
            for (int y = Height-1; y >=0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (y == 0)
                        labels[x, y].BackColor = Color.White;
                    else
                        labels[x, y].BackColor = labels[x, y - 1].BackColor;
                }
            }
        }
        private void MoveLeft()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (x == Width-1)
                        labels[x, y].BackColor = Color.White;
                    else
                        labels[x, y].BackColor = labels[x+1, y].BackColor;
                }
            }
           

            if (checkBox3.Checked)
                InsertToBitMap();
        }

        private void MoveRight()
        {
            for (int x = (Width-1); x >=0; x--)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (x == 0)
                        labels[x, y].BackColor = Color.White;
                    else
                        labels[x, y].BackColor = labels[x - 1, y].BackColor;
                }
            }


            if (checkBox3.Checked)
                InsertToBitMap();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MoveLeft();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MoveUp();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MoveRight();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MoveDown();
        }

        private void PreviewPrint()
        {
            if(!PreviewSizeChengeFun) PreviewSizeReCalc();
            if (!PreviewEnable.Checked) return;

            int FW = Program.fontBitMap.GetMaxWidth();
            int FH = Program.fontBitMap.GetMaxHeight();

            int PS = (int)PreviewSize.Value;
            int SizeX = PS / ((FW > 0) ? FW : Width);
            int SizeY = PS / ((FH > 0) ? FH : Height);

            int Size = (SizeX > SizeY) ? SizeY : SizeX;
            //int Size = (int)PreviewSize.Value;

            int bytes = (int)Math.Ceiling(((decimal)Width * Height / 8));

            Bitmap bitmap = new Bitmap(PS, PS);
            Graphics g = Graphics.FromImage(bitmap);

            int offset = ((int)Offset.Value - 6) * -1 * Size;
            int x = 0;
            int y = 0;
            for (int b = 0; b < bytes; b++)
            {
                for (byte bt = 0; bt < 8; bt++)
                {
                    if (y >= Height) continue;
                    int xp = x * Size;
                    int yp = y * Size + offset;
                    if (GetBit(x, y))
                        g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(xp, yp, Size, Size));
                    else
                        g.FillRectangle(new SolidBrush(Color.White), new Rectangle(xp, yp, Size, Size));
                    x++;
                    if (x >= Width)
                    {
                        x = 0;
                        y++;
                    }
                }
            }

            g.Flush();
            Preview.BackgroundImage = bitmap;

        }
        bool PreviewSizeReCalcFun = false;
        bool PreviewSizeChengeFun = false;
        private void PreviewSize_ValueChanged(object sender, EventArgs e)
        {
            if (PreviewSizeReCalcFun) return;
            Preview.Width = (int)PreviewSize.Value+2;
            Preview.Height = (int)PreviewSize.Value+2;
            PreviewSizeChengeFun = true;
            PreviewPrint();
            PreviewSizeChengeFun = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            PreviewPrint();
        }

        private void PreviewEnable_CheckedChanged(object sender, EventArgs e)
        {
            PreviewPrint();
        }

        private void PreviewSizeReCalc()
        {
            if (Program.fontBitMap == null) return;
            PreviewSizeReCalcFun = true;

            int FW = Program.fontBitMap.GetMaxWidth();
            int FH = Program.fontBitMap.GetMaxHeight();

            int SizeX = ((FW > 0) ? FW : Width);
            int SizeY = ((FW > 0) ? FH : Height);

            int Size = (SizeX > SizeY) ? SizeX : SizeY;
            PreviewSize.Maximum = 0;
            do
            {
                PreviewSize.Maximum += Size;
            } while (PreviewSize.Maximum < 128);
            PreviewSize.Maximum -= Size;

            PreviewSize.Minimum = Size;
            PreviewSize.Value = PreviewSize.Maximum;

            PreviewSize.Increment = Size;
            PreviewSizeReCalcFun = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NotInsertToBitMap = false;
            timer1.Enabled = false;
        }
    }


}
