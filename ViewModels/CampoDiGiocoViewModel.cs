using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.ViewModels
{
    public class CampoDiGiocoViewModel
    {
        public CampoDiGioco Campo { get; private set; }
        public bool GiocoTerminato { get; private set; }

        public CampoDiGiocoViewModel(CampoDiGioco campo)
        {
            Campo = campo;
            GiocoTerminato = false;
        }

        public int ScopriCella(int x, int y)
        {
            if (GiocoTerminato) return -1;

            var cella = Campo.Campo[x, y];

            if (cella.Scoperta || cella.HaBandierina)
                return 0;

            if (cella.ContieneMina)
            {
                GiocoTerminato = true;
                return -1;
            }
            
            if (cella.MineAdiacenti == 0)
            {
                ScopriAdiacenti(x, y);
                return 1;
            }
            else
            {
                cella.Scoperta = true;
                return 1;
            }
        }

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
                            vicina.Scoperta = true;

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
    }
}
