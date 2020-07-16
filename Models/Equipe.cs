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
using E_players_2.Interfaces;//necessario para implementar a interface na raça

namespace E_players_2.Models{
    public class Equipe : EplayersBase, IEquipe{
        //propriedades
        public int IdEquipe{get; set;}
        public string Nome{get; set;}
        public string Imagem{get; set;}
        private const string PATH ="Database/equipe.csv";//caminho arquivo

        //construtor
        /// <summary>
        /// Criar os arquivos e o diretorio caso não exista
        /// </summary>
        public Equipe(){
             CreateFolderAndFile(PATH);
        }//end construtor

        //interface
        /// <summary>
        /// Vai criar o arquivo e colocar os dados num array de string, 
        /// que ira trazer uma organização feita no metodo preparar
        /// </summary>
        /// <param name="e">apoio para entrar no outro metodo</param>
        public void Create(Equipe e){
            string[] linha ={PrepararLinha(e)};
            File.AppendAllLines(PATH, linha);
        }//end void create

        /// <summary>
        /// Prepara as linhas do arquivo csv
        /// </summary>
        /// <param name="e">apoio</param>
        /// <returns></returns>
        public string PrepararLinha(Equipe e){
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }//end preparar linha

        /// <summary>
        /// cria uma listra e faz uma especie de tratamento nela
        /// </summary>
        /// <returns>retorna uma lista tratada</returns>
        public List<Equipe> ReadAll(){
            List<Equipe> equipes = new List<Equipe>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas){
                string[] linha = item.Split(";");
                Equipe equipe = new Equipe();
                equipe.IdEquipe = Int32.Parse(linha[0]);
                equipe.Nome = linha[1];
                equipe.Imagem = linha[2];
                equipes.Add(equipe);
            }//end foreach
            return equipes;
        }//end void readall

        /// <summary>
        /// esse metodo faz alterações no csv rescrevendo ele
        /// </summary>
        /// <param name="e">apoio</param>
        public void Update(Equipe e){
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());
            linhas.Add( PrepararLinha(e) );
            RewriteCSV(PATH, linhas);
        }//end void update

        /// <summary>
        /// esse metodo deleta um arquivo do csv
        /// </summary>
        /// <param name="id">indentificador</param>
        public void Delete(int id){
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
            RewriteCSV(PATH, linhas);
        }//end void delete






    }//end class equipe
}//end namespace