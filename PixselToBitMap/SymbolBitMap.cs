using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixselToBitMap
{
    class SymbolBitMap
    {

        byte[] _BitMap;
        public int Width;
        public int Height;
        public int Size;
        public int Offset;
        public int CursorBitMap;

        public SymbolBitMap()
        {
            _BitMap = null;
        }
        public byte this[int i]
        {
            get { return _BitMap[i]; }
            set { _BitMap[i] = value; }
        }

        public byte[] BitMap
        {
            get { return _BitMap; }
            set { _BitMap = value; }
        }


    }
}
