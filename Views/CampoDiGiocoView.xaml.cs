using MauiApp1.ViewModels;
namespace MauiApp1.Views
{
    public partial class CampoDiGiocoView : ContentPage
    {
        ViewModels.CampoDiGiocoViewModel campoViewModel;
        TapGestureRecognizer tapGesture = new TapGestureRecognizer();
        public CampoDiGiocoView(Models.CampoDiGioco model)
        {
            InitializeComponent();
            campoViewModel = new ViewModels.CampoDiGiocoViewModel(model);
            BindingContext = campoViewModel;
            CostruisciGriglia();
        }

        private void CostruisciGriglia()
        {
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
            GameGrid.Children.Clear();

            for (int r = 0; r < campoViewModel.Campo.LunghezzaCampo; r++)
                GameGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            for (int c = 0; c < campoViewModel.Campo.LarghezzaCampo; c++)
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            for (int r = 0; r < campoViewModel.Campo.LunghezzaCampo; r++)
            {
                for (int c = 0; c < campoViewModel.Campo.LarghezzaCampo; c++)
                {
                    var cella = campoViewModel.Campo.Campo[r, c];

                    var btn = new Button
                    {
                        Text = " ", // puoi cambiarlo in base a stato
                        BackgroundColor = Colors.Gray,
                        WidthRequest = GameGrid.Width / GameGrid.ColumnDefinitions.Count,
                        HeightRequest = GameGrid.Height / GameGrid.RowDefinitions.Count,
                        Command = ((CampoDiGiocoViewModel)BindingContext).ScopriCella(), // neanche questo va per qualche arcano motivo
                        CommandParameter = new Tuple<int, int>(r, c),
                    };

                    var LongPress = new LongPressRecognizer // non va per qualche motivo arcano
                    {
                        Command = ((CellaViewModel)BindingContext).ToggleBandierina(),
                        MinimumPressDuration = 700 //in millisecondi
                    };

         

                    GameGrid.Add(btn, c, r);
                }
            }
        }
    }
}