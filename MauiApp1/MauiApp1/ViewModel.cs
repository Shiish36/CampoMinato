using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1
{
    public class ViewModel
    {
        Mine mine;
        Model model;
        public bool trovataLaMina;

        public Mine Mine
        {
            get => default;
            set
            {
            }
        }

        public Model Model
        {
            get => default;
            set
            {
            }
        }

        //con il Tentativo verifico se ho cliccato la mina
        public void GestioneDifficoltà()
        {
            throw new System.NotImplementedException();
            
        }

        public void GrandezzaPersonalizzata(int x, int y)
        {
            //x e y sono la lunghezza e larghezza del campo
        }

        public void Tentativo(int x, int y)
        {
            throw new System.NotImplementedException();
            //posizione che l'utente clicca nella matrice
           
        }
        public bool Vittoria()
        {
            if(trovataLaMina == true)
            {
                return false;
            }
            return true;
        }
    }
}