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

        public override string ToString() 
        {
            if (!Scoperta)
                return HaBandierina ? "🚩" : "";
            else
                if (ContieneMina)
                return "💣";
            else
            if(MineAdiacenti==0)
                return String.Empty;
            else
                return MineAdiacenti.ToString();
        }
    }
}