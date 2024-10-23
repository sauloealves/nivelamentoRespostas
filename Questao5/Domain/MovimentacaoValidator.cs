using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore;

namespace Questao5.Domain {
	public class MovimentacaoValidator {
		public void Validate(MovimentacaoCommand request, Conta conta) {
			if(conta == null) {
				throw new Exception("Conta não encontrada. TIPO: INVALID_ACCOUNT");
			}

			if(!conta.Ativa) {
				throw new Exception("A conta está inativa. TIPO: INACTIVE_ACCOUNT");
			}

			if(request.Valor <= 0) {
				throw new Exception("O valor deve ser positivo. TIPO: INVALID_VALUE");
			}

			if(request.TipoMovimento != "C" && request.TipoMovimento != "D") {
				throw new Exception("O tipo de movimento deve ser 'C' ou 'D'. TIPO: INVALID_TYPE");
			}
		}
	}
}
