using Android.Widget;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using static Android.InputMethodServices.Keyboard;

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
            await Navigation.PushAsync(new Views.CampoDiGiocoView(new Models.CampoDiGioco(8, 8, 10)));
        }

        [RelayCommand]
        public async Task BtnMedioClickCommand()
        {
            await Navigation.PushAsync(new Views.CampoDiGiocoView(new Models.CampoDiGioco(16, 16, 40)));
        }

        [RelayCommand]
        public async Task BtnDifficileClickCommand()
        {
            await Navigation.PushAsync(new Views.CampoDiGiocoView(new Models.CampoDiGioco(16, 30, 99)));
        }

        [RelayCommand]
        public async Task BtnCustomClickCommand()
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

    



