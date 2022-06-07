using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace exemplosMongoDB
{
    class ProgramGET
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

            //GET ALL
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("----------------Buscando todos os Livros------------------------");
            Console.WriteLine("----------------------------------------------------------------");            
            var livros = await conexao.Livros.Find(new BsonDocument()).ToListAsync();
            foreach(var livro in livros)
            {
                Console.WriteLine(livro.ToJson<Livro>());
            }

            //GET por filtros (Manualmente) 
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("------------Buscando Livros com 50 paginas...-------------------");
            Console.WriteLine("----------------------------------------------------------------");            
            var filtro = new BsonDocument()
            {
                { "Paginas", 50 }
            };            

            var livrosfiltrados = await conexao.Livros.Find(filtro).ToListAsync();
            foreach (var livrofiltrado in livrosfiltrados)
            {
                Console.WriteLine(livrofiltrado.ToJson<Livro>());
            }

            //Get por filtros utilizando a Classe do Mongo
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("---------Buscando Livros do autor Jose... usando a classe-------");
            Console.WriteLine("----------------------------------------------------------------");
            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Autor, "Jose");

            var livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }

            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("--Buscando Livros do autor com mais de 50 paginas e ano a partir de 2000--");
            Console.WriteLine("--------------------------------------------------------------------------");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Gte(x => x.Paginas, 50) & construtor.Gt(x => x.Ano, 2000);

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("--------------------Buscando Livros de Terror-----------------------------");
            Console.WriteLine("--------------------------------------------------------------------------");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.AnyEq(x => x.Assunto, "Terror");

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }

            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("-------Buscando Livros >= 50 paginas, ordernado por autor-------");
            Console.WriteLine("----------------------------------------------------------------");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Gte(x => x.Paginas, 50);

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).SortBy(x => x.Autor).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }

            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("--Buscando 2 primeiros Livros >= 50 paginas, ordernado por autor--");
            Console.WriteLine("------------------------------------------------------------------");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Gte(x => x.Paginas, 50);

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).SortBy(x => x.Autor).Limit(2).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }

        }
    }
}
