using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ContaController : DryController
    {
        private readonly DataContext context;
        private readonly ITokenService tokenService;

        public ContaController(DataContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }

        [HttpPost("registrar")] // rota: api/conta/registrar
        public async Task<ActionResult<UsuarioDto>> Registrar(RegistroDto registroDto)
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
            return new UsuarioDto
            {
                Username = novoUsuario.Username,
                Token = tokenService.CreateToken(novoUsuario)
            };
        }

        [HttpPost("login")] // rota: api/conta/login
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await context.Usuarios.SingleOrDefaultAsync(x => x.Username == loginDto.Username);

            if (usuario == null) return Unauthorized("Usuário não encontrado!");

            using var hmac = new HMACSHA512(usuario.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i]) return Unauthorized("Senha inválida!");
            }

            return new UsuarioDto
            {
                Username = usuario.Username,
                Token = tokenService.CreateToken(usuario)
            };

        }


        private async Task<bool> UsuarioExiste(string username)
        {
            return await context.Usuarios.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}