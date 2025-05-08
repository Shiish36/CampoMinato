using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1
{
    public class Mine
    {
        private int _x;
        public int X
        {
            get { return _x; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                value = _x;
            }
        }
        private int _y;
        public int Y
        {
            get { return _y; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                value = _y;
            }
        }
        public Mine(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}