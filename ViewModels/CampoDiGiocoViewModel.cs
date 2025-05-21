using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;

namespace MauiApp1.ViewModels
{
    public partial class CampoDiGiocoViewModel : ObservableObject
    {
        [ObservableProperty]
        private CampoDiGioco _campo;

        [ObservableProperty]
        private bool _giocoTerminato;

        [ObservableProperty]
        private bool _giocoVinto;

        public CampoDiGiocoViewModel(CampoDiGioco campo)
        {
            Campo = campo;
            GiocoTerminato = false;
            GiocoVinto = false;
        }

        [RelayCommand]
        public void ScopriCella((int x, int y) coordinates)
        {
            if (GiocoTerminato) return;

            var (x, y) = coordinates;
            var cella = Campo.Campo[x, y];

            if (cella.Scoperta || cella.HaBandierina)
                return;

            cella.Scoperta = true;

            if (cella.ContieneMina)
            {
                GiocoTerminato = true;
                // Mostra tutte le mine
                RivelareTutteLeMine();
                return;
            }

            if (cella.MineAdiacenti == 0)
            {
                ScopriAdiacenti(x, y);
            }

            VerificaVittoria();
        }

        private void VerificaVittoria()
        {
            foreach (var cella in Campo.Campo)
            {
                if (!cella.ContieneMina && !cella.Scoperta)
                    return;
            }

            GiocoVinto = true;
            GiocoTerminato = true;
        }

        private void RivelareTutteLeMine()
        {
            for (int i = 0; i < Campo.LunghezzaCampo; i++)
            {
                for (int j = 0; j < Campo.LarghezzaCampo; j++)
                {
                    if (Campo.Campo[i, j].ContieneMina)
                    {
                        Campo.Campo[i, j].Scoperta = true;
                    }
                }
            }
        }

        /// <summary>
        /// Attiva automaticamente tutte le Celle adiacenti alla Cella nella poizione x,y. 
        /// La funzione si richiama automaticamente se una casella adiacente non ha mine affianco.
        /// </summary>
        /// /// <param name="x">Numero colonna della grid</param>
        /// <param name="y">Numero riga della grid</param>

        private void ScopriAdiacenti(int x, int y)
        {
            // usa una coda invece della ricorsione per evitare stack overflow
            var queue = new Queue<(int, int)>();
            queue.Enqueue((x, y));

            while (queue.Count > 0)
            {
                var (currentX, currentY) = queue.Dequeue();
                var currentCell = Campo.Campo[currentX, currentY];

                // scopri tutte le celle adiacenti
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = currentX + dx;
                        int ny = currentY + dy;

                        if (nx >= 0 && nx < Campo.LunghezzaCampo &&
                            ny >= 0 && ny < Campo.LarghezzaCampo)
                        {
                            var neighbor = Campo.Campo[nx, ny];

                            if (!neighbor.Scoperta && !neighbor.ContieneMina && !neighbor.HaBandierina)
                            {
                                neighbor.Scoperta = true;

                                if (neighbor.MineAdiacenti == 0)
                                {
                                    queue.Enqueue((nx, ny));
                                }
                            }
                        }
                    }
                }
            }
        }


        [RelayCommand]
        public void ToggleBandierina((int x, int y) coordinate)
        {
            var (x, y) = coordinate;
            var cella = Campo.Campo[x, y];

            // non toggliare se già scoperta
            if (cella.Scoperta) return;

            cella.HaBandierina = !cella.HaBandierina;
            OnPropertyChanged(nameof(cella.HaBandierina)); // Notifica il cambiamento
        }
    }
}
