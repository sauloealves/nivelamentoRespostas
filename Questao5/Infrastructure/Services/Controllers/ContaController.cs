using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Infrastructure.Database.CommandStore;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
		private readonly IMediator _mediator;

		public ContaController(IMediator mediator) {
			_mediator = mediator;
		}

		[HttpPost("movimentacao")]
		public async Task<IActionResult> Movimentacao([FromBody] MovimentacaoCommand request) {
			try {
				var movimentoId = await _mediator.Send(request);
				return Ok(new { IdMovimento = movimentoId });
			} catch(Exception ex) {
				return BadRequest(new {
					Mensagem = ex.Message,
					Tipo = ex.Message.Contains("INVALID_ACCOUNT") ? "INVALID_ACCOUNT" :
						   ex.Message.Contains("INACTIVE_ACCOUNT") ? "INACTIVE_ACCOUNT" :
						   ex.Message.Contains("INVALID_VALUE") ? "INVALID_VALUE" :
						   ex.Message.Contains("INVALID_TYPE") ? "INVALID_TYPE" :
						   "UNKNOWN_ERROR"
				});
			}
		}
	}
        
}