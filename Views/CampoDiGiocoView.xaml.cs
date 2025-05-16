using CommunityToolkit.Maui.Behaviors;
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

            for (int r = 0; r < campoViewModel.Campo.; r++)
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
                        WidthRequest = 40,
                        HeightRequest = 40
                    };

                    var rCopy = r;
                    var cCopy = c;

                    var tapGesture = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 1
                    };
                    tapGesture.Tapped += (s, e) => OnTap(rCopy, cCopy);

                    var longPress = new Microsoft.Maui.Controls.GestureRecognizers.LongPressGestureRecognizer();
                    longPress.LongPressed += (s, e) => OnLongPress(rCopy, cCopy);

                    btn.GestureRecognizers.Add(tapGesture);
                    btn.GestureRecognizers.Add(longPress);

                    GameGrid.Add(btn, c, r);
                }
            }
        }
    }
}