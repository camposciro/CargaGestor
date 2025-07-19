using Microsoft.Maui.Storage;

namespace CargaGestor;

public partial class ConfigPage : ContentPage
{
    private const string TemaSalvoKey = "TemaEscuroAtivo";
    private const bool TemaEscuroPadrao = false; // se nada estiver salvo, assume tema claro

    private bool notificacoesAtivas = true;

    public ConfigPage()
    {
        InitializeComponent();

        // Lê o tema salvo
        bool temaEscuroAtivo = Preferences.Get(TemaSalvoKey, TemaEscuroPadrao);
        App.Current.UserAppTheme = temaEscuroAtivo ? AppTheme.Dark : AppTheme.Light;

        switchModoEscuro.IsToggled = temaEscuroAtivo;
        switchNotificacoes.IsToggled = notificacoesAtivas;
    }

    private void OnSwitchModoEscuroToggled(object sender, ToggledEventArgs e)
    {
        bool temaEscuro = e.Value;

        // Aplica o tema
        App.Current.UserAppTheme = temaEscuro ? AppTheme.Dark : AppTheme.Light;

        // Salva a escolha
        Preferences.Set(TemaSalvoKey, temaEscuro);
    }

    private void OnSwitchNotificacoesToggled(object sender, ToggledEventArgs e)
    {
        notificacoesAtivas = e.Value;
        // Lógica futura de notificações
    }
}
