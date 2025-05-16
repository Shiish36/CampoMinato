using CommunityToolkit.Maui.Views;

namespace MauiApp1.Views
{
    public partial class CustomGamePopup : Popup
    {
        public CustomGamePopup()
        {
            InitializeComponent();
        }

        private void OnConfirmClicked(object sender, EventArgs e)
        {
            int rows = int.Parse(RowsEntry.Text);
            int cols = int.Parse(ColumnsEntry.Text);
            int mines = int.Parse(MinesEntry.Text);

            if (mines >= rows * cols)
            {
                Close(null); // Oppure mostra errore
                return;
            }

            // Restituisco i valori come tupla
            Close((rows, cols, mines));
        }
    }
}