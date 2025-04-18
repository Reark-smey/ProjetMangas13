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
            if (HttpContext.Session.GetString("login") == null) return RedirectToAction("Index", "Home");

            System.Data.DataTable mesMangas = null;
            string title = "Liste de mes Mangas";
            ViewBag.Titre = title;
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
           if (HttpContext.Session.GetString("login") == null) return RedirectToAction("Index", "Home");
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
            if (HttpContext.Session.GetString("login") == null) return RedirectToAction("Index", "Home");
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
            if (HttpContext.Session.GetString("login") == null) return RedirectToAction("Index", "Home");
            System.Data.DataTable mesMangas = null;
            try
            {
                mesMangas = ServiceManga.GetTitres(); ;
                return View(mesMangas);
            }
            catch (MonException e)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Rechercher(String Titre, int id)
        {
            if (HttpContext.Session.GetString("login") == null) return RedirectToAction("Index", "Home");
            System.Data.DataTable mesMangas = null;
            String title = "Liste des mangas recherchés";
            ViewBag.Titre = title;
            try
            {
                if ((id > 0) && (String.IsNullOrEmpty(Titre)))
                {
                    mesMangas = ServiceManga.RechercheTitreParId(id);
                }
                else if (id == 0)
                {
                    mesMangas = ServiceManga.RechercheTitreParString(Titre);
                }


            }
            catch (MonException e)
            {
                return NotFound();
            }
            return View("Index", mesMangas);
        }

      
        public IActionResult Supprimer(int id)
        {
            if (HttpContext.Session.GetString("login") == null) return RedirectToAction("Index", "Home");
            System.Data.DataTable mesMangas = null;
            string title = "Liste de mes Mangas";
            ViewBag.Titre = title;
            try
            {
                ServiceManga.Supprimer(id);
                
            }
            catch (MonException e)
            {
                return NotFound();

            }
            mesMangas = ServiceManga.GetToutLesMangas();
            return View("Index", mesMangas);
        }

        public IActionResult Ajouter()
        {
            if (HttpContext.Session.GetString("login") == null) return RedirectToAction("Index", "Home");

            try
            {
                ServiceManga.GetToutLesMangas();
                return View();
            }
            catch (MonException e)
            {
                return NotFound();

            }
        }

            [HttpPost]
            public IActionResult Ajouter(Manga unM)
            {
                if (HttpContext.Session.GetString("login") == null) return RedirectToAction("Index", "Home");
            System.Data.DataTable mesMangas = null;
            string title = "Liste de mes Mangas";
            ViewBag.Titre = title;
            try
                {
                mesMangas = ServiceManga.GetToutLesMangas();
                ServiceManga.AjouterManga(unM);
                    return View("Index", mesMangas);
                }
                catch (MonException e)
                {
                    return NotFound();

                }
            }
    }
}

