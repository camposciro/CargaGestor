using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace CargaGestor;

public partial class ListarCargasPage : ContentPage
{
    public ObservableCollection<Carga> Cargas => CargaRepository.Cargas;

    private ObservableCollection<Carga> cargasFiltradas;
    private Carga cargaSelecionada;

    public ListarCargasPage()
    {
        InitializeComponent();

        cargasFiltradas = new ObservableCollection<Carga>(Cargas);
        collectionViewCargas.ItemsSource = cargasFiltradas;

        btnEditar.IsEnabled = false;
        btnExcluir.IsEnabled = false;

        Cargas.CollectionChanged += (s, e) => AtualizarFiltro();
        AtualizarFiltro();
    }

    private void AtualizarFiltro(string filtro = "")
    {
        filtro = filtro?.ToLower() ?? "";

        cargasFiltradas.Clear();

        var filtradas = Cargas.Where(c =>
            c.NumeroCTE.ToLower().Contains(filtro) ||
            c.Status.ToLower().Contains(filtro));

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

        // Navegar usando Navigation.PushAsync para poder passar o objeto diretamente no construtor
        await Navigation.PushAsync(new EditarCargaPage(cargaSelecionada));
    }

    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        if (cargaSelecionada == null)
            return;

        bool confirmar = await DisplayAlert("Excluir", $"Excluir carga {cargaSelecionada.NumeroCTE}?", "Sim", "Não");
        if (!confirmar)
            return;

        Cargas.Remove(cargaSelecionada);
        AtualizarFiltro(entryFiltro.Text);
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Home");
    }
}
