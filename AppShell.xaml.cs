using Microsoft.Maui.Controls;

namespace CargaGestor;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrar rotas extras que podem ser usadas na navegação programática
        Routing.RegisterRoute("Home", typeof(HomePage));
        Routing.RegisterRoute("CadastroCarga", typeof(CadastroCargaPage));
        Routing.RegisterRoute("Configuracoes", typeof(ConfigPage));
        Routing.RegisterRoute("Login", typeof(TelaLoginPage));
        Routing.RegisterRoute("ListarCargas", typeof(ListarCargasPage));
        Routing.RegisterRoute("Relatorios", typeof(RelatoriosPage));
        Routing.RegisterRoute("ControleStatus", typeof(ControleStatusPage));
        Routing.RegisterRoute("Ajuda", typeof(AjudaPage));

        AtualizarRotasPorLogin();
    }

    public void AtualizarRotasPorLogin()
    {
        if (Session.UsuarioLogado)
        {
            menuLogin.IsVisible = false;
            menuHome.IsVisible = true;
            menuCadastro.IsVisible = true;
            menuListarCargas.IsVisible = true;
            menuRelatorios.IsVisible = true;
            menuControleStatus.IsVisible = true;
            menuConfiguracoes.IsVisible = true;
            menuAjuda.IsVisible = true;

            CurrentItem = menuHome;
        }
        else
        {
            menuLogin.IsVisible = true;
            menuHome.IsVisible = false;
            menuCadastro.IsVisible = false;
            menuListarCargas.IsVisible = false;
            menuRelatorios.IsVisible = false;
            menuControleStatus.IsVisible = false;
            menuConfiguracoes.IsVisible = false;
            menuAjuda.IsVisible = false;

            CurrentItem = menuLogin;
        }
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Sair", "Deseja realmente sair?", "Sim", "Cancelar");
        if (confirmar)
        {
            Session.UsuarioLogado = false;
            AtualizarRotasPorLogin();
            await Shell.Current.GoToAsync("//Login");
        }
    }

    public async Task LogoutAsync()
    {
        Session.UsuarioLogado = false;
        AtualizarRotasPorLogin();
        await Task.Delay(100); // Espera para garantir atualização antes de navegar
        await GoToAsync("//Login");
    }
}
