namespace CargaGestor;

public partial class ConfigPage : ContentPage
{
    // Simulando armazenamento local para configurações (em app real usar Preferences ou banco)
    private bool notificacoesAtivas = true;

    public ConfigPage()
    {
        InitializeComponent();

        // Inicializa switch do modo escuro conforme tema atual
        switchModoEscuro.IsToggled = App.Current.UserAppTheme == AppTheme.Dark;

        // Inicializa switch de notificações
        switchNotificacoes.IsToggled = notificacoesAtivas;
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
}
