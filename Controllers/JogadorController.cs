using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projeto_gamer_manha.Infra;
using projeto_gamer_manha.Models;

namespace projeto_gamer_manha.Controllers
{
    [Route("[controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();
        private string folder;

        [Route("Listar")]

        public IActionResult Index()
        {
            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipe.ToList();
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            c.Jogador.Add(novoJogador);





            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/JOgadores");

            }

            if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }


            c.SaveChanges();
            return LocalRedirect("Jogador/Listar");

        }
           [Route("Excluir/{id}")]

        public IActionResult Excluir(int id)
{
    Jogador jogador = c.Jogador.First(j => j.IdJogador == id);

    c.Jogador.Remove(jogador);
    c.SaveChanges();

    return LocalRedirect("~/Jogador/Listar");
}

[Route("Editar/{id}")]

public IActionResult Editar(int id)
{
    Jogador jogador = c.Jogador.First(j => j.IdJogador == id);

    ViewBag.Jogador = jogador;
    
    ViewBag.Equipe = c.Equipe.ToList();

    return View("Edit");
}

[Route("Atualizar")]

public IActionResult Atualizar(IFormCollection form)
{
    Jogador novoJogador = new Jogador();

novoJogador.IdJogador = int.Parse(form["IdJogador"])
    novoJogador.Nome = form["Nome"].ToString();
    novoJogador.Email = form["Email"].ToString();
    novoJogador.Senha = form["Senha"].ToString();
    novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

    
}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}