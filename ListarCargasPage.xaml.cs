using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CargaGestor;

public partial class ListarCargasPage : ContentPage
{
    private ObservableCollection<Carga> cargasFiltradas = new ObservableCollection<Carga>();
    private Carga cargaSelecionada;

    public ListarCargasPage()
    {
        InitializeComponent();

        collectionViewCargas.ItemsSource = cargasFiltradas;

        btnEditar.IsEnabled = false;
        btnExcluir.IsEnabled = false;

        entryFiltro.TextChanged += OnFiltroTextChanged;
        collectionViewCargas.SelectionChanged += OnCargaSelecionada;

        btnEditar.Clicked += OnEditarClicked;
        btnExcluir.Clicked += OnExcluirClicked;
        btnSair.Clicked += OnSairClicked;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargaRepository.CarregarCargasAsync();
        AtualizarFiltro(entryFiltro.Text);
    }

    private void AtualizarFiltro(string filtro = "")
    {
        filtro = filtro?.ToLower() ?? "";

        cargasFiltradas.Clear();

        var filtradas = CargaRepository.Cargas.Where(c =>
            (c.NumeroCTE?.ToLower().Contains(filtro) ?? false) ||
            (c.Status?.ToLower().Contains(filtro) ?? false));

        foreach (var carga in filtradas)
            cargasFiltradas.Add(carga);

        cargaSelecionada = null;
        collectionViewCargas.SelectedItem = null;
        btnEditar.IsEnabled = false;
        btnExcluir.IsEnabled = false;
    }

    private void OnFiltroTextChanged(object sender, TextChangedEventArgs e)
    {
        AtualizarFiltro(e.NewTextValue);
    }

    private void OnCargaSelecionada(object sender, SelectionChangedEventArgs e)
    {
        cargaSelecionada = e.CurrentSelection.FirstOrDefault() as Carga;

        btnEditar.IsEnabled = cargaSelecionada != null;
        btnExcluir.IsEnabled = cargaSelecionada != null;
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        if (cargaSelecionada == null)
            return;

        await Navigation.PushAsync(new EditarCargaPage(cargaSelecionada));
    }

    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        if (cargaSelecionada == null)
            return;

        bool confirmar = await DisplayAlert("Excluir", $"Deseja excluir a carga {cargaSelecionada.NumeroCTE}?", "Sim", "Não");
        if (!confirmar)
            return;

        bool sucesso = await CargaRepository.ExcluirCargaAsync(cargaSelecionada);
        if (sucesso)
        {
            AtualizarFiltro(entryFiltro.Text);
        }
        else
        {
            await DisplayAlert("Erro", "Falha ao excluir carga no Firebase.", "OK");
        }
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Home");
    }
}
