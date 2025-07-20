using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CargaGestor;

public class Carga : INotifyPropertyChanged
{
    private string numeroCTE;
    private DateTime dataCarregamento;
    private double pesoKg;
    private string status;
    private string valeOpcao;

    public event PropertyChangedEventHandler PropertyChanged;

    public int Id { get; set; }

    public string NumeroCTE
    {
        get => numeroCTE;
        set
        {
            if (numeroCTE != value)
            {
                numeroCTE = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime DataCarregamento
    {
        get => dataCarregamento;
        set
        {
            if (dataCarregamento != value)
            {
                dataCarregamento = value;
                OnPropertyChanged();
            }
        }
    }

    public double PesoKg
    {
        get => pesoKg;
        set
        {
            if (pesoKg != value)
            {
                pesoKg = value;
                OnPropertyChanged();
            }
        }
    }

    public string Status
    {
        get => status;
        set
        {
            if (status != value)
            {
                status = value;
                OnPropertyChanged();
            }
        }
    }

    public string ValeOpcao
    {
        get => valeOpcao;
        set
        {
            if (valeOpcao != value)
            {
                valeOpcao = value;
                OnPropertyChanged();
            }
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string? nomePropriedade = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
    }
}
