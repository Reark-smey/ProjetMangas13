using Microsoft.AspNetCore.Mvc;
using ProjetMangas.Models.dao;
using ProjetMangas.Models.MesExceptions;
using ProjetMangas.Models.Metier;
using ProjetMangas.Models.Utilitaires;

namespace ProjetMangas.Controllers
{
    public class ConnexionController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Controle()
        {
            try
            {
                HttpContext.Session.SetInt32("id", 0);
                // on récupère les données du formulaire
                string login = Request.Form["login"];
                string mdp = Request.Form["pwd"];
                try
                {
                    ServiceUtilisateur unService = new ServiceUtilisateur();
                    Utilisateur unUtilisateur = unService.getUtilisateur(login);
                    if (unUtilisateur != null)
                    {
                        try
                        {
                            Byte[] selmdp = MonMotPassHash.GenerateSalt();
                            Byte[] mdpByte = MonMotPassHash.PasswordHashe("secret", selmdp);
                            String mdpS = MonMotPassHash.BytesToString(mdpByte);
                            String saltS = MonMotPassHash.BytesToString(selmdp);
                            String sel = unUtilisateur.Salt;
                            // on récupère le sel
                            Byte[] salt = MonMotPassHash.transformeEnBytes(unUtilisateur.Salt);
                            // on génère le mot de passe
                            Byte[] tempo = MonMotPassHash.transformeEnBytes(unUtilisateur.MotPasse);
                            if (MonMotPassHash.VerifyPassword(salt, mdp, tempo) == false)
                            {
                                ModelState.AddModelError("Erreur", "Erreur lors du contrôle du mot de passe pour : " + login);
                                return RedirectToAction("Index", "Connexion");
                            }
                            HttpContext.Session.SetString("login", unUtilisateur.NomUtil);
                            HttpContext.Session.SetString("pwd", unUtilisateur.MotPasse);
                            
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("Erreur", "Erreur lors du contrôle : " + e.Message);
                            return RedirectToAction("Index", "Connexion");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Erreur", "Erreur login erroné : " + login);
                        return RedirectToAction("Index", "Connexion");
                    }
                }
                catch (MonException e)
                {
                    ModelState.AddModelError("Erreur", "Erreur lors de l'authentification : " + e.Message);
                    return RedirectToAction("Index", "Connexion");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de l'authentification : " + e.Message);
                return RedirectToAction("Index", "Connexion");
            }
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("login") != null)
            {
                HttpContext.Session.Remove("login");
                HttpContext.Session.Remove("pwd");

                return RedirectToAction("Index", "Connexion");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
