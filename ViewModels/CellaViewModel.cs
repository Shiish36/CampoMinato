using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1.ViewModels
{
    public partial class CellaViewModel : ObservableObject
    {
        private readonly Cella _cella;

        public int X { get; }
        public int Y { get; }

        public CellaViewModel(Cella cella, int x, int y)
        {
            _cella = cella;
            X = x;
            Y = y;
        }

        public bool ContieneMina => _cella.ContieneMina;

        public bool Scoperta
        {
            get => _cella.Scoperta;
            set
            {
                SetProperty(_cella.Scoperta, value, _cella, (c, v) => c.Scoperta = v);
                OnPropertyChanged(nameof(DisplayText));
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public string DisplayText => !_cella.Scoperta ? "" :
            _cella.ContieneMina ? "💣" :
            _cella.MineAdiacenti > 0 ? _cella.MineAdiacenti.ToString() : "";

        public Color BackgroundColor => !_cella.Scoperta ? Colors.Gray : Colors.LightGray;

        [RelayCommand]
        public async void ToggleBandierina()
        {
            _cella.HaBandierina = !_cella.HaBandierina;
            OnPropertyChanged(nameof(DisplayText));
        }
    }
}