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
            Console.WriteLine("Vous allez jouer une nouvelle partie de monopoly, appuyez sur entrée pour commencer à jouer.");
            Console.ReadLine();
            Console.Clear();
            ajoutJoueur();
            jouer(plateau);
        }

        public void ajoutJoueur() // ajoute les joueurs dans une liste à la partie (de 2 à 8)
        {
            string nom;
            int i = 1;
            
            do
            {
                
                    Console.WriteLine("Entrez le nom du joueur n° " + i + ".\n Taper * une fois tous les joueurs rentrés. (de 2 à 8 joueurs)");
                    nom = Console.ReadLine();
                    if (nom != "*" && nom!="")
                    {
                        joueurs.AddLast(new Joueur(nom,plateau,this));
                        i++;
                    }
            

            }
            while ((i < 2 || nom != "*")&& i<9);
            Console.Clear();
            Console.WriteLine("La partie commence ! \n");
        }

        public void jouer(Plateau p) // gère les différents etats des joueurs et effectue les actions en conséquence
        {
            int maxDe = 0;
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

            
            while (nombreJoueursEncoreEnVie()) // si le nombre de joueurs en vie est 1 la partie se termine
            {
                foreach (Joueur j in joueurs)
                {
                    #region Execution Joueur en Prison 
                    if (j.statut == Joueur.statutJoueur.enPrison) // si le joueur est en prison
                    {
                        j.position = 10;
                        ConsoleKeyInfo c;
                        Console.WriteLine("\nC'est au tour de " + j.nom_joueur +" de jouer");
                        Console.WriteLine("\n Vous êtes en prison : vous avez 3 choix possibles. Faites 1 pour payer une amende de 50 euros et sortir, 2 pour utiliser une carte sortie de prison et 3 pour tenter de faire un double.");
                        do
                        {
                            c = Console.ReadKey();
                        }
                        while (c.KeyChar != '1' && c.KeyChar != '2' && c.KeyChar != '3');
                        if(c.KeyChar == '1')
                        {
                            j.debiter(50);
                            Console.WriteLine("\nVous avez payé une amende de 50 euros, vous êtes libéré de prison. Lancez les dés.");
                            j.statut = Joueur.statutJoueur.vivant;
                            j.avancer();
                        }
                        else if(c.KeyChar == '2') 
                        {
                            
                            
                                if (j.cartesDuJoueur.Count > 0)// on ne peut que posséder 1 seul carte(celle de la prison)
                                {
                                    Libere_Prison c1 = j.cartesDuJoueur[0] as Libere_Prison;
                                    c1.EffetCarte(j);
                                }
                                else
                                {
                                    Console.WriteLine("\nVous ne possédez pas la carte");
                                    break;
                                }
                            
                 
                        }
                        else if (c.KeyChar == '3')
                        {
                            Console.WriteLine("\nFaites un double pour sortir de prison");
                            Random r = new Random();
                            int de1 = r.Next(1, 7);
                            int de2 = r.Next(1, 7);
                            Console.WriteLine("Dé 1 : " +de1 + "\nDé2 : " +de2);
                            if(de1 == de2 || j.nbTourEnPrison == 3)
                            {
                                Console.WriteLine("C'est un double!");
                                Console.WriteLine("Vous êtes libéré de prison");
                                j.statut = Joueur.statutJoueur.vivant;
                                j.dernierLanceDe = de1 + de2;
                                j.position += j.dernierLanceDe;
                            }
                            else
                            {
                                Console.WriteLine("Dommage, réessayez au prochain tour");
                                j.nbTourEnPrison++;
                            }
                           
                        }
                    }
                    #endregion 

                    #region Execution Joueur vivant
                    else if (j.statut == Joueur.statutJoueur.vivant) // si le joueur est vivant
                    {
                        
                        j.compteurDouble = 0;
                        while (j.compteurDouble >= 0 && j.compteurDouble <= 3)
                        {
                            
                            Console.WriteLine("\nC'est au tour de " +j.nom_joueur + " de jouer. Que souhaitez vous faire ?");
                            Console.WriteLine(" 1 pour lancer les dés, 2 pour consulter vos informations, 3 pour construire un batiment");
                            ConsoleKeyInfo c;
                            do
                            {
                                c = Console.ReadKey();
                            }
                            while (c.KeyChar != '1' && c.KeyChar != '2' && c.KeyChar!='3');
                            if (c.KeyChar == '1')
                            {
                                Console.Clear();
                                int newPosition = j.avancer();
                                if (j.compteurDouble == 3)
                                {
                                    j.compteurDouble = 0;
                                    break;
                                }
                                j.position = newPosition;
                                Console.WriteLine(p.cases[j.position]);


                                if (j.compteurDouble != 3)
                                {
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
                                    else if (p.cases[j.position] is CasesCommunautes)
                                    {
                                        j.tirerUneCarte(p.cartesCommunaute);
                                        Console.ReadLine();
                                        Console.Clear();

                                    }
                                    else if (p.cases[j.position] is CasesChances)
                                    {
                                        j.tirerUneCarte(p.cartesChance);
                                    }
                                    else if (p.cases[j.position] is Prison | p.cases[j.position] is ParcGratuit)
                                    {
                                        Console.WriteLine("Reposez vous ");
                                        Console.ReadLine();
                                        Console.Clear();
                                    }
                                    else if (p.cases[j.position] is Police)
                                    {
                                        Police police = p.cases[j.position] as Police;
                                        police.arrestationPolice(j);
                                        Console.ReadLine();
                                        Console.Clear();
                                    }
                                }
                            }
                            else if (c.KeyChar == '2')
                            {
                                Console.Clear();
                                j.infoJoueur();
                            }
                            
                           else if (c.KeyChar == '3')
                            {
                                Console.Clear();
                               int i=1;
                               List<Terrain> constructionPossibleMaisons= new List<Terrain>();
                               List<Terrain> constructionPossibleHotels= new List<Terrain>();
                                foreach (Terrain t in j.proprieteDuJoueur)
                                {
                                    if (t.peutConstruireMaison(j))
                                    {
                                        constructionPossibleMaisons.Add(t);
                                        i++;
                                    }
                                    else if (t.peutConstruireHotel(j))
                                    {
                                        constructionPossibleHotels.Add(t);
                                        i++;
                                    }
                                    
                                }
                                PropositionConstructionBatiment(j, constructionPossibleMaisons, constructionPossibleHotels);

                               
                            }
                    #endregion
                        }
                            
                    }
                       
                    else // si le joueur est mort 
                    {
                        joueurs.Remove(j);
                    }

                }
                

            }  
            foreach (Joueur j in joueurs)
            {
                Console.WriteLine(j.nom_joueur + " a gagné !! ");
            }  
            


        }
            

        public bool nombreJoueursEncoreEnVie() // true si le nombre de joueurs est supérieur à 1
        {
            int nb = 0;

            foreach(Joueur j in joueurs)
            {
                if(j.statut != Joueur.statutJoueur.perdu)
                {
                    nb++;
                }
            }
            if (nb > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PropositionConstructionBatiment(Joueur j, List<Terrain> Maisons, List<Terrain> Hotels)
        {
            int taille = Maisons.Count;
            int taille1=Hotels.Count;
            int i=1;
            if (taille > 0)
            {
                Console.WriteLine("Maisons :");
                foreach (Terrain t in Maisons)
                {
                    Console.WriteLine(i + " :\n  " + t.nom_case + " (" + t.Couleur + ")");
                    i++;
                }
                Console.WriteLine("\nVoulez vous construire une maison sur un terrain  ? Si oui, taper le numéro correspondant, sinon taper 0");

                int c;
                bool erreur = true;
                do
                {
                    try
                    {
                        c = int.Parse(Console.ReadLine());

                        if (c != 0)
                        {
                            if (c < taille + 1)
                            {
                                Terrain t1 = Maisons[c - 1];
                                j.construireMaison(t1);
                                erreur = false;
                            }
                            else
                            {
                                Console.WriteLine("L'indice désiré est trop élevé.");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                while (erreur == true);
            }
            else
            {
                Console.WriteLine("Vous n'avez aucune propriété permettant la construction de maisons.");
            }
        
        if (taille1 > 0)
            {
                Console.WriteLine("Hotels :");
                foreach (Terrain t in Hotels)
                {
                    Console.WriteLine(i + " :/n  " + t.nom_case + " (" + t.Couleur + ")");
                }
                Console.WriteLine("\nVoulez vous construire un hotel sur un terrain  ? Si oui, taper le numéro correspondant, sinon taper 0");

                int c;
                bool erreur = true;
                do
                {
                    try
                    {
                        c = int.Parse(Console.ReadLine());

                        if (c != 0)
                        {
                            if (c < taille + 1)
                            {
                                Terrain t2 = Hotels[c - 1];
                                j.construireHotel(t2);
                                erreur = false;
                            }
                            else
                            {
                                Console.WriteLine("L'indice désiré est trop élevé.");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                while (erreur == true);
            }
            else
            {
                Console.WriteLine("Vous n'avez aucune propriété permettant la construction d'hotels.");
                Console.ReadLine();
                Console.Clear();
            }
        }

    }
}
