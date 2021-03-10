using System;
using My.Bank.Enum;

namespace My.Bank.Classes
{
    public class Conta
    {
        private TipoCadastro TipoCadastro { get; set; }
        private TipoConta Tipo { get; set; }
        private double Saldo { get; set; }
        private double Credito { get; set; }
        private string Nome { get; set; }

        public Conta(TipoCadastro tipoCadastro, TipoConta tipo, double saldo, double credito, string nome)
        // public Conta(double saldo, double credito, string nome)
        {
            this.TipoCadastro = tipoCadastro;
            this.Tipo = tipo;
            this.Saldo = saldo;
            this.Credito = credito;
            this.Nome = nome;
        }

        public string GetNome(){
            return this.Nome;
        }

        public double GetSaldo(){
            return this.Saldo;
        }

        public double GetCredito(){
            return this.Credito;
        }

         public bool Sacar(double valorSaque){
            if (this.Saldo - valorSaque < (this.Credito * -1)){
                Console.WriteLine("Saldo Insuficiente.");
                return false;
            }

            this.Saldo -= valorSaque;
            return true;           
        }

        public void Depositar(double valorDeposito){
            this.Saldo += valorDeposito;
        }

        public void Transferir(double valorTransferencia, Conta contaDestino){
            if (this.Sacar(valorTransferencia)){
                contaDestino.Depositar(valorTransferencia);
            }
        }

        // Sobreescrevendo o metodo.
        public override string ToString()
        {
            string retorno = "";

            retorno += "Tipo " + this.Tipo + " | ";
            retorno += "Nome " + this.Nome + " | ";
            retorno += "Saldo " + this.Saldo + " | ";
            retorno += "Credito " + this.Credito + " | ";
            retorno += "Cadstro " +this.TipoCadastro;

            return retorno;
        }


    }
}