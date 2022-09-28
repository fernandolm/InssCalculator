using INSS;
using INSS.service;
using System;
using System.Globalization;

namespace CalculadoraInss
{
    internal class Program
    {
        private static readonly IFormatProvider formatProviderPtBr = CultureInfo.GetCultureInfo("pt-BR");
        private static readonly string ComandoEncerrar = "s";

        static void Main(string[] args)
        {
            GerarTitulo();

            do
            {
                Console.WriteLine("Digite a data do desconto do Inss. Exemplo: 31/12/2011. Formato: dd/mm/aaaa");
                var dataInss = Console.ReadLine();

                var dataInssConvertida = ValidarDataInss(dataInss);

                Console.WriteLine("Digite o valor do salário com duas casas decimais. Utilizar vírgula para casa decimal (,). Exemplo: 1234,56");
                var salario = Console.ReadLine();

                var salarioConvertido = ValidarSalario(salario);

                ICalculadorInss calculadorInss = new CalculadorInss();
                decimal valorDescontoInss = 0;

                try
                {
                    valorDescontoInss = calculadorInss.CalcularDesconto(dataInssConvertida, salarioConvertido);
                    ImprimirResultado(dataInssConvertida, salarioConvertido, valorDescontoInss);
                }
                catch (Exception erro) { 
                    Console.WriteLine("### Não foi possível calcular o desconto do Inss. ERRO: " + erro.Message + " ###");
                }
                
                Console.Write(String.Format("Deseja encerrar o programa? (Digite '{0}' para finalizar ou pressione qualquer outra tecla para continuar) ", ComandoEncerrar));
            } while (Console.ReadLine() != ComandoEncerrar);
        }

        private static void GerarTitulo()
        {
            Console.WriteLine(@"   ____      _            _           _              ___               
/ ___|__ _| | ___ _   _| | __ _  __| | ___  _ __  |_ _|_ __  ___ ___ 
| |   / _` | |/ __| | | | |/ _` |/ _` |/ _ \| '__|  | || '_ \/ __/ __|
| |__| (_| | | (__| |_| | | (_| | (_| | (_) | |     | || | | \__ \__ \
\____\__,_|_|\___|\__,_|_|\__,_|\__,_|\___/|_|    |___|_| |_|___/___/");
            
            Console.WriteLine("\n");
        }

        private static DateTime ValidarDataInss(String dataInss)
        {
            DateTime dataConvertida;

            while (1 == 1)
            {
                if ((dataInss.Length == 10 || dataInss.Length == 8) &&
                    DateTime.TryParse(dataInss,
                                    formatProviderPtBr,
                                    DateTimeStyles.None,
                                    out dataConvertida))
                {
                    return dataConvertida;
                }

                Console.WriteLine("Data inválida. Digite novamente. Exemplo: 28/05/1999. Formato dd/mm/aaaa");
                dataInss = Console.ReadLine();
            }
        }

        private static decimal ValidarSalario(String salario)
        {
            const string ErroSalarioInvalido = "Não foi possível obter um salário válido.";
            decimal salarioConvertido = 0;

            while (salarioConvertido == 0)
            {
                if (decimal.TryParse(salario, NumberStyles.AllowDecimalPoint, formatProviderPtBr, out salarioConvertido) &&
                    salarioConvertido > 0)
                {
                    return salarioConvertido;
                }

                Console.WriteLine("Salário inválido. Digite somente números e um valor maior que zero. Tente novamente. Exemplo: 3548,10");
                salario = Console.ReadLine();
            }

            throw new Exception(ErroSalarioInvalido);
        }

        private static void ImprimirResultado(DateTime dataInss, decimal salario, decimal valorDescontoInss)
        {
            const int TotalLinha = 50;

            Console.WriteLine(new String('-', TotalLinha));
            Console.WriteLine("RESULTADO");
            Console.WriteLine(new String('-', TotalLinha));
            Console.WriteLine(string.Format("Tabela: {0}", dataInss.Year));
            Console.WriteLine(string.Format("Salário: {0}", string.Format(formatProviderPtBr, "{0:c}", salario)));
            Console.WriteLine(string.Format("O valor do desconto é: {0}", string.Format(formatProviderPtBr, "{0:c}", valorDescontoInss)));
            Console.WriteLine(new String('-', TotalLinha));
        }
    }
}
