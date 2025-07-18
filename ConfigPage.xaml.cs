namespace CargaGestor;

public partial class ConfigPage : ContentPage
{
    // Simulando armazenamento local para configurações (em app real usar Preferences ou banco)
    private bool notificacoesAtivas = true;
    private string idiomaSelecionado = "Português";

    public ConfigPage()
    {
        InitializeComponent();

        // Inicializa switch do modo escuro conforme tema atual
        switchModoEscuro.IsToggled = App.Current.UserAppTheme == AppTheme.Dark;

        // Inicializa switch de notificações
        switchNotificacoes.IsToggled = notificacoesAtivas;

        // Inicializa picker com idioma selecionado
        pickerIdioma.SelectedIndex = GetIndexIdioma(idiomaSelecionado);
    }

    private void OnSwitchModoEscuroToggled(object sender, ToggledEventArgs e)
    {
        App.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }

    private void OnSwitchNotificacoesToggled(object sender, ToggledEventArgs e)
    {
        notificacoesAtivas = e.Value;
        // Aqui você pode adicionar lógica para habilitar/desabilitar notificações reais
    }

    private void OnIdiomaChanged(object sender, EventArgs e)
    {
        if (pickerIdioma.SelectedIndex == -1)
            return;

        idiomaSelecionado = pickerIdioma.Items[pickerIdioma.SelectedIndex];
        // Aqui pode ser implementada a troca de idioma do app (recursos, traduções)
        DisplayAlert("Idioma", $"Idioma alterado para {idiomaSelecionado}", "OK");
    }

    private int GetIndexIdioma(string idioma)
    {
        switch (idioma)
        {
            case "Português":
                return 0;
            case "Inglês":
                return 1;
            case "Espanhol":
                return 2;
            default:
                return 0;
        }
    }
}
