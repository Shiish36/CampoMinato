using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Models
{
    public class Cella : INotifyPropertyChanged
    {
        private bool _contieneMina;
        private int _mineAdiacenti;
        private bool _scoperta;
        private bool _haBandierina;

        public bool ContieneMina
        {
            get => _contieneMina;
            internal set => SetProperty(ref _contieneMina, value);
        }

        public int MineAdiacenti
        {
            get => _mineAdiacenti;
            internal set => SetProperty(ref _mineAdiacenti, value);
        }

        public bool Scoperta
        {
            get => _scoperta;
            set => SetProperty(ref _scoperta, value);
        }

        public bool HaBandierina
        {
            get => _haBandierina;
            set => SetProperty(ref _haBandierina, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public override string ToString()
        {
            if (!Scoperta)
                return HaBandierina ? "🚩" : "";
            else if (ContieneMina)
                return "💣";
            else if (MineAdiacenti == 0)
                return string.Empty;
            else
                return MineAdiacenti.ToString();
        }
    }
}