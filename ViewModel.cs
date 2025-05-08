using CoreNFC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1
{
    public class ViewModel
    {
        public List<Mine> ListaMine { get; private set; }
        Model[,] model;
        public bool trovataLaMina;

        public ViewModel(Model[,] campoGioco)
        {
            model = campoGioco;
            ListaMine = new List<Mine>();
        }

        //con il Tentativo verifico se ho cliccato la mina
        public void GestioneDifficoltà()
        {
            

        }

        public void GrandezzaPersonalizzata(int x, int y)
        {
            model = new Model[x,y];
        }

        public bool Tentativo(int x, int y)
        {
            return trovataLaMina;
        }
        public bool Vittoria()
        {
            if (trovataLaMina == true)
            {
                return false;
            }
            return true;
        }
    }
}
    
