using API.DTOs;
using API.Exceptions;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ContasController : DryController
    {
        private readonly UsuarioService usuarioService;

        public ContasController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost("registros")] // rota: contas/registros
        public async Task<ActionResult<UsuarioDto>> Registros(RegistroDto registroDto)
        {
            try
            {
                var usuarioDto = await usuarioService.Registrar(registroDto);
                return usuarioDto;
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logins")] // rota: contas/logins
        public async Task<ActionResult<UsuarioDto>> Logins(LoginDto loginDto)
        {
            try
            {
                var usuarioDto = await usuarioService.Logar(loginDto);
                return usuarioDto;
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("admins/adiciona-emails/{adicionaEmailId}")] // rota: contas/admins-criacao/{adminCriacaoId}
        public async Task<ActionResult> RegistrosAdmin(string adicionaEmailId)
        {
            try
            {
                await usuarioService.RegistrarAdmin(adicionaEmailId);
                return Ok($"{adicionaEmailId} agora é um administrador!");
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("admins/remove-emails/{removeEmailId}")] // rota: contas/admins/exclusoes-admin/{Id}
        public async Task<ActionResult> ExclusoesAdmin(string removeEmailId)
        {
            try
            {
                await usuarioService.RemoverAdmin(removeEmailId);
                return Ok($"{removeEmailId} não é mais um administrador!");
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}