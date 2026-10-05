using System;
using System.Globalization;

namespace Desafio3
{
    class Programa
    {
        static void Main(string[] args)
        {
            Console.Write("Informe o valor original (R$): ");
            double valor = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.Write("Informe a data de vencimento (dd/MM/yyyy): ");
            string dataStr = Console.ReadLine();
            DateTime dataVencimento = DateTime.ParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime hoje = DateTime.Now.Date;

            double juros = 0;
            if (hoje > dataVencimento)
            {
                int diasAtraso = (hoje - dataVencimento).Days;
                juros = valor * 0.025 * diasAtraso;
            }
            else
            {
                juros = 0;
            }

            double total = valor + juros;

            Console.WriteLine("Valor original: R$ " + valor.ToString("F2"));
            Console.WriteLine("Dias de atraso: " + (hoje > dataVencimento ? (hoje - dataVencimento).Days.ToString() : "0"));
            Console.WriteLine("Juros (2,5% ao dia): R$ " + juros.ToString("F2"));
            Console.WriteLine("Valor total a pagar: R$ " + total.ToString("F2"));
        }
    }
}
