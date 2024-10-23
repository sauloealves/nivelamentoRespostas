using MediatR;

namespace Questao5.Infrastructure.Database.QueryStore {
	public class ConsultaSaldoQuery :IRequest<decimal> {
		public int IdConta { get; set; }
	}
}
