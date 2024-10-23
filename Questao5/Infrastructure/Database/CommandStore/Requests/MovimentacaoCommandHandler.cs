using Dapper;
using Questao5.Domain;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class MovimentacaoCommandHandler {
		private readonly IDbConnection _dbConnection;
		private readonly MovimentacaoValidator _validator;

		public MovimentacaoCommandHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
			_validator = new MovimentacaoValidator();
		}

		public async Task<Guid> Handle(MovimentacaoCommand request, CancellationToken cancellationToken) {
			
			if(request.Valor <= 0)
				throw new Exception("Valor inválido"); 

			if(request.TipoMovimento != "C" && request.TipoMovimento != "D")
				throw new Exception("Tipo de movimento inválido"); 

			var conta = await _dbConnection.QueryFirstOrDefaultAsync<Conta>("SELECT * FROM Conta WHERE Id = @IdConta", new { request.IdConta });
			if(conta == null)
				throw new Exception("Conta inválida");
			if(!conta.Ativa)
				throw new Exception("Conta inativa");

			_validator.Validate(request, conta);

			var movimentoId = Guid.NewGuid();
			string sql = @"INSERT INTO MOVIMENTO (Id, IdConta, Valor, TipoMovimento, DataMovimento) 
                       VALUES (@Id, @IdConta, @Valor, @TipoMovimento, @DataMovimento)";
			await _dbConnection.ExecuteAsync(sql, new {
				Id = movimentoId,
				IdConta = request.IdConta,
				Valor = request.Valor,
				TipoMovimento = request.TipoMovimento,
				DataMovimento = DateTime.Now
			});

			return movimentoId; 
		}
	}
}
