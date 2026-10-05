using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Desafio2
{
    class Produto
    {
        public int codigoProduto { get; set; }
        public string descricaoProduto { get; set; }
        public int estoque { get; set; }
    }

    class EstoqueRoot
    {
        public List<Produto> estoque { get; set; }
    }

    class Movimentacao
    {
        public int id { get; set; }
        public int codigoProduto { get; set; }
        public string descricao { get; set; }
        public int quantidade { get; set; }
    }

    class Programa
    {
        static void Main(string[] args)
        {
            string caminho = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "estoque.json");
            string json = File.ReadAllText(caminho);
            EstoqueRoot dados = JsonSerializer.Deserialize<EstoqueRoot>(json);

            List<Movimentacao> historico = new List<Movimentacao>();
            int proximoId = 1;

            while (true)
            {
                Console.WriteLine("\n=== MENU DE MOVIMENTACAO ===");
                Console.WriteLine("1 - Registrar movimentacao");
                Console.WriteLine("2 - Sair");
                Console.Write("Opcao: ");
                string escolha = Console.ReadLine();

                if (escolha == "1")
                {
                    Console.Write("Codigo do produto: ");
                    int codigo = int.Parse(Console.ReadLine());

                    Produto p = null;
                    foreach (Produto prod in dados.estoque)
                    {
                        if (prod.codigoProduto == codigo)
                        {
                            p = prod;
                            break;
                        }
                    }

                    if (p == null)
                    {
                        Console.WriteLine("Produto nao encontrado.");
                        continue;
                    }

                    Console.Write("Descricao da movimentacao (Entrada/Saida): ");
                    string desc = Console.ReadLine();

                    Console.Write("Quantidade (positiva = entrada, negativa = saida): ");
                    int qtd = int.Parse(Console.ReadLine());

                    Movimentacao mov = new Movimentacao()
                    {
                        id = proximoId,
                        codigoProduto = codigo,
                        descricao = desc,
                        quantidade = qtd
                    };
                    proximoId++;

                    p.estoque = p.estoque + qtd;
                    if (p.estoque < 0) p.estoque = 0;

                    historico.Add(mov);

                    Console.WriteLine($"Movimentacao registrada (ID={mov.id}). Estoque atual de {p.descricaoProduto}: {p.estoque}");
                }
                else if (escolha == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opcao invalida.");
                }
            }
        }
    }
}
