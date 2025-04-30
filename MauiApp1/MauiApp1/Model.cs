using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1
{
    public class Model
    {
        private int _lunghezzaCampo;
        public int LunghezzaCampo
        {
            get { return _lunghezzaCampo; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                value = _lunghezzaCampo;
            }
        }
        private int _larghezzaCampo;
        public int LarghezzaCampo
        {
            get { return _larghezzaCampo; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                value = _larghezzaCampo;
            }
        }
        private int _quantitàMine;
        public int QuantitàMine
        {
            get { return _quantitàMine; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                value = _quantitàMine;
            }
        }
        public Model(int lunghezza, int larghezza, int quantitàMine)
        {
            _larghezzaCampo = larghezza;
            _lunghezzaCampo = lunghezza;
            _quantitàMine = quantitàMine;
        }
    }
}