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
            int columns = int.Parse(ColumnsEntry.Text);
            int mines = int.Parse(MinesEntry.Text);

            if (mines >= rows * columns)
            {
                mines = rows * columns - 1; //imposto un valore massimo di mine se le mine superano il numero di celle
            }

            // Restituisco i valori come tupla
            Close((rows, columns, mines));
        }
    }
}