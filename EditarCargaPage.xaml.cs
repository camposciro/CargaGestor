using Microsoft.Maui.Controls;
using System;

namespace CargaGestor;

public partial class EditarCargaPage : ContentPage
{
    private Carga carga;

    public EditarCargaPage(Carga carga)
    {
        InitializeComponent();
        this.carga = carga;

        // Preencher campos com dados existentes
        entryNumeroCTE.Text = carga.NumeroCTE;
        datePickerDataCarregamento.Date = carga.DataCarregamento;
        entryPesoKg.Text = carga.PesoKg.ToString("F2");

        // Selecionar no picker o status atual da carga
        switch (carga.Status)
        {
            case "Em Viagem":
                pickerStatus.SelectedIndex = 0;
                break;
            case "Aguardando Descarga":
                pickerStatus.SelectedIndex = 1;
                break;
            case "Finalizada":
                pickerStatus.SelectedIndex = 2;
                break;
            default:
                pickerStatus.SelectedIndex = -1;
                break;
        }
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryNumeroCTE.Text) ||
            string.IsNullOrWhiteSpace(entryPesoKg.Text) ||
            pickerStatus.SelectedIndex == -1)
        {
            await DisplayAlert("Erro", "Preencha todos os campos e selecione um status.", "OK");
            return;
        }

        carga.NumeroCTE = entryNumeroCTE.Text;
        carga.DataCarregamento = datePickerDataCarregamento.Date;

        if (double.TryParse(entryPesoKg.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture, out double peso))
            carga.PesoKg = peso;
        else
        {
            await DisplayAlert("Erro", "Peso inválido. Use formato numérico.", "OK");
            return;
        }

        carga.Status = pickerStatus.Items[pickerStatus.SelectedIndex];

        await Shell.Current.GoToAsync(".."); // Voltar para a página anterior
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // Voltar sem salvar
    }
}
