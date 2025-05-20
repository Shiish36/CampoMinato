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
        public CampoDiGioco Campo { get; private set; }
        public bool GiocoTerminato { get; private set; }

        public CampoDiGiocoViewModel(CampoDiGioco campo)
        {
            Campo = campo;
            GiocoTerminato = false;
        }

        [RelayCommand]
        public void ScopriCella((int x, int y) Coordinates)
        {

            var (x, y) = Coordinates;
            if (GiocoTerminato) return;

            var cella = Campo.Campo[x, y];

            if (cella.Scoperta || cella.HaBandierina)
                return;

            if (cella.ContieneMina)
            {
                GiocoTerminato = true;
            }

            cella.Scoperta = true; // Scopri la cella
            OnPropertyChanged(nameof(cella.Scoperta)); // Notifica il cambiamento
            if (cella.MineAdiacenti == 0)
            {
                ScopriAdiacenti(x, y);
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
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && nx < Campo.LunghezzaCampo &&
                        ny >= 0 && ny < Campo.LarghezzaCampo)
                    {
                        var vicina = Campo.Campo[nx, ny];
                        if (!vicina.Scoperta && !vicina.ContieneMina)
                        {
                            vicina.Scoperta = true; // Scopri la cella
                            OnPropertyChanged(nameof(vicina.Scoperta)); // Notifica il cambiamento
                            if (vicina.MineAdiacenti == 0)
                            {
                                // ricorsione
                                ScopriAdiacenti(nx, ny);
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
