using Microsoft.Maui.Storage;

namespace CargaGestor;

public partial class ConfigPage : ContentPage
{
    private const string TemaSalvoKey = "TemaEscuroAtivo";
    private const bool TemaEscuroPadrao = false;
    private const string BiometriaAtivaKey = "BiometriaAtiva";

    private bool notificacoesAtivas = true;

    public ConfigPage()
    {
        InitializeComponent();

        // Tema
        bool temaEscuroAtivo = Preferences.Get(TemaSalvoKey, TemaEscuroPadrao);
        App.Current.UserAppTheme = temaEscuroAtivo ? AppTheme.Dark : AppTheme.Light;
        switchModoEscuro.IsToggled = temaEscuroAtivo;

        // Notificações (por enquanto assume true)
        switchNotificacoes.IsToggled = notificacoesAtivas;

        // Biometria
        bool biometriaAtiva = Preferences.Get(BiometriaAtivaKey, false);
        switchBiometria.IsToggled = biometriaAtiva;
    }

    private void OnSwitchModoEscuroToggled(object sender, ToggledEventArgs e)
    {
        bool temaEscuro = e.Value;
        App.Current.UserAppTheme = temaEscuro ? AppTheme.Dark : AppTheme.Light;
        Preferences.Set(TemaSalvoKey, temaEscuro);
    }

    private void OnSwitchNotificacoesToggled(object sender, ToggledEventArgs e)
    {
        notificacoesAtivas = e.Value;
        // lógica futura
    }

    private void OnSwitchBiometriaToggled(object sender, ToggledEventArgs e)
    {
        Preferences.Set(BiometriaAtivaKey, e.Value);
    }
}
