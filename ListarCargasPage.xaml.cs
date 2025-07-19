using System.Collections.ObjectModel;

namespace CargaGestor;

public partial class ListarCargasPage : ContentPage
{
    public ObservableCollection<Carga> Cargas => CargaRepository.Cargas;

    public ListarCargasPage()
    {
        InitializeComponent();

        BindingContext = this;
    }
}
