using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ControleAtendimentos.Models;

namespace ControleAtendimentos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly string _connectionString;

        public ChamadosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public IActionResult Criar(Chamado chamado)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(@"
                INSERT INTO chamados 
                (titulo, descricao, setor_id, prioridade_id, status) 
                VALUES 
                (@titulo, @descricao, @setor, @prioridade, 'Aberto')
            ", conn);

            cmd.Parameters.AddWithValue("titulo", chamado.Titulo);
            cmd.Parameters.AddWithValue("descricao", (object?)chamado.Descricao ?? DBNull.Value);
            cmd.Parameters.AddWithValue("setor", chamado.SetorId);
            cmd.Parameters.AddWithValue("prioridade", chamado.PrioridadeId);

            cmd.ExecuteNonQuery();

            return Ok("Chamado criado com status ABERTO");
        }
        [HttpPost("{id}/iniciar")]
        public IActionResult Iniciar(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var checkCmd = new NpgsqlCommand("SELECT status FROM chamados WHERE id = @id", conn);
            checkCmd.Parameters.AddWithValue("id", id);

            var status = checkCmd.ExecuteScalar()?.ToString();

            if (status == null)
                return NotFound("Chamado não encontrado");

            if (status != "Aberto")
                return BadRequest("Chamado não pode ser iniciado");

            var updateCmd = new NpgsqlCommand(@"
        UPDATE chamados 
        SET status = 'EmAtendimento' 
        WHERE id = @id
    ", conn);

            updateCmd.Parameters.AddWithValue("id", id);
            updateCmd.ExecuteNonQuery();

            var atendimentoCmd = new NpgsqlCommand(@"
        INSERT INTO atendimentos (chamado_id, data_inicio)
        VALUES (@id, NOW())
    ", conn);

            atendimentoCmd.Parameters.AddWithValue("id", id);
            atendimentoCmd.ExecuteNonQuery();

            return Ok("Atendimento iniciado");
        }
        [HttpPost("{id}/finalizar")]
        public IActionResult Finalizar(int id, [FromBody] string solucao)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var checkCmd = new NpgsqlCommand("SELECT status FROM chamados WHERE id = @id", conn);
            checkCmd.Parameters.AddWithValue("id", id);

            var status = checkCmd.ExecuteScalar()?.ToString();

            if (status == null)
                return NotFound("Chamado não encontrado");

            if (status != "EmAtendimento")
                return BadRequest("Chamado não pode ser finalizado");

            var updateCmd = new NpgsqlCommand(@"
        UPDATE chamados 
        SET status = 'Finalizado' 
        WHERE id = @id
    ", conn);

            updateCmd.Parameters.AddWithValue("id", id);
            updateCmd.ExecuteNonQuery();

            var atendimentoCmd = new NpgsqlCommand(@"
        UPDATE atendimentos
        SET data_fim = NOW(), solucao = @solucao
        WHERE chamado_id = @id
    ", conn);

            atendimentoCmd.Parameters.AddWithValue("id", id);
            atendimentoCmd.Parameters.AddWithValue("solucao", solucao);

            atendimentoCmd.ExecuteNonQuery();

            return Ok("Atendimento finalizado");
        }
        [HttpGet]
        public IActionResult Listar()
        {
            var lista = new List<ChamadoResponse>();

            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(@"
        SELECT 
            c.id,
            c.titulo,
            s.nome AS setor,
            p.nome AS prioridade,
            c.status,
            a.data_inicio,
            a.data_fim,
            a.solucao,
            p.tempo_estimado_horas
        FROM chamados c
        JOIN setores s ON c.setor_id = s.id
        JOIN prioridades p ON c.prioridade_id = p.id
        LEFT JOIN atendimentos a ON a.chamado_id = c.id
    ", conn);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var dataInicio = reader["data_inicio"] as DateTime?;
                var dataFim = reader["data_fim"] as DateTime?;
                var tempoEstimado = Convert.ToInt32(reader["tempo_estimado_horas"]);

                double tempoHoras = 0;

                if (dataInicio != null && dataFim != null)
                {
                    tempoHoras = (dataFim.Value - dataInicio.Value).TotalHours;
                }

                bool atrasado = tempoHoras > tempoEstimado;

                lista.Add(new ChamadoResponse
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Titulo = reader["titulo"].ToString(),
                    Setor = reader["setor"].ToString(),
                    Prioridade = reader["prioridade"].ToString(),
                    Status = reader["status"].ToString(),
                    TempoAtendimentoHoras = Math.Round(tempoHoras, 2),
                    Atrasado = atrasado,
                    Solucao = reader["solucao"]?.ToString()
                });
            }

            return Ok(lista);
        }

    }
}