using Microsoft.AspNetCore.Mvc;
using ProjetMangas.Models.dao;
using ProjetMangas.Models.MesExceptions;
using ProjetMangas.Models.Metier;

namespace ProjetMangas.Controllers
{
    public class MangasController : Controller
    {
        public IActionResult Index()
        {
            System.Data.DataTable mesMangas = null;
            try
            {

                mesMangas = ServiceManga.GetToutLesMangas();


            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des mangas : " + e.Message);
            }
            return View(mesMangas);
        }



        public IActionResult Modifier(string id)
        {
            Manga unManga = null;
            try
            {
                unManga = ServiceManga.GetunManga(id);
                return View(unManga);
            }
            catch (MonException e)
            {
                return NotFound();

            }
        }

        [HttpPost]
        public IActionResult Modifier(Manga unM)
        {
            try
            {
                ServiceManga.UpdateManga(unM);
                return View(unM);
            }
            catch (MonException e)
            {
                return NotFound();

            }
        }

        public IActionResult Rechercher()
        {
            System.Data.DataTable mesMangas = null;
            try
            {
                mesMangas = ServiceManga.GetTitres(); ;
                return View();
            }
            catch (MonException e)
            {
                return NotFound();
            }
        }
    }
}

