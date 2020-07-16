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
    public class EquipeController : Controller{
        Equipe equipeModel = new Equipe();
        
        /// <summary>
        /// esse metodo é usado para retornar a view e demais alterações
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Index(){
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }//fim IactionResult

        /// <summary>
        /// vai cadasrar os dados recebendo eles de um formulario
        /// </summary>
        /// <param name="form">formulario</param>
        /// <returns>dados cadastrados no form</returns>
        public IActionResult Cadastrar(IFormCollection form){
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(form["IdEquipe"]);
            novaEquipe.Nome = form["Nome"];


            // Upload Início
            var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

            if(file != null){
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }//end is

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create)){  
                    file.CopyTo(stream);  
                }//end using
                novaEquipe.Imagem   = file.FileName;
            }//end if
            else{
                novaEquipe.Imagem   = "padrao.png";
            }//end else
            // Upload Final

            equipeModel.Create(novaEquipe);
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe");
        }//end iaction cadastrar

        [Route("[controller]/{id}")]
        public IActionResult Excluir(int id){
            equipeModel.Delete(id);
            ViewBag.Equipes = equipeModel.ReadAll();
            return LocalRedirect("~/Equipe");
        }//end iaction excluir
        

        
    }//end clas equipeController
}//end namespace
