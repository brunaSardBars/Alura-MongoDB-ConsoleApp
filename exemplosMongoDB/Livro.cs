using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exemplosMongoDB
{
    public class Livro
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Ano { get; set; }
        public int Paginas { get; set; }
        public List<string> Assunto { get; set; }
    }

    public class ValoresLivro
    {
        public static Livro addLivro(string Titulo,
                                     string Autor,
                                     int Ano,
                                     int Paginas,
                                     string Assuntos)
        {
            Livro livro = new Livro();
            livro.Titulo = Titulo;
            livro.Autor = Autor;
            livro.Ano = Ano;
            livro.Paginas = Paginas;
            string[] vetAssunto = Assuntos.Split(',');
            List<string> listAssuntos = new List<string>();
            foreach (string item in vetAssunto)
            {
                listAssuntos.Add(item.Trim());
            }
            livro.Assunto = listAssuntos;
            return livro;
        }
    }
}
