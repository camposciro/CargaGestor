namespace CargaGestor;

public partial class TelaLoginPage : ContentPage
{
    public TelaLoginPage()
    {
        InitializeComponent();
    }

    private async void OnEntrarClicked(object sender, EventArgs e)
    {
        string usuario = entryUsuario.Text;
        string senha = entrySenha.Text;

        if (usuario == "admin" && senha == "123")
        {
            Session.UsuarioLogado = true;

            AppShell shell = (AppShell)Shell.Current;
            shell.AtualizarRotasPorLogin();

            await DisplayAlert("Sucesso", "Login realizado com sucesso!", "OK");
            await Shell.Current.GoToAsync("///Home");
        }
        else
        {
            await DisplayAlert("Erro", "Usuário ou senha inválidos", "Tentar novamente");
        }
    }
}
