using CargaGestor;

namespace CargaGestor;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var shell = new AppShell();

        // Atualiza visibilidade dos menus conforme login
        shell.AtualizarRotasPorLogin();

        return new Window(shell);
    }
}
