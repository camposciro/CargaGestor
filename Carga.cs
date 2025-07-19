using CargaGestor;

namespace CargaGestor;

public class Carga
{
    public string DataCarregamento { get; set; } = string.Empty;
    public string NumeroCTE { get; set; } = string.Empty;
    public double PesoKg { get; set; }
    public string Status { get; set; } = "Em Percurso";
}
