using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.ViewModels;
using System.Diagnostics;
namespace MauiApp1.Views
{
    public partial class CampoDiGiocoView : ContentPage
    {
        Random rnd = new Random();
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
                    Cella cella = campoViewModel.Campo.Campo[r, c];

                    Button btn = new Button
                    {
                        Text = " "
                    };
                    btn.Command = campoViewModel.ScopriCellaCommand;
                    btn.CommandParameter = btn;
                    var longPress = new TouchBehavior
                    {
                        
                    };
                    longPress.LongPressCommand = campoViewModel.ToggleBandierinaCommand;
                    longPress.LongPressCommandParameter = btn;
                    longPress.LongPressDuration = 700;
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
}