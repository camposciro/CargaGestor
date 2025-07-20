using Microsoft.Maui.Controls;

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
            pickerStatus.SelectedIndex = 0; // Seleciona "Em Viagem" por padrão
        };
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        string data = entryDataCarregamento.Text;
        string cte = entryNumeroCTE.Text;
        string pesoTexto = entryPeso.Text;
        string status = pickerStatus.SelectedItem as string;

        // Validação dos campos
        if (string.IsNullOrWhiteSpace(data) ||
            string.IsNullOrWhiteSpace(cte) ||
            string.IsNullOrWhiteSpace(pesoTexto) ||
            string.IsNullOrWhiteSpace(status))
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
            Status = status
        };

        // Adicionar nova carga no repositório estático
        CargaRepository.AdicionarCarga(novaCarga);

        await DisplayAlert("Sucesso", "Carga salva com sucesso!", "OK");

        // Navegar para a página de listar cargas
        await Shell.Current.GoToAsync("//ListarCargas");
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        // Voltar para a página principal (Home)
        await Shell.Current.GoToAsync("//Home");
    }
}
