using Microsoft.Maui.Controls;
using System;

namespace CargaGestor;

public partial class CadastroCargaPage : ContentPage
{
    public CadastroCargaPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LimparCampos();
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        string cte = entryNumeroCTE.Text;
        string pesoTexto = entryPeso.Text;
        string status = pickerStatus.SelectedItem as string;
        string valeOpcao = pickerVale.SelectedItem as string;

        if (string.IsNullOrWhiteSpace(cte) ||
            string.IsNullOrWhiteSpace(pesoTexto) ||
            string.IsNullOrWhiteSpace(status) ||
            string.IsNullOrWhiteSpace(valeOpcao))
        {
            await DisplayAlert("Erro", "Preencha todos os campos obrigatórios.", "OK");
            return;
        }

        if (!double.TryParse(pesoTexto.Replace(",", "."), out double pesoKg))
        {
            await DisplayAlert("Erro", "Peso inválido. Use apenas números.", "OK");
            return;
        }

        var novaCarga = new Carga
        {
            NumeroCTE = cte,
            DataCarregamento = datePickerDataCarregamento.Date,
            PesoKg = pesoKg,
            Status = status,
            ValeOpcao = pickerVale.SelectedItem as string  // Corrigido aqui
        };

        CargaRepository.AdicionarCarga(novaCarga);

        await DisplayAlert("Sucesso", "Carga salva com sucesso!", "OK");

        LimparCampos();

        await Shell.Current.GoToAsync("//ListarCargas");
    }

    private void LimparCampos()
    {
        entryNumeroCTE.Text = string.Empty;
        entryPeso.Text = string.Empty;
        pickerStatus.SelectedIndex = 0;
        pickerVale.SelectedIndex = 0; // ✅ Resetando o picker do Vale
        datePickerDataCarregamento.Date = DateTime.Now;
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Home");
    }
}
