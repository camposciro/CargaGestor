using CargaGestor;

namespace CargaGestor;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnCadastrarCargaClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CadastroCarga");
    }

    private async void OnListarCargasClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ListarCargas");
    }

    private async void OnRelatoriosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Relatorios");
    }

    private async void OnConfiguracoesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Configuracoes");
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Sair", "Deseja realmente sair?", "Sim", "Cancelar");
        if (confirmar)
        {
            var shell = (AppShell)Shell.Current;
            await shell.LogoutAsync();
        }
    }
}
