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
                        joueurs.AddLast(new Joueur(nom,plateau));
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
                //int aux = de;
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

            int tmp = nombreJoueursEncoreEnVie();
            while (tmp > 1)
            {
                foreach (Joueur j in joueurs)
                {
                    if (j.statut == Joueur.statutJoueur.enPrison)
                    {
                        ConsoleKeyInfo c;
                        Console.WriteLine("Vous êtes en prison : vous avez 3 choix possibles. Faites 1 pour payer une amende de 50 euros et sortir, 2 pour utiliser une carte sortie de prison et 3 pour tenter de faire un double.");
                        do
                        {
                            c = Console.ReadKey();
                        }
                        while (c.Key != ConsoleKey.NumPad1 && c.Key != ConsoleKey.NumPad2 && c.Key != ConsoleKey.NumPad3);
                        if(c.Key == ConsoleKey.NumPad1)
                        {
                            j.debiter(50);
                            Console.WriteLine("Vous avez payé une amende de 50 euros, vous êtes libéré de prison. Lancez les dés.");
                            j.statut = Joueur.statutJoueur.vivant;
                        }
                    }
                    else if (j.statut == Joueur.statutJoueur.vivant)
                    {

                        int newPosition = j.avancer();
                        j.position = newPosition;
                        Console.WriteLine(p.cases[j.position]);

                        if (p.cases[j.position] is Propriete)
                        {
                            Propriete prop = p.cases[j.position] as Propriete;
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

                        else if (p.cases[j.position] is Impot)
                        {
                            Impot impot = p.cases[j.position] as Impot;
                            j.payeImpot(impot);
                        }
                    }
                    else
                    {
                        joueurs.Remove(j);
                    }

                }
                

            }


        }
            
            
        

        public int nombreJoueursEncoreEnVie()
        {
            int nb = 0;

            foreach(Joueur j in joueurs)
            {
                if(j.statut == Joueur.statutJoueur.vivant || j.statut == Joueur.statutJoueur.enPrison)
                {
                    nb++;
                }
            }
            return nb;
        }

    }
}
