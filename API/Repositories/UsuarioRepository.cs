using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UsuarioRepository
    {
        private readonly DataContext context;

        public UsuarioRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task Adicionar(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task<Usuario> BuscarUsuario(string email)
        {
            return await context.Usuarios.SingleOrDefaultAsync(x => x.Email == email.ToLower());
        }

        public bool VerificarSenha(Usuario usuario, string senha)
        {
            using var hmac = new HMACSHA512(usuario.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i])
                    return false;
            }

            return true;
        }

        public async Task TornarAdmin(Usuario usuario)
        {
            usuario.EhAdmin = true;
            await context.SaveChangesAsync();
        }

        public async Task RemoverAdmin(Usuario usuario)
        {
            usuario.EhAdmin = false;
            await context.SaveChangesAsync();
        }
    }
}