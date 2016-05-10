using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{
    class Partie
    {
        public LinkedList<Joueur> joueurs;
        public Plateau plateau;
        
        public Partie()
        {
            joueurs = new LinkedList<Joueur>();
            plateau = new Plateau();
            
        }
        public void partie() // methode qui execute toutes les fonctions nécessaires pour jouer une partie 
        {
            ajoutJoueur();
            jouer(plateau);
        }
        public void ajoutJoueur()
        {
            string nom;
            int i = 1;
            
            do
            {
                
                    Console.WriteLine("Entrez le nom du joueur n° " + i + ".\n Taper * une fois tous les joueurs rentrés. (de 2 à 8 joueurs)");
                    nom = Console.ReadLine();
                    if (nom != "*" && nom!="")
                    {
                        joueurs.AddLast(new Joueur(nom));
                        i++;
                    }
            //penser au try-catch pour gérer les erreurs

            }
            while ((i < 2 || nom != "*")&& i<9);
            
        }

        public void jouer(Plateau p)
        {
            int maxDe = 1;
            string nomFirstPlayer = "";

            foreach (Joueur j in joueurs)// 
            {
                int de = j.lanceDe();
                if (de > maxDe)
                {
                    maxDe = de;
                    nomFirstPlayer = j.nom_joueur;
                }
            }
            Console.WriteLine(nomFirstPlayer + " commence à jouer");// stocker peut etre le numéro correspondant a ce joueur.
  

            foreach(Joueur j in joueurs)
            {
                int newPosition= j.avancer();
                j.position = newPosition;
                Console.WriteLine(j.position);
                Console.WriteLine(p.cases[j.position]);
                //if (p.cases[j.position] is Propriete)
                //{

                //}
                if (p.cases[j.position] is Terrain)
                {
                    Terrain t =  p.cases[j.position] as Terrain;
                    t.affiche_info_terrain();
                }

                




            }
        }

    }
}
