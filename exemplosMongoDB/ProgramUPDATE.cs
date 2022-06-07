using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace exemplosMongoDB
{
    class ProgramUPDATE
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
            foreach (var livro in livros)
            {
                Console.WriteLine(livro.ToJson<Livro>());
            }

            //UPDATE UNICO
            //Alterando manualmente.
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("---------Alterando ano do Livro Guerra dos Tronos... -----------");
            Console.WriteLine("----------------------------------------------------------------");
            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Titulo, "Guerra dos Tronos");

            var livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).Limit(1).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
                livrofiltradoClasse.Ano = 2000;
                await conexao.Livros.ReplaceOneAsync(condicao, livrofiltradoClasse);

            }

            //exibindo após alteração
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("---------Buscar Livro Guerra dos Tronos... alterado-------------");
            Console.WriteLine("----------------------------------------------------------------");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Eq(x => x.Titulo, "Guerra dos Tronos");

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }


            //Alterando com classe update.
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("--Alterando ano do Livro Guerra dos Tronos... com classe update--");
            Console.WriteLine("-----------------------------------------------------------------");
            var construtorAlteracao = Builders<Livro>.Update;
            var condicaoAlteracao = construtorAlteracao.Set(x => x.Ano, 2001);
            await conexao.Livros.UpdateOneAsync(condicao, condicaoAlteracao);

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }

            //UPDATE ALL
            //Alterando vários 
            //buscando
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("--------------Buscar Livros do ano igual 200--------------------");
            Console.WriteLine("----------------------------------------------------------------");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Eq(x => x.Ano, 200);

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }
            //alterando
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("-----Alterando ano dos Livros com ano igual a 200 para 2000------");
            Console.WriteLine("-----------------------------------------------------------------");
            construtorAlteracao = Builders<Livro>.Update;
            condicaoAlteracao = construtorAlteracao.Set(x => x.Ano, 2000);
            await conexao.Livros.UpdateManyAsync(condicao, condicaoAlteracao);

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }
            //exibindo apos alteracao
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("--------------Buscar Livros do ano igual 2000-------------------");
            Console.WriteLine("----------------------------------------------------------------");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Eq(x => x.Ano, 2000);

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }
        }
    }
}
