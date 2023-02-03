using ExercicioSemana2.Context;
using ExercicioSemana2.Dto;
using ExercicioSemana2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExercicioSemana2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly RhContext _rhContext;
        private readonly ITokenService _tokenService;

        public AutenticacaoController(RhContext rhContext, ITokenService tokenService)
        {
            _rhContext = rhContext;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> AutenticacaoAsync([FromBody] FuncionarioDto dto)
        {
            var funcionario = await _rhContext.Funcionarios.Include(x => x.Permissao)
                .FirstOrDefaultAsync(x => x.Email == dto.Email && x.Senha == dto.Senha);
            if (funcionario is null)
            {
                return BadRequest(new { Message = "Funcionário não encontrado." });
            }
            var token = _tokenService.GerarToken(funcionario);

            return Ok(token);
        }
    }
}
