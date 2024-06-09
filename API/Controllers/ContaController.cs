using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ContaController : DryController
    {
        private readonly DataContext context;

        public ContaController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost("registrar")] // rota: api/conta/registrar
        public async Task<ActionResult<Usuario>> Registrar(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.Username)) return BadRequest("Usuário já está sendo utilizado. Tente novamente!");

            using var hmac = new HMACSHA512();

            var novoUsuario = new Usuario
            {
                Username = registroDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
                PasswordSalt = hmac.Key
            };

            context.Usuarios.Add(novoUsuario);
            await context.SaveChangesAsync();
            return novoUsuario;
        }

        [HttpPost("login")] // rota: api/conta/login
        public async Task<ActionResult<Usuario>> Login(LoginDto loginDto) 
        {
            var usuario = await context.Usuarios.SingleOrDefaultAsync(x => x.Username == loginDto.Username);

            if (usuario == null) return Unauthorized("Usuário não encontrado!");

            using var hmac = new HMACSHA512(usuario.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i]) return Unauthorized("Senha inválida!");
            }

            return usuario;

        }


        private async Task<bool> UsuarioExiste(string username)
        {
            return await context.Usuarios.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}