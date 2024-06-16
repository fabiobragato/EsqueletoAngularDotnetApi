using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Entities;
using API.Exceptions;
using API.Interfaces;
using API.Repositories;

namespace API.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository usuarioRepository;
        private readonly ITokenService tokenService;

        public UsuarioService(UsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            this.usuarioRepository = usuarioRepository;
            this.tokenService = tokenService;
        }

        public async Task<UsuarioDto> Registrar(RegistroDto registroDto)
        {
            var usuario = await usuarioRepository.BuscarUsuario(registroDto.Email);

            if (usuario != null)
            {
                throw new BadRequestException("E-mail já está sendo utilizado. Tente novamente!");
            }

            using var hmac = new HMACSHA512();

            var novoUsuario = new Usuario
            {
                Email = registroDto.Email.ToLower(),
                EmailConfirmado = false,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Senha)),
                PasswordSalt = hmac.Key,
                Celular = registroDto.Celular,
                EhAdmin = registroDto.EhAdmin
            };

            await usuarioRepository.Adicionar(novoUsuario);

            return new UsuarioDto
            {
                Email = novoUsuario.Email,
                EhAdmin = novoUsuario.EhAdmin,
                Token = tokenService.CreateToken(novoUsuario)
            };
        }

        public async Task<UsuarioDto> Logar(LoginDto loginDto)
        {
            var usuario = await usuarioRepository.BuscarUsuario(loginDto.Email);

            if (usuario == null)
            {
                throw new BadRequestException("E-mail não cadastrado. Tente novamente!");
            }

            if (!usuarioRepository.VerificarSenha(usuario, loginDto.Senha))
            {
                throw new BadRequestException("Senha inválida!");
            }

            return new UsuarioDto
            {
                Email = usuario.Email,
                EhAdmin = usuario.EhAdmin,
                Token = tokenService.CreateToken(usuario)
            };
        }

        public async Task RegistrarAdmin(string email)
        {
            var usuario = await usuarioRepository.BuscarUsuario(email);

            if (usuario == null)
            {
                throw new BadRequestException("E-mail não encontrado. Tente novamente!");
            }

            await usuarioRepository.TornarAdmin(usuario);
        }

        public async Task RemoverAdmin(string email)
        {
            var usuario = await usuarioRepository.BuscarUsuario(email);

            if (usuario == null)
            {
                throw new BadRequestException("E-mail não encontrado. Tente novamente!");
            }

            await usuarioRepository.RemoverAdmin(usuario);
        }

    }
}