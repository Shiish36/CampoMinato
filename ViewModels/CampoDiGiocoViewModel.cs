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
        private CampoDiGioco _campo;

        public CampoDiGioco Campo
        {
            get => _campo;
            set
            {
                if (_campo != value)
                {
                    _campo = value;
                }
            }
        }

        private bool _giocoTerminato;

        public bool GiocoTerminato 
        {
            get => _giocoTerminato;
            set
            {
                if (_giocoTerminato != value)
                {
                    _giocoTerminato = value;
                    OnPropertyChanged(nameof(GiocoTerminato));
                }
            }
        }

        private bool _giocoVinto;
        public bool GiocoVinto 
        {
            get => _giocoVinto;
            set
            {
                if (_giocoVinto != value)
                {
                    _giocoVinto = value;
                    OnPropertyChanged(nameof(GiocoVinto));
                }
            }
        }

        public CampoDiGiocoViewModel(CampoDiGioco campo)
        {
            if(campo == null) throw new ArgumentNullException(nameof(campo), "Il campo di gioco non può essere nullo.");
            Campo = campo;
            GiocoTerminato = false;
            GiocoVinto = false;
            if(_campo==null) throw new ArgumentNullException(nameof(_campo), "Il campo di gioco non può essere nullo."); // non serve ma mi da errore ed é assai fastidioso.
        }

        [RelayCommand]
        private void ScopriCella(object sender)
        {
            if (sender is Button btn)
            {
                // Recupera la riga e la colonna dalla griglia (o da proprietà del btn)
                int y = Grid.GetRow(btn);
                int x = Grid.GetColumn(btn);
                var cella = Campo.Campo[x, y];

                if (cella.Scoperta || cella.HaBandierina)
                    return;

                if (!cella.ContieneMina)
                {
                    cella.Scoperta = true;
                    Campo.MineScoperte++;
                    btn.Text = cella.ToString();
                    Color backgroundColor;
                    if (cella.Scoperta)
                    {
                        backgroundColor = Color.Parse("#D3D3D3"); // Grigio chiaro per le celle scoperte
                    }
                    else if (cella.HaBandierina)
                    {
                        backgroundColor = Color.Parse("#FF0000"); // Rosso per le celle con bandierina
                    }
                    else
                    {
                        backgroundColor = Color.Parse("#FFFFFF"); // Bianco per le celle non scoperte
                    }
                    btn.BackgroundColor = backgroundColor;
                    OnPropertyChanged(nameof(cella.Scoperta));
                    return;
                }
                if (cella.MineAdiacenti == 0)
                {
                    ScopriAdiacenti(x, y);
                }

                VerificaVittoria();
                if (cella.ContieneMina)
                {
                    GiocoTerminato = true;
                    RivelareTutteLeMine();
                }
                sender = btn; // Aggiorna il sender per il binding
            }
        }

        private void VerificaVittoria()
        {
            if (Campo.QuantitàMine == Campo.MineScoperte)
            {
                GiocoVinto = true;
                GiocoTerminato = true;
            }            
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
                                OnPropertyChanged(nameof(neighbor.Scoperta));

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
        public void ToggleBandierina(object sender)
        {
            if (sender is Button btn)
            {
                // Recupera la riga e la colonna dalla griglia (o da proprietà del btn)
                int y = Grid.GetRow(btn);
                int x = Grid.GetColumn(btn);
                var cella = Campo.Campo[x, y];

                // non toggliare se già scoperta
                if (cella.Scoperta) return;

                cella.HaBandierina = !cella.HaBandierina;
                OnPropertyChanged(nameof(cella.HaBandierina)); // Notifica il cambiamento
                btn.BackgroundColor = Color.Parse("#FF0000");
                btn.Text=cella.ToString();
            }
        }
    }
}
