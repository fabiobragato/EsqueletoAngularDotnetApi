using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // Exemplo de rota: https://localhost/api/usuarios
public class UsuariosController : ControllerBase
{
    private readonly DataContext context;

    public UsuariosController(DataContext context)
    {
        this.context = context;
    }

    [HttpGet] // Método GET /api/usuarios
    public async Task<ActionResult<IEnumerable<Usuario>>> listaUsuarios()
    {
        return await context.Usuarios.ToListAsync();
    }

    [HttpGet("{id}")] // Método GET /api/usuarios/{id}
    public async Task<ActionResult<Usuario>> obterUsuario(int id)
    {
        return await context.Usuarios.FindAsync(id);
    }
}
