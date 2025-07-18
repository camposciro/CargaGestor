using Microsoft.Maui.Controls;
using CargaGestor;

namespace CargaGestor;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registra rotas (opcional, pois usamos ShellContent com nome e rota)
        Routing.RegisterRoute("Home", typeof(HomePage));
        Routing.RegisterRoute("CadastroCarga", typeof(CadastroCargaPage));
        Routing.RegisterRoute("Configuracoes", typeof(ConfigPage));
        Routing.RegisterRoute("Login", typeof(TelaLoginPage));

        AtualizarRotasPorLogin();
    }

    public void AtualizarRotasPorLogin()
    {
        if (Session.UsuarioLogado)
        {
            menuLogin.IsVisible = false;
            menuHome.IsVisible = true;
            menuCadastro.IsVisible = true;
            menuConfiguracoes.IsVisible = true;

            CurrentItem = menuHome;
        }
        else
        {
            menuLogin.IsVisible = true;
            menuHome.IsVisible = false;
            menuCadastro.IsVisible = false;
            menuConfiguracoes.IsVisible = false;

            CurrentItem = menuLogin;
        }
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Sair", "Deseja realmente sair?", "Sim", "Cancelar");
        if (confirmar)
        {
            Session.UsuarioLogado = false;

            if (Shell.Current is AppShell shell)
            {
                shell.AtualizarRotasPorLogin();

                // Navegar para login limpando histórico
                await Shell.Current.GoToAsync("//Login");
            }
        }
    }


    public async Task LogoutAsync()
    {
        Session.UsuarioLogado = false;

        AtualizarRotasPorLogin();

        // Evita o erro de "ShellItem não encontrado"
        await Task.Delay(100); // dá tempo de trocar o item antes de navegar
        await GoToAsync("//Login");
    }
}
