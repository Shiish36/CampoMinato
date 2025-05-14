using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1.ViewModels
{
    public class CellaViewModel
    {
        CampoDiGioco CampoDiGioco;
        public CellaViewModel(CampoDiGioco campoDiGioco) 
        {
            CampoDiGioco = campoDiGioco;
        }

        public void ToggleBandierina(int x, int y) 
        {
            CampoDiGioco.Campo[x, y].HaBandierina = !CampoDiGioco.Campo[x,y].HaBandierina;
        }
    }
}