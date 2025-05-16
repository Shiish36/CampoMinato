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
            await Navigation.PushAsync(new Views.CampoDiGiocoView(new Models.CampoDiGioco(8, 8, 10)));
        }


        Models.CampoDiGioco CampoDiGioco;
        private void BtnDifficileClick(object sender, EventArgs e)
        {
            CampoDiGioco = new Models.CampoDiGioco(16, 30, 99);
        }
        private void BtnNormaleClick(object sender, EventArgs e)
        {
            CampoDiGioco = new Models.CampoDiGioco(16, 16, 40);
        }
        public void BtnCustomClick(object sender, EventArgs e)
        {

        }
    }
}