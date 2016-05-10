using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{
    public class Partie
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
            Joueur jfirst = null;

            foreach (Joueur j in joueurs)// 
            {
                int de = j.lanceDe();
                int aux = de;
                if (de > maxDe)
                {
                    maxDe = de;
                    nomFirstPlayer = j.nom_joueur;
                    jfirst = j;
                }
               

            }
            
            joueurs.Remove(jfirst);
            joueurs.AddFirst(jfirst);


                    
                
        
            Console.WriteLine(nomFirstPlayer + " commence à jouer");// stocker peut etre le numéro correspondant a ce joueur.
            Console.ReadLine();
            Console.Clear();
  

            foreach(Joueur j in joueurs)
            {
                int newPosition= j.avancer();
                j.position = newPosition;
                Console.WriteLine(p.cases[j.position]);
                
                if (p.cases[j.position] is Propriete )
                {
                    Propriete prop =  p.cases[j.position] as Propriete;
                    if (prop.estPossedee == false)
                    {
                        prop.affiche_info_case(p.cases[j.position]);
                        j.acheterPropriete(prop);
                    }
                    else
                    {
                        j.paye_loyer(prop, this);
                    }
                    
                }

                




            }
        }

    }
}
