namespace CargaGestor;

public partial class CadastroCargaPage : ContentPage
{
    public CadastroCargaPage()
    {
        InitializeComponent();

        // Define a data/hora atual assim que a tela é carregada
        Loaded += (s, e) =>
        {
            entryDataCarregamento.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        };
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        string data = entryDataCarregamento.Text;
        string cte = entryNumeroCTE.Text;
        string pesoTexto = entryPeso.Text;

        // Validação
        if (string.IsNullOrWhiteSpace(data) ||
            string.IsNullOrWhiteSpace(cte) ||
            string.IsNullOrWhiteSpace(pesoTexto))
        {
            await DisplayAlert("Erro", "Preencha todos os campos obrigatórios.", "OK");
            return;
        }

        if (!double.TryParse(pesoTexto.Replace(",", "."), out double pesoKg))
        {
            await DisplayAlert("Erro", "Peso inválido. Use apenas números.", "OK");
            return;
        }

        // Futuramente: salvar no banco (data, cte, pesoKg, status)

        await DisplayAlert("Sucesso", "Carga salva com sucesso!", "OK");

        // Volta automaticamente para a página principal (Home)
        await Shell.Current.GoToAsync("//Home");
    }
}
