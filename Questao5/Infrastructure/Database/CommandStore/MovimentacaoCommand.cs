using MediatR;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class MovimentacaoCommand : IRequest<Guid>
    {
        public string IdRequisicao { get; set; }
        public int IdConta { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; }
    }
}
