using System;
using System.Collections.Generic;
using System.IO;
using My.Bank.Classes;
using My.Bank.Enum;

namespace My.Bank
{
    class Program
    {
        // Criando Lista para guardar as contas
        static List<Conta> listContas = new List<Conta>();

        static void Main(string[] args)
        {
           // Gerando lista de Favorecidos atraves de arquivo texto delimitado.
            CarregarFavorecidos(listContas);

            string opcao = Menu();

            while(opcao.ToUpper() !="X"){
                switch(opcao){
                    case "1":
                        InserirConta();
                        break;
                    case "2":
                        ConsultarSaldo();
                        break;
                    case "3":
                        listarFavorecidos();
                        break;
                    case "4":
                        Transferir();
                        break;
                    case "5":
                        Sacar();
                        break;
                    case "6":
                        Depositar();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcao = Menu().ToUpper();
            }

            Console.Clear();
            Console.WriteLine("Obrigado por utilizar os servicos My Bank.");
            Console.WriteLine();             
        }

        private static string Menu()
        {
            HeaderMenu("Bem Vindo ao My Bank !");

            Console.WriteLine("Informe a opcao desejada:");

            Console.WriteLine("1: Inserir Conta");
            Console.WriteLine("2: Consultar Saldo");
            Console.WriteLine("3: Listar Favorecidos");
            Console.WriteLine("4: Transferir");
            Console.WriteLine("5: Sacar");
            Console.WriteLine("6: Depositar");
            Console.WriteLine("X: Sair");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static void HeaderMenu(string mensagem){
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("##########################");
            Console.WriteLine($"  {mensagem.ToUpper()}");
            Console.WriteLine("##########################");
            Console.WriteLine();
        }

        private static void FooterMenu(){
            Console.WriteLine();
            Console.WriteLine("Pressione Enter para continuar... ");
            Console.ReadKey();
            // Console.ReadLine();
        }

        private static void CarregarFavorecidos(List<Conta> listContas){
            String line;

            try
            {
                // Obtendo caminho do arquivo
                StreamReader sr = new StreamReader(@".\\FileInput\\favoritos.txt");

                //Lendo primeira linha do arquivo
                line = sr.ReadLine();
                
                while (line != null)
                {
                    string[] dados = line.Split(",");
                    listContas.Add(
                        new Conta(
                            tipoCadastro: (TipoCadastro)int.Parse(dados[0]),
                            tipo: (TipoConta)int.Parse(dados[1]),
                            nome: dados[2],
                            saldo: double.Parse(dados[3]),
                            credito: double.Parse(dados[4]))
                        );
                    line = sr.ReadLine();
                }
          
                sr.Close();
             }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Final da Listagem.");
            }
        }

        private static void InserirConta()
        {
            Console.WriteLine("Inserir nova conta");

            Console.WriteLine("Digite 1 para Beneficiario ou 2 para Favorecido: ");
            int entradaTipoCadastro = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite 1 para Conta Pessoa Fisica ou 2 para Juridica: ");
            int entradaTipoConta = int.Parse(Console.ReadLine());

            Console.WriteLine("Digito o Nome do Cliente: ");
            string entradaNome = Console.ReadLine().ToUpper();

            Console.WriteLine("Digite o Saldo Inicial: ");
            double entradaSaldo = double.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Crédito: ");
            double entradaCredito = double.Parse(Console.ReadLine());

            Conta novaConta = new Conta(tipoCadastro: (TipoCadastro)entradaTipoCadastro,
                                        tipo: (TipoConta)entradaTipoConta,
                                        saldo: entradaSaldo,
                                        credito: entradaCredito,
                                        nome: entradaNome
            );

            // Adicionando a lista de objetos tipo Conta.
            listContas.Add(novaConta);
        }

        private static void listarFavorecidos(){
            HeaderMenu("Lista de Favorecidos");

            if(listContas.Count == 0){
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            for (int i = 0; i < listContas.Count; i++){
                Conta conta = listContas[i];
                Console.WriteLine($"# Conta: {i} - {conta.GetNome()}");
            }

            FooterMenu();
        }

        // ToDo: Ajustar Metodo.
        private static void ConsultarSaldo(){
            HeaderMenu("Consulta Saldo");

            if(listContas.Count == 0){
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            Console.WriteLine("Informe o numero da conta: ");
            int numeroConta = int.Parse(Console.ReadLine());

            Conta conta = listContas[numeroConta];
            Console.WriteLine($"# {numeroConta} - {conta.GetNome()} Saldo: {conta.GetSaldo()} - Limite de Credito: {conta.GetCredito()}");

            FooterMenu();
        }

        private static void Transferir()
        {
            HeaderMenu("Transferencia Entre Contas");

            Console.Write("Digite o número da conta de origem: ");
            int indiceContaOrigem = int.Parse(Console.ReadLine());

            Console.Write("Digite o número da conta de destino: ");
            int indiceContaDestino = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser transferido: ");
            double valorTransferencia = double.Parse(Console.ReadLine());

            listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
        }

        private static void Depositar()
        {
            HeaderMenu("Deposito");

            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listContas[indiceConta].Depositar(valorDeposito);
        }

        private static void Sacar()
        {
            HeaderMenu("Saque");

            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());

            listContas[indiceConta].Sacar(valorSaque);
        }

    }

}
