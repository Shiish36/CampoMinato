using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1.Models
{
    public class Cella
    {
        public bool ContieneMina { get; internal set; }
        public int MineAdiacenti { get; internal set; }
        public bool Scoperta { get; set; }
        public bool HaBandierina { get; set; }
        public Cella()
        {
            ContieneMina = false;
            MineAdiacenti = 0;
        }
    }
}