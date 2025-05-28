using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1.Models
{
    public class CampoDiGioco 

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
                _lunghezzaCampo = value;
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
                _larghezzaCampo = value;
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
                _quantitàMine = value;
            }
        }
        private int _mineScoperte=0;
        public int MineScoperte
        {
            get { return _mineScoperte; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                _mineScoperte = value;
            }
        }
        public Cella[,] Campo { get; private set; } //grid per la gestione delle celle, cioè il campo di gioco

        public CampoDiGioco(int lunghezza, int larghezza, int quantitàMine)
        {
            LunghezzaCampo = lunghezza;
            LarghezzaCampo = larghezza;
            QuantitàMine = quantitàMine;

            Campo = new Cella[LunghezzaCampo, LarghezzaCampo];


            for (int i = 0; i < LunghezzaCampo; i++)
            {
                for (int j = 0; j < LarghezzaCampo; j++)
                {
                    Campo[i, j] = new Cella();
                }
            }
            GeneraMineCasuali(QuantitàMine);
        }

        /// <summary>
        /// Imposta il valore di Cella, ContieneMina a true, ad un x numero di mine
        /// </summary>
        private void GeneraMineCasuali(int quantitàMine)
        {
            Random rand = new Random();
            int mineGenerate = 0;

            while (mineGenerate < quantitàMine)
            {
                int x = rand.Next(0, LunghezzaCampo);
                int y = rand.Next(0, LarghezzaCampo);

                if (!Campo[x, y].ContieneMina)
                {
                    Campo[x, y].ContieneMina = true;
                    mineGenerate++;
                    AggiornaMineAdiacenti(x, y);
                }
            }
        }


        /// <summary>
        /// Aggiorna le mine adiacenti a tutte le caselle che non sono mine, in quanto vengono inizializzate prima della generazione delle mine.
        /// </summary>
        /// <param name="x">Numero colonna della grid</param>
        /// <param name="y">Numero riga della grid</param>
        private void AggiornaMineAdiacenti(int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < LunghezzaCampo && j >= 0 && j < LarghezzaCampo)
                    {
                        if (!Campo[i, j].ContieneMina)
                        {
                            Campo[i, j].MineAdiacenti++;
                        }
                    }
                }
            }
        }
    }
}
