using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Desafio1
{
    class Venda
    {
        public string vendedor { get; set; }
        public double valor { get; set; }
    }

    class RootObject
    {
        public List<Venda> vendas { get; set; }
    }

    class Programa
    {
        static void Main(string[] args)
        {
            string caminho = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vendas.json");
            string json = File.ReadAllText(caminho);
            RootObject dados = JsonSerializer.Deserialize<RootObject>(json);

            Dictionary<string, double> comissaoPorVendedor = new Dictionary<string, double>();

            foreach (Venda v in dados.vendas)
            {
                double comissao = 0;
                if (v.valor < 100)
                {
                    comissao = 0;
                }
                else
                {
                    if (v.valor < 500)
                    {
                        comissao = v.valor * 0.01;
                    }
                    else
                    {
                        comissao = v.valor * 0.05;
                    }
                }

                if (comissaoPorVendedor.ContainsKey(v.vendedor))
                {
                    double valorAtual = comissaoPorVendedor[v.vendedor];
                    comissaoPorVendedor[v.vendedor] = valorAtual + comissao;
                }
                else
                {
                    comissaoPorVendedor.Add(v.vendedor, comissao);
                }
            }

            foreach (KeyValuePair<string, double> kvp in comissaoPorVendedor)
            {
                Console.WriteLine("Vendedor: " + kvp.Key + " - Comissão total: R$ " + kvp.Value.ToString("F2"));
            }
        }
    }
}
