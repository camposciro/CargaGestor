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

        entryNumeroCTE.Text = carga.NumeroCTE;
        datePickerDataCarregamento.Date = carga.DataCarregamento;
        entryPesoKg.Text = carga.PesoKg.ToString("F2", CultureInfo.InvariantCulture);

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

        bool sucesso = await CargaRepository.AtualizarCargaAsync(carga);
        if (sucesso)
        {
            await DisplayAlert("Sucesso", "Carga atualizada com sucesso!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await DisplayAlert("Erro", "Falha ao atualizar a carga na nuvem.", "OK");
        }
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
