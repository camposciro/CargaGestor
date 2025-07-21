using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CargaGestor;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Aqui você pode colocar configurações globais, como tema padrão
        // Exemplo: UserAppTheme = AppTheme.Dark;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var shell = new AppShell();

        shell.AtualizarRotasPorLogin();

        if (!Session.UsuarioLogado)
        {
            // Usa o shell criado para navegação, não Shell.Current
            _ = shell.GoToAsync("//Login");
        }

        return new Window(shell);
    }
}
