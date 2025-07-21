using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CargaGestor;

public static class CargaRepository
{
    public static ObservableCollection<Carga> Cargas { get; } = new ObservableCollection<Carga>();

    public static async Task<bool> CarregarCargasAsync()
    {
        await DatabaseService.InitializeAsync();
        var lista = await DatabaseService.GetCargasAsync();
        Cargas.Clear();
        foreach (var c in lista)
            Cargas.Add(c);
        return true;
    }

    public static async Task<bool> AdicionarCargaAsync(Carga carga)
    {
        await DatabaseService.InitializeAsync();
        int resultado = await DatabaseService.AddCargaAsync(carga);
        if (resultado > 0)
        {
            Cargas.Add(carga);
            return true;
        }
        return false;
    }

    public static async Task<bool> AtualizarCargaAsync(Carga carga)
    {
        await DatabaseService.InitializeAsync();
        int resultado = await DatabaseService.UpdateCargaAsync(carga);
        return resultado > 0;
    }

    public static async Task<bool> ExcluirCargaAsync(Carga carga)
    {
        await DatabaseService.InitializeAsync();
        int resultado = await DatabaseService.DeleteCargaAsync(carga);
        if (resultado > 0)
        {
            Cargas.Remove(carga);
            return true;
        }
        return false;
    }
}
