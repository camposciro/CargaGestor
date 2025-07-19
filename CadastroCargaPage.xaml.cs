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

        // Validação dos campos
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

        // Criar objeto Carga com os dados informados
        var novaCarga = new Carga
        {
            DataCarregamento = data,
            NumeroCTE = cte,
            PesoKg = pesoKg,
            Status = "Em Percurso" // Status inicial fixo
        };

        // Adicionar nova carga no repositório estático
        CargaRepository.AdicionarCarga(novaCarga);

        await DisplayAlert("Sucesso", "Carga salva com sucesso!", "OK");

        // Navegar para a página de listar cargas
        await Shell.Current.GoToAsync("//ListarCargas");
    }
}
