using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace CargaGestor;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("Login", typeof(TelaLoginPage));
        Routing.RegisterRoute("Home", typeof(HomePage));
        Routing.RegisterRoute("CadastroCarga", typeof(CadastroCargaPage));
        Routing.RegisterRoute("ListarCargas", typeof(ListarCargasPage));
        Routing.RegisterRoute("Relatorios", typeof(RelatorioGanhosPage));
        Routing.RegisterRoute("Configuracoes", typeof(ConfigPage));
        Routing.RegisterRoute(nameof(EditarCargaPage), typeof(EditarCargaPage));
        Routing.RegisterRoute("ControleStatus", typeof(ControleStatusPage));
        Routing.RegisterRoute("Ajuda", typeof(AjudaPage));

        AtualizarRotasPorLogin();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!Session.UsuarioLogado)
        {
            CurrentItem = menuLogin;
        }
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
            menuConfiguracoes.IsVisible = true;

            CurrentItem = menuHome;
        }
        else
        {
            menuLogin.IsVisible = true;
            menuHome.IsVisible = false;
            menuCadastro.IsVisible = false;
            menuListarCargas.IsVisible = false;
            menuRelatorios.IsVisible = false;
            menuConfiguracoes.IsVisible = false;

            CurrentItem = menuLogin;
        }
    }

    public async Task LogoutAsync()
    {
        Session.UsuarioLogado = false;

        AtualizarRotasPorLogin();

        await Task.Delay(100);

        CurrentItem = menuLogin;
    }
}
