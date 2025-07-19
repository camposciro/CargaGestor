using System.Collections.ObjectModel;

namespace CargaGestor;

public static class CargaRepository
{
    public static ObservableCollection<Carga> Cargas { get; } = new ObservableCollection<Carga>();

    public static void AdicionarCarga(Carga carga)
    {
        Cargas.Add(carga);
    }
}
