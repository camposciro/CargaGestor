using Microsoft.Maui.Storage;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace CargaGestor;

public partial class TelaLoginPage : ContentPage
{
    private bool _biometriaTentada = false;

    public TelaLoginPage()
    {
        InitializeComponent();

        entryUsuario.Text = Preferences.Get("usuario", string.Empty);
        entrySenha.Text = Preferences.Get("senha", string.Empty);

        switchSalvarLogin.IsToggled =
            !string.IsNullOrEmpty(entryUsuario.Text) &&
            !string.IsNullOrEmpty(entrySenha.Text);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_biometriaTentada)
        {
            _biometriaTentada = true;
            await VerificarBiometriaAsync();
        }
    }

    private async Task VerificarBiometriaAsync()
    {
#if ANDROID || IOS
        bool biometriaAtiva = Preferences.Get("BiometriaAtiva", false);

        if (biometriaAtiva && await CrossFingerprint.Current.IsAvailableAsync())
        {
            var authRequestConfig = new AuthenticationRequestConfiguration("Autenticação", "Use sua digital para entrar")
            {
                CancelTitle = "Cancelar"
            };

            var result = await CrossFingerprint.Current.AuthenticateAsync(authRequestConfig);

            if (result.Authenticated)
            {
                Session.UsuarioLogado = true;

                var shell = (AppShell)Shell.Current;
                shell.AtualizarRotasPorLogin();

                await Shell.Current.GoToAsync("//Home");
            }
            else
            {
                await DisplayAlert("Erro", "Autenticação biométrica falhou ou foi cancelada.", "OK");
            }
        }
#endif
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

            await Shell.Current.GoToAsync("//Home");
        }
        else
        {
            await DisplayAlert("Erro", "Usuário ou senha inválidos.", "Tentar novamente");
        }
    }
}
