using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CervejaAPITeste.Models;
[Table("Cerveja")]
public class Cerveja
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é campo obrigatório")]
    [MinLength(5, ErrorMessage = "")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Estilo é campo obrigatório")]
    public string Estilo { get; set; }
    [Required(ErrorMessage = "IBU é campo obrigatório")]
    [Range(0, 120, ErrorMessage = "O IBU deve estar entre 0 a 120.")]
    public int IBU { get; set; }
    [Required(ErrorMessage = "ABV é campo obrigatório")]
    [Range(0, 80, ErrorMessage = "O teor alcoólico deve der dentre 0 e 80")]
    public double ABV { get; set; }
}