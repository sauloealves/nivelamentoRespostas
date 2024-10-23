using Dapper;
using MediatR;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Database.QueryStore.Requests {
	public class ConsultaSaldoQueryHandler :IRequestHandler<ConsultaSaldoQuery, decimal> {
		private readonly IDbConnection _dbConnection;

		public ConsultaSaldoQueryHandler(IDbConnection dbConnection) {
			_dbConnection = dbConnection;
		}

		public async Task<decimal> Handle(ConsultaSaldoQuery request, CancellationToken cancellationToken) {
			var conta = await _dbConnection.QueryFirstOrDefaultAsync<Conta>("SELECT * FROM Conta WHERE Id = @IdConta", new { request.IdConta });

			if(conta == null) {
				throw new Exception("Conta não encontrada"); 
			}
			
			string sql = @"SELECT 
                          SUM(CASE WHEN TipoMovimento = 'C' THEN Valor ELSE 0 END) - 
                          SUM(CASE WHEN TipoMovimento = 'D' THEN Valor ELSE 0 END) AS Saldo
                       FROM MOVIMENTO 
                       WHERE IdConta = @IdConta";

			var saldo = await _dbConnection.ExecuteScalarAsync<decimal>(sql, new { IdConta = request.IdConta });

			return saldo; 
		}
	}
}
