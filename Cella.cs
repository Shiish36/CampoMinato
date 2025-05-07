using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1
{
    public class Cella
    {
        public bool ContieneMina { get; set; }
        public int MineAdiacenti { get; set; }

        public Cella()
        {
            ContieneMina = false;
            MineAdiacenti = 0;
        }
    }
}