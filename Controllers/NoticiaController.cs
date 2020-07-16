using System;
using System.IO;
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
            
            // Upload Início
            var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias");

            if(file != null){
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }//end is

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create)){  
                    file.CopyTo(stream);  
                }//end using
                novaNoticia.Imagem   = file.FileName;
            }//end if
            else{
                novaNoticia.Imagem   = "padrao.png";
            }//end else
            // Upload Final

            noticiaModel.Create(novaNoticia);
            ViewBag.Noticias = noticiaModel.ReadAll();
            return LocalRedirect("~/Noticia");


        }//end iaction cadastrar

        [Route("[controller]/{id}")]
        public IActionResult Excluir(int id){
            noticiaModel.Delete(id);
            ViewBag.Noticias = noticiaModel.ReadAll();
            return LocalRedirect("~/Noticia");
        }//end iaction excluir

        

        
    }//end clas NoticiaController
}//end namespace