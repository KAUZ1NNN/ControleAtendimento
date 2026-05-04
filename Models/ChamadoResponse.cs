namespace ControleAtendimentos.Models
{
    public class ChamadoResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Setor { get; set; }
        public string Prioridade { get; set; }
        public string Status { get; set; }
        public double TempoAtendimentoHoras { get; set; }
        public bool Atrasado { get; set; }
        public string? Solucao { get; set; }
    }
}