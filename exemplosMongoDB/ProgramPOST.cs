using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace exemplosMongoDB
{
    class ProgramPOST
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Iniciando...");
            await MainAsync(args);
            Console.WriteLine("Executando...");
        }

        private static async Task MainAsync(string[] args)
        {
            var conexao = new ConnectionMongoDB();
            Console.WriteLine("BD Conectado");

            Livro livro = new Livro();
            livro = ValoresLivro.addLivro("Guerra dos Tronos", "R R Martin", 1999, 365, "Guerra, Aventura");
            Console.WriteLine("Inserindo unico");
            await conexao.Livros.InsertOneAsync(livro);

            List<Livro> livros = new List<Livro>();
            livros.Add(ValoresLivro.addLivro("Livro 1", "Jose", 200, 50, "Guerra, Aventura"));
            livros.Add(ValoresLivro.addLivro("Livro 2", "Maria", 200, 50, "Romance, Aventura"));
            livros.Add(ValoresLivro.addLivro("Livro 3", "Josele", 200, 50, "Aventura"));
            livros.Add(ValoresLivro.addLivro("Livro 4", "Joseida", 200, 50, "Terror"));

            Console.WriteLine("Inserindo " + livros.Count + " livros.");
            await conexao.Livros.InsertManyAsync(livros);
          

        }
    }
}
