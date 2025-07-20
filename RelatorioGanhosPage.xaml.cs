using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CargaGestor;

public partial class RelatorioGanhosPage : ContentPage
{
    private ObservableCollection<CargaComGanhos> cargasFiltradas = new ObservableCollection<CargaComGanhos>();

    public RelatorioGanhosPage()
    {
        InitializeComponent();

        datePickerInicio.Date = DateTime.Today.AddMonths(-1);
        datePickerFim.Date = DateTime.Today;

        collectionViewRelatorio.ItemsSource = cargasFiltradas;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AtualizarRelatorio();
    }

    private void AtualizarRelatorio(string filtroTexto = "")
    {
        filtroTexto = filtroTexto?.ToLower() ?? "";

        var cargas = CargaRepository.Cargas.Where(c =>
            c.DataCarregamento.Date >= datePickerInicio.Date &&
            c.DataCarregamento.Date <= datePickerFim.Date &&
            (c.NumeroCTE.ToLower().Contains(filtroTexto) || c.Status.ToLower().Contains(filtroTexto))
        ).ToList();

        cargasFiltradas.Clear();

        double totalGanhos = 0;

        foreach (var carga in cargas)
        {
            double ganhoBruto = carga.PesoKg * 120 * 0.00011;
            double ganhoLiquido = ganhoBruto;

            if (carga.ValeOpcao == "Com Vale")
                ganhoLiquido -= 80;

            if (ganhoLiquido < 0)
                ganhoLiquido = 0;

            var cargaComGanhos = new CargaComGanhos
            {
                Id = carga.Id,
                NumeroCTE = carga.NumeroCTE,
                DataCarregamento = carga.DataCarregamento,
                PesoKg = carga.PesoKg,
                Status = carga.Status,
                ValeOpcao = carga.ValeOpcao,
                GanhoBruto = ganhoBruto,
                GanhoLiquido = ganhoLiquido
            };

            cargasFiltradas.Add(cargaComGanhos);
            totalGanhos += ganhoLiquido;
        }

        lblTotalCargas.Text = cargasFiltradas.Count.ToString();
        lblTotalGanhos.Text = totalGanhos.ToString("F2");
    }

    private void OnFiltroTextChanged(object sender, TextChangedEventArgs e)
    {
        AtualizarRelatorio(e.NewTextValue);
    }

    private void OnFiltrarClicked(object sender, EventArgs e)
    {
        AtualizarRelatorio(entryFiltro.Text);
    }

    private void OnLimparFiltrosClicked(object sender, EventArgs e)
    {
        entryFiltro.Text = string.Empty;
        datePickerInicio.Date = DateTime.Today.AddMonths(-1);
        datePickerFim.Date = DateTime.Today;
        AtualizarRelatorio();
    }
}

// Classe auxiliar para exibição no relatório
public class CargaComGanhos : Carga
{
    public double GanhoBruto { get; set; }
    public double GanhoLiquido { get; set; }
}
