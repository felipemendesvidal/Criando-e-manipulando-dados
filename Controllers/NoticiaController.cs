using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_players_2.Models;
using Microsoft.AspNetCore.Http;


namespace E_players_2.Controllers{
    public class NoticiaController : Controller{
        Noticia noticiaModel = new Noticia();
        
        /// <summary>
        /// esse metodo é usado para retornar a view e demais alterações
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Index(){
            ViewBag.Noticias = noticiaModel.ReadAll();
            return View();
        }//fim IactionResult

        /// <summary>
        /// vai cadasrar os dados recebendo eles de um formulario
        /// </summary>
        /// <param name="form">formulario</param>
        /// <returns>dados cadastrados no form</returns>
        public IActionResult Cadastrar(IFormCollection form){
            Noticia novaNoticia = new Noticia();
            novaNoticia.IdNoticia = Int32.Parse(form["IdNoticia"]);
            novaNoticia.Titulo = form["Titulo"];
            novaNoticia.Texto = form["Texto"];
            novaNoticia.Imagem = form["Imagem"];

            noticiaModel.Create(novaNoticia);
            ViewBag.Noticias = noticiaModel.ReadAll();
            return LocalRedirect("~/Noticia");


        }//end iaction cadastrar

        

        
    }//end clas NoticiaController
}//end namespace