using Microsoft.AspNetCore.Mvc;
using Npgsql;

[ApiController]
[Route("api/[controller]")]
public class PrioridadesController : ControllerBase
{
    private readonly string _connectionString;

    public PrioridadesController(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    [HttpGet]
    public IActionResult Get()
    {
        var lista = new List<object>();

        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        var cmd = new NpgsqlCommand("SELECT id, nome FROM prioridades", conn);
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new
            {
                id = reader.GetInt32(0),
                nome = reader.GetString(1)
            });
        }

        return Ok(lista);
    }
}