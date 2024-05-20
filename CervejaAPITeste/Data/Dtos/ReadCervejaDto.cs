using System.ComponentModel.DataAnnotations;

namespace CervejaAPITeste.Data.Dtos;

public class ReadCervejaDto
{

    public string Nome { get; set; }
    public string Estilo { get; set; }
    public int IBU { get; set; }
    public double ABV { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
}
