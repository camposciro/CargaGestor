using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace CargaGestor;

public partial class RelatorioGanhosPage : ContentPage
{
    private ObservableCollection<Carga> cargasFiltradas = new ObservableCollection<Carga>();

    public RelatorioGanhosPage()
    {
        InitializeComponent();

        collectionViewRelatorio.ItemsSource = cargasFiltradas;

        entryFiltro.TextChanged += OnFiltroTextChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await CargaRepository.CarregarCargasAsync();

        datePickerInicio.Date = DateTime.Today.AddDays(-30);
        datePickerFim.Date = DateTime.Today;

        AplicarFiltros();
    }

    private void OnFiltroTextChanged(object sender, TextChangedEventArgs e)
    {
        AplicarFiltros();
    }

    private void OnFiltrarClicked(object sender, EventArgs e)
    {
        AplicarFiltros();
    }

    private void OnLimparFiltrosClicked(object sender, EventArgs e)
    {
        entryFiltro.Text = string.Empty;
        datePickerInicio.Date = DateTime.Today.AddDays(-30);
        datePickerFim.Date = DateTime.Today;

        AplicarFiltros();
    }

    private void AplicarFiltros()
    {
        string filtroTexto = entryFiltro.Text?.ToLower() ?? "";
        DateTime inicio = datePickerInicio.Date.Date;
        DateTime fim = datePickerFim.Date.Date.AddDays(1).AddSeconds(-1); // fim do dia

        cargasFiltradas.Clear();

        var filtradas = CargaRepository.Cargas
            .Where(c =>
                (c.NumeroCTE?.ToLower().Contains(filtroTexto) ?? false) ||
                (c.Status?.ToLower().Contains(filtroTexto) ?? false))
            .Where(c => c.DataCarregamento >= inicio && c.DataCarregamento <= fim)
            .ToList();

        foreach (var carga in filtradas)
            cargasFiltradas.Add(carga);

        AtualizarResumo();
    }

    private void AtualizarResumo()
    {
        lblTotalCargas.Text = cargasFiltradas.Count.ToString();

        double total = cargasFiltradas.Sum(c => c.GanhoLiquido);
        lblTotalGanhos.Text = total.ToString("N2", new CultureInfo("pt-BR"));
    }
}
