
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp1.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        [RelayCommand]
        public async Task BtnFacileClick()
        {
            Models.CampoDiGioco campoDiGioco = new Models.CampoDiGioco(3, 3, 1);
            CampoDiGiocoView c = new CampoDiGiocoView(campoDiGioco);
            await Navigation.PushAsync(c);
        }

        [RelayCommand]
        public async Task BtnMedioClick()
        {
            await Navigation.PushAsync(new Views.CampoDiGiocoView(new Models.CampoDiGioco(16, 16, 40)));
        }

        [RelayCommand]
        public async Task BtnDifficileClick()
        {
            await Navigation.PushAsync(new Views.CampoDiGiocoView(new Models.CampoDiGioco(16, 30, 99)));
        }

        [RelayCommand]
        public async Task BtnCustomClick()
        {
            var result = await Shell.Current.ShowPopupAsync(new CustomGamePopup());

            if (result is ValueTuple<int, int, int> size) 
            {
                var (rows, columns, mines) = size;
                if (mines >= rows * columns)
                {
                    // Mostra errore
                    return;
                }
                await Navigation.PushAsync(new Views.CampoDiGiocoView(new Models.CampoDiGioco(rows, columns, mines)));
            }

        }
    }
}