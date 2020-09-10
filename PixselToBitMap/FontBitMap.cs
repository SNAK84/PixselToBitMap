using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixselToBitMap
{
    class FontBitMap
    {
        int FirstSymbol;
        int LastSymbol;
        public string FontName;
        public int Advance = 9;
        public int Offset = 1;

        private Dictionary<int, SymbolBitMap> Symbols = new Dictionary<int, SymbolBitMap>(255);

        public bool Is(int Symbol) => Symbols.ContainsKey(Symbol);
        
        public SymbolBitMap this[int i]
        {
            get {
                if (!Symbols.ContainsKey(i))
                    Symbols.Add(i, new SymbolBitMap());
                return Symbols[i];
            }
            set
            {
                if (!Symbols.ContainsKey(i))
                    Symbols.Add(i, new SymbolBitMap());
                Symbols[i] = value;
            }
        }

        public FontBitMap()
        {
            FirstSymbol = 0x20;
            LastSymbol = 0x7E;
        }

        public FontBitMap(int First, int Last)
        {
            FirstSymbol = First;
            LastSymbol = Last;
        }

        public string GetTextFile()
        {

            string Bitmaps = "";
            string Glyphs = "";
            int BitCursor = 0;

            byte[][] BitMap = new byte[256][];

            for (int s = FirstSymbol; s <= LastSymbol; s++)
            {
                if (Symbols.ContainsKey(s) && Symbols[s].BitMap != null)
                {
                    int Double = FindDouble(s);
                    if (Double > -1)
                    {
                        Symbols[s].CursorBitMap = Symbols[Double].CursorBitMap;
                    }
                    else
                    {
                        Symbols[s].CursorBitMap = BitCursor;

                        if(Symbols[s].BitMap.Length>0) Bitmaps += ((BitCursor > 0) ? ",\n" : "");
                        for(int i=0;i< Symbols[s].BitMap.Length;i++)
                        {
                            if (i > 0) Bitmaps += ", ";
                            Bitmaps += "0x" + (Symbols[s].BitMap[i]<0x10?"0":"") + Symbols[s].BitMap[i].ToString("X");
                        }
                        //Bitmaps += ((BitCursor > 0) ? ",\n" : "") + "0x" + Symbols[s].BitMap.ToString("X"); //BitConverter.ToString(Symbols[s].BitMap).Replace("-", ", 0x");
                        BitCursor += Symbols[s].BitMap.Length;
                    }
                }

                Glyphs += 
                        "{ " + this[s].CursorBitMap +
                        ", " + this[s].Width +
                        ", " + this[s].Height +
                        ", " + (this[s].Width + Offset) +
                        ", 0, " + this[s].Offset * -1 +
                        "}"+ ((s == LastSymbol) ? "" : ",") + 
                        "\t// 0x" + s.ToString("X") + " '"+ Program.form1.FontChars[s] + "'\n";
                
            }

            string TextFile = "const uint8_t " + FontName + "Bitmaps[] PROGMEM = {\n" + Bitmaps + "\n};\n" +
                "const GFXglyph " + FontName + "Glyphs[] PROGMEM = {\n" + Glyphs + "\n};\n" +
                "const GFXfont " + FontName + " PROGMEM = {(uint8_t *)" + FontName + "Bitmaps,(GFXglyph *)" + FontName + "Glyphs, 0x" + FirstSymbol.ToString("X") + ",0x" + LastSymbol.ToString("X") + ", "+Advance+"};";

            return TextFile;
        }

        private int FindDouble(int Symbol)
        {
            for (int s = FirstSymbol; s < Symbol; s++)
            {
                if (Symbols.ContainsKey(s) && Symbols[s].BitMap != null && Symbols[s].BitMap.Length > 0)
                {
                    bool doub = true;
                    for (int i = 0; i < Symbols[s].BitMap.Length; i++)
                    {
                        if (Symbols[s].BitMap.Length != Symbols[Symbol].BitMap.Length)
                        {
                            doub = false;
                        }
                        else
                        {
                            if (Symbols[s].BitMap[i] != Symbols[Symbol].BitMap[i])
                                doub = false;
                        }
                    }
                    if(doub) return s;
                }
            }
            return -1;
        }

        public int GetMaxWidth()
        {
            int MaxWidth = 0;
            foreach(var sym in Symbols)
                if (sym.Value.Width > MaxWidth) MaxWidth = sym.Value.Width;
            return MaxWidth;
        }
        public int GetMaxHeight()
        {
            int MaxHeight = 0;
            foreach (var sym in Symbols)
                if (sym.Value.Height > MaxHeight) MaxHeight = sym.Value.Height;
            return MaxHeight;
        }
    }
}
