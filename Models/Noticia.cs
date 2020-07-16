//prevenção de erros por bibliotecas
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_players_2.Interfaces;//necessario para fazer a interface funcionar

namespace E_players_2.Models{
    public class Noticia : EplayersBase, INoticias{
        //propriedades
        public int IdNoticia { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Imagem { get; set; }
        private const string PATH ="Database/noticia.csv";//caminho arquivo


        //construtor
        /// <summary>
        /// Criar os arquivos e o diretorio caso não exista
        /// </summary>
        public Noticia(){
             CreateFolderAndFile(PATH);
        }//end construtor


        //interface
        public void Create(Noticia n){
            string[] linha ={PrepararLinha(n)};
            File.AppendAllLines(PATH, linha);

        }//end void interface create

        /// <summary>
        /// Prepara as linhas do arquivo csv
        /// </summary>
        /// <param name="n">apoio</param>
        /// <returns></returns>
        public string PrepararLinha(Noticia n){
            return $"{n.IdNoticia};{n.Titulo};{n.Texto};{n.Imagem}";
        }//end preparar linha

        /// <summary>
        /// cria uma listra e faz uma especie de tratamento nela
        /// </summary>
        /// <returns>retorna uma lista tratada</returns>
        public List<Noticia> ReadAll(){
            List<Noticia> noticias = new List<Noticia>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas){
                string[] linha = item.Split(";");
                Noticia noticia = new Noticia();
                noticia.IdNoticia = Int32.Parse(linha[0]);
                noticia.Titulo = linha[1];
                noticia.Texto = linha[2];
                noticia.Imagem = linha[3];
                noticias.Add(noticia);
            }//end foreach
            return noticias;
        }//end void interface readaal

        /// <summary>
        /// esse metodo faz alterações no csv rescrevendo ele
        /// </summary>
        /// <param name="e">apoio</param>
        public void Update(Noticia n){
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == n.IdNoticia.ToString());
            linhas.Add( PrepararLinha(n) );
            RewriteCSV(PATH, linhas);
        }//end void interface  update

        /// <summary>
        /// esse metodo deleta um arquivo do csv
        /// </summary>
        /// <param name="id">indentificador</param>
        public void Delete(int idNoticia){
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == idNoticia.ToString());
            RewriteCSV(PATH, linhas);
        }//end void interface delete
    }//end class noticia
}//end namespace