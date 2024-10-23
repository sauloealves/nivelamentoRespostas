using System;
using System.Globalization;

namespace Questao1 {
	public class ContaBancaria {
		public int NumeroConta { get; private set; }
		public string Titular { get; set; }
		public double Saldo { get; private set; }

		private const double TaxaSaque = 3.50;

		public ContaBancaria(int numeroConta, string titular, double depositoInicial = 0) {
			NumeroConta = numeroConta;
			Titular = titular;
			Saldo = depositoInicial;
		}

		public void Deposito(double valor) {
			Saldo += valor;
		}

		public void Saque(double valor) {
			Saldo -= valor + TaxaSaque;
		}

		public override string ToString() {
			return $"Conta {NumeroConta}, Titular: {Titular}, Saldo: $ {Saldo:F2}";
		}
	}

}
