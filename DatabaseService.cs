using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace CargaGestor;

public class DatabaseService
{
    private static SQLiteAsyncConnection _database;

    public static async Task InitializeAsync()
    {
        if (_database != null)
            return;

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cargagestor.db3");
        _database = new SQLiteAsyncConnection(dbPath);

        await _database.CreateTableAsync<Carga>();
    }

    public static Task<List<Carga>> GetCargasAsync()
    {
        return _database.Table<Carga>().ToListAsync();
    }

    public static Task<int> AddCargaAsync(Carga carga)
    {
        return _database.InsertAsync(carga);
    }

    public static Task<int> UpdateCargaAsync(Carga carga)
    {
        return _database.UpdateAsync(carga);
    }

    public static Task<int> DeleteCargaAsync(Carga carga)
    {
        return _database.DeleteAsync(carga);
    }
}
