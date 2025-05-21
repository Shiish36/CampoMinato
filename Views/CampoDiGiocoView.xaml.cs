using CommunityToolkit.Maui.Behaviors;
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

            // Usa Star per dimensioni uniformi
            for (int r = 0; r < campoViewModel.Campo.LunghezzaCampo; r++)
                GameGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            for (int c = 0; c < campoViewModel.Campo.LarghezzaCampo; c++)
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            for (int r = 0; r < campoViewModel.Campo.LunghezzaCampo; r++)
            {
                for (int c = 0; c < campoViewModel.Campo.LarghezzaCampo; c++)
                {
                    var cella = campoViewModel.Campo.Campo[r, c];

                    var btn = new Button
                    {
                        Text = " ",
                        Command = campoViewModel.ScopriCellaCommand,
                        CommandParameter = (r, c)
                    };

                    // Binding per il testo
                    btn.SetBinding(Button.TextProperty, new Binding($"Campo.Campo[{r},{c}]"));

                    // MultiBinding per il colore di sfondo
                    var multiBinding = new MultiBinding
                    {
                        Converter = new BoolToColorMultiConverter()
                    };
                    multiBinding.Bindings.Add(new Binding($"Campo.Campo[{r},{c}].Scoperta"));
                    multiBinding.Bindings.Add(new Binding($"Campo.Campo[{r},{c}].HaBandierina"));

                    btn.SetBinding(Button.BackgroundColorProperty, multiBinding);

                    var longPress = new TouchBehavior
                    {
                        LongPressCommand = campoViewModel.ToggleBandierinaCommand,
                        LongPressCommandParameter = (r, c),
                        LongPressDuration = 700
                    };

                    btn.Behaviors.Add(longPress);
                    GameGrid.Add(btn, c, r);
                }
            }
        }
    }
}