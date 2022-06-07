using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace exemplosMongoDB
{
    class ProgramDELETE
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Iniciando...");
            await MainAsync(args);
        }

        private static async Task MainAsync(string[] args)
        {
            var conexao = new ConnectionMongoDB();
            Console.WriteLine("BD Conectado");

            //buscando antes de deletar
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("--------Buscando Livro Guerra dos Tronos...---------------------");
            Console.WriteLine("----------------------------------------------------------------");
            var construtor = Builders<Livro>.Filter;
            var condicao = construtor.Eq(x => x.Titulo, "Guerra dos Tronos");

            var livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).Limit(1).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }

            //DELETE 
            //deletando varios
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("------------Deletando Livro Guerra dos Tronos... ---------------");
            Console.WriteLine("----------------------------------------------------------------");
            await conexao.Livros.DeleteManyAsync(condicao);

            //exibindo após alteração
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("---------Buscar Livro Guerra dos Tronos... deletado-------------");
            Console.WriteLine("----------------------------------------------------------------");
            construtor = Builders<Livro>.Filter;
            condicao = construtor.Eq(x => x.Titulo, "Guerra dos Tronos");

            livrosfiltradosPelaClasse = await conexao.Livros.Find(condicao).ToListAsync();
            foreach (var livrofiltradoClasse in livrosfiltradosPelaClasse)
            {
                Console.WriteLine(livrofiltradoClasse.ToJson<Livro>());
            }           
        }
    }
}
