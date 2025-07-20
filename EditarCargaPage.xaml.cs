using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace CargaGestor;

public partial class EditarCargaPage : ContentPage
{
    private Carga carga;

    public EditarCargaPage(Carga carga)
    {
        InitializeComponent();
        this.carga = carga;

        // Preenche os campos com os dados existentes
        entryNumeroCTE.Text = carga.NumeroCTE;
        datePickerDataCarregamento.Date = carga.DataCarregamento;
        entryPesoKg.Text = carga.PesoKg.ToString("F2", CultureInfo.InvariantCulture);

        // Define o status atual no Picker
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

        // Define a opção atual de Vale
        switch (carga.ValeOpcao)
        {
            case "Sem Vale":
                pickerValeOpcao.SelectedIndex = 0;
                break;
            case "Com Vale":
                pickerValeOpcao.SelectedIndex = 1;
                break;
            default:
                pickerValeOpcao.SelectedIndex = -1;
                break;
        }
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryNumeroCTE.Text) ||
            string.IsNullOrWhiteSpace(entryPesoKg.Text) ||
            pickerStatus.SelectedIndex == -1 ||
            pickerValeOpcao.SelectedIndex == -1)
        {
            await DisplayAlert("Erro", "Preencha todos os campos corretamente.", "OK");
            return;
        }

        carga.NumeroCTE = entryNumeroCTE.Text;
        carga.DataCarregamento = datePickerDataCarregamento.Date;

        // Conversão segura do peso
        if (double.TryParse(entryPesoKg.Text.Replace(",", "."), CultureInfo.InvariantCulture, out double peso))
        {
            carga.PesoKg = peso;
        }
        else
        {
            await DisplayAlert("Erro", "Peso inválido. Use apenas números (ex: 41500 ou 41500,50).", "OK");
            return;
        }

        carga.Status = pickerStatus.Items[pickerStatus.SelectedIndex];
        carga.ValeOpcao = pickerValeOpcao.Items[pickerValeOpcao.SelectedIndex];

        await Shell.Current.GoToAsync(".."); // Voltar para a página anterior
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // Voltar sem salvar
    }
}
