using Microsoft.Maui.Storage;

namespace CargaGestor;

public partial class TelaLoginPage : ContentPage
{
    public TelaLoginPage()
    {
        InitializeComponent();

        // Carrega usuário e senha salvos (se existirem)
        entryUsuario.Text = Preferences.Get("usuario", string.Empty);
        entrySenha.Text = Preferences.Get("senha", string.Empty);

        // Marca o switch se usuário e senha foram salvos
        switchSalvarLogin.IsToggled =
            !string.IsNullOrEmpty(entryUsuario.Text) &&
            !string.IsNullOrEmpty(entrySenha.Text);
    }

    private async void OnEntrarClicked(object sender, EventArgs e)
    {
        string usuario = entryUsuario.Text?.Trim() ?? string.Empty;
        string senha = entrySenha.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
        {
            await DisplayAlert("Erro", "Informe usuário e senha.", "OK");
            return;
        }

        if (usuario == "admin" && senha == "123")
        {
            Session.UsuarioLogado = true;

            // Salva ou remove as credenciais
            if (switchSalvarLogin.IsToggled)
            {
                Preferences.Set("usuario", usuario);
                Preferences.Set("senha", senha);
            }
            else
            {
                Preferences.Remove("usuario");
                Preferences.Remove("senha");
            }

            var shell = (AppShell)Shell.Current;
            shell.AtualizarRotasPorLogin();

            await DisplayAlert("Sucesso", "Login realizado com sucesso!", "OK");
            await Shell.Current.GoToAsync("///Home");
        }
        else
        {
            await DisplayAlert("Erro", "Usuário ou senha inválidos.", "Tentar novamente");
        }
    }
}
