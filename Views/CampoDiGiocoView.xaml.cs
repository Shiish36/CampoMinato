using CommunityToolkit.Maui.Behaviors;
using MauiApp1.Structure;
using MauiApp1.ViewModels;
using System.Diagnostics;
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
            GameGrid.RowSpacing = 3; 
            GameGrid.ColumnSpacing = 3;
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

                    var multiBinding = new MultiBinding
                    {
                        Converter = new BoolToColorMultiConverter()
                    };

                    // Binding per Scoperta (usa la cella come Source, non il ViewModel)
                    multiBinding.Bindings.Add(new Binding(
                        path: "Scoperta",  // <-- Nota: ora il path è solo il nome della proprietà!
                        source: cella      // <-- Source è la cella stessa, non il ViewModel
                    ));

                    // Binding per HaBandierina
                    multiBinding.Bindings.Add(new Binding(
                        path: "HaBandierina",
                        source: cella
                    ));
                    btn.SetBinding(Button.BackgroundColorProperty, multiBinding);

                    var longPress = new TouchBehavior
                    {
                        LongPressCommand = campoViewModel.ToggleBandierinaCommand,
                        LongPressCommandParameter = (r, c),
                        LongPressDuration = 700
                    };

                    btn.Behaviors.Add(longPress);
                    
                    if (btn != null)
                    {
                        Grid.SetColumn(btn, c);
                        Grid.SetRow(btn, r);
                        GameGrid.Add(btn);
                    }
                    else
                    {
                        Debug.WriteLine("ERRORE: btn è null!");
                    }
                }
            }
        }
    }
}