using System.Text.Json.Serialization;

namespace ControleAtendimentos.Models
{
   public class Chamado
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string? Descricao { get; set; }
    public int SetorId { get; set; }
    public int PrioridadeId { get; set; }

[JsonIgnore]
public string Status { get; set; } = "Aberto";

    public DateTime DataCriacao { get; set; }
}
}