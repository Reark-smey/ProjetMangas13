
using System.Data;
using ProjetMangas.Models.Persistance;
using ProjetMangas.Models.MesExceptions;
using ProjetMangas.Models.Metier;
using System.Security.Cryptography.X509Certificates;
using MySqlX.XDevAPI;

namespace ProjetMangas.Models.dao
{
    public class ServiceManga
    {
        public static DataTable GetToutLesMangas()
        {
            DataTable mesMangas;
            Serreurs er = new Serreurs("Erreur sur lecture de mes Mangas", "Mangas.getMangas()");
            try
            {
                String mysql = " Select id_manga, Titre, lib_genre, nom_dessinateur, nom_scenariste, prix, couverture";
                mysql += " from manga";
                mysql += " join genre on manga.id_genre = genre.id_genre";
                mysql += " join dessinateur on manga.id_dessinateur = dessinateur.id_dessinateur";
                mysql += " join scenariste on manga.id_scenariste = scenariste.id_scenariste";

                mesMangas = DBInterface.Lecture(mysql, er);

                return mesMangas;


            } catch(MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static Manga GetunManga(String id)
        {
            DataTable dt;
            Manga unManga = null;
            Serreurs er = new Serreurs("Erreur sur la lecture des Mangas", "Service.GetunManga()");
            try
            {
                String mysql = " Select id_manga, id_genre , id_dessinateur, id_scenariste, titre, prix, couverture";
                mysql += " from manga";
                mysql += " where id_manga = " + id;
                dt = DBInterface.Lecture(mysql, er);
                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    unManga = new Manga();
                    DataRow dataRow = dt.Rows[0];
                    unManga.Id_manga = int.Parse(dataRow[0].ToString());
                    unManga.Id_genre = int.Parse(dataRow[1].ToString());
                    unManga.Id_dessinateur = int.Parse(dataRow[2].ToString());
                    unManga.Id_scenariste = int.Parse(dataRow[3].ToString());
                    unManga.Titre = dataRow[4].ToString();
                    unManga.Prix = Double.Parse(dataRow[5].ToString());
                    unManga.Couverture = dataRow[6].ToString();

                    return unManga;

                }
                else
                    return null;

            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

           
        

     public static void UpdateManga(Manga unM)
        {
            Serreurs er = new Serreurs(" Erreur sur l'écriture d'un manga.", "ServiceManga.update()");
            String requete = " UPDATE Manga SET " +
            "id_scenariste = " + unM.Id_scenariste +
            ",id_dessinateur = " + unM.Id_dessinateur + 
            " ,id_genre = " + unM.Id_genre +  
            ", titre = '" + unM.Titre + "'" +
            ",Prix = " + unM.Prix.ToString().Replace(',', '.') +
            ",couverture = '" + unM.Couverture + "'" +
            " WHERE id_manga = " + unM.Id_manga;
            try
            {
                DBInterface.Execute_Transaction(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }
        public static DataTable GetTitres()
        {
            DataTable mesMangas;
            Serreurs er = new Serreurs("Erreur sur lecture de mes Mangas", "Mangas.getMangas()");
            try
            {
                String mysql = " Select id_manga, Titre";
                mysql += " from manga";
                mysql += " ORDER BY Titre ";
                
                mesMangas = DBInterface.Lecture(mysql, er);

                return mesMangas;

            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static DataTable RechercheTitreParString(String id)
        {
            DataTable mesMangas;
            Serreurs er = new Serreurs(" Erreur sur la recherche d'un manga.", "ServiceManga.RechercheTitreParString()");
            try
            {
                String mysql = " Select id_manga, Titre, lib_genre, nom_dessinateur, nom_scenariste, prix, couverture";
                mysql += " from manga";
                mysql += " join genre on manga.id_genre = genre.id_genre";
                mysql += " join dessinateur on manga.id_dessinateur = dessinateur.id_dessinateur";
                mysql += " join scenariste on manga.id_scenariste = scenariste.id_scenariste";
                mysql += " where Titre LIKE '" + id + "%'";

                mesMangas = DBInterface.Lecture(mysql, er);

                return mesMangas;

            } catch(MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static DataTable RechercheTitreParId(int id)
        {
            DataTable mesMangas;
            Serreurs er = new Serreurs(" Erreur sur la recherche d'un manga.", "ServiceManga.RechercheTitreParString()");
            try
            {
                String mysql = " Select id_manga, Titre, lib_genre, nom_dessinateur, nom_scenariste, prix, couverture";
                mysql += " from manga";
                mysql += " join genre on manga.id_genre = genre.id_genre";
                mysql += " join dessinateur on manga.id_dessinateur = dessinateur.id_dessinateur";
                mysql += " join scenariste on manga.id_scenariste = scenariste.id_scenariste";
                mysql += " where id_manga = " + id;

                mesMangas = DBInterface.Lecture(mysql, er);

                return mesMangas;

            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }
    }
}



