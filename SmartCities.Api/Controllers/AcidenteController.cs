using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("acidentes")]
public class AcidenteController : ControllerBase
{
    // GET /acidentes
    [HttpGet]
    public IActionResult GetAll()
    {
        var lista = new[] {
            new { Id = 1, Local = "Av. Brasil", Gravidade = "Média", Data = DateTime.Parse("2025-05-01T08:30:00Z") },
            new { Id = 2, Local = "Rua das Flores", Gravidade = "Alta", Data = DateTime.Parse("2025-05-02T14:15:00Z") }
        };
        return Ok(lista);
    }

    // GET /acidentes/1
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        if (id == 1)
            return Ok(new { Id = 1, Local = "Av. Brasil", Gravidade = "Média", Data = DateTime.Parse("2025-05-01T08:30:00Z") });
        return NotFound();
    }
}
