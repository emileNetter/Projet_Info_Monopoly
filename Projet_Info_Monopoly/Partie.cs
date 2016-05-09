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
        public void partie()
        {
            ajoutJoueur();
            Jouer(plateau);
        }
        public void ajoutJoueur()
        {
            string nom;
            int i = 1;
            
            do
            {
                
                    Console.WriteLine("Entrez le nom du joueur n° " + i + ".\n Taper * une fois tous les joueurs rentrés. (de 2 à 8 joueurs)");
                    nom = Console.ReadLine();
                    if (nom != "*")
                    {
                        joueurs.AddLast(new Joueur(nom));
                        i++;
                    }
            //penser au try-catch pour gérer les erreurs

            }
            while (i < 9 || nom != "*"); 

        }

        public void Jouer(Plateau p)
        {
            foreach(Joueur j in joueurs)
            {
                int newPosition= j.Avancer(); 
                Console.WriteLine(p.cases[newPosition]);
            }
        }

    }
}
