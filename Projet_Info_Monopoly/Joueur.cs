﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Projet_Info_Monopoly
{
    public class Joueur
    {
        public string nom_joueur { get; set; }
        public double argent { get; set; } // argent du joueur (initialisé à 1500)
        public int position { get; set; } // la position du joueur sur le plateau
        public enum statutJoueur { vivant, enPrison, perdu};
        public statutJoueur statut;
        private List<Cartes> cartesDuJoueur; 
        public List<Propriete> proprieteDuJoueur;
        private static Random r = new Random();
        public Plateau plateau;
        public int dernierLanceDe;
        public int nbTourEnPrison;
        public int nbMaisonPossedes;
        public int nbHotelPossedes;
        public int compteurDouble ;
        public Partie partie;



        public Joueur(string nom, Plateau p, Partie p1)
        {
            nom_joueur = nom;
            plateau = p;
            argent = 1500;
            position = 0;
            cartesDuJoueur = new List<Cartes>(); // on initialise une liste de cartes dans laquelle on va ajouter les cartes qu'il possède
            proprieteDuJoueur = new List<Propriete>();
            statut = statutJoueur.vivant;
            nbTourEnPrison = 0;
            partie = p1;
            compteurDouble = 0;


        }

        public void acheterPropriete(Propriete p)
        {
            if ((this.argent > p.prixAchat) && p.estPossedee == false)
            {
                ConsoleKeyInfo c;
                Console.WriteLine("Souhaitez vous acheter {0} pour {1} euros ? (o) (n)", p.nom_case, p.prixAchat);
                do
                {
                    c = Console.ReadKey();
                }
                while (c.KeyChar != 'o' && c.KeyChar != 'n');
                if (c.KeyChar == 'o')
                {
                    Console.Clear();
                    Console.WriteLine(this.nom_joueur + " a acheté {0}", p.nom_case);
                    p.proprietaire = this;
                    p.estPossedee = true;
                    this.proprieteDuJoueur.Add(p);
                    this.argent -= p.prixAchat;
                    Console.WriteLine("Il vous reste {0} euros.", this.argent);
                    Console.ReadLine();
                    Console.Clear();
                    //this.addCard(carte qui correspond à la propriete)
                }

            }
            else if (this.argent < p.prixAchat)
            {
                Console.WriteLine("Vous n'avez pas assez d'argent pour acheter cette propriété");
                Console.ReadLine();
                Console.Clear();
            }

        }

        public void paye_loyer(Propriete p, Partie partie)
        {
            
            if (p.estPossedee == true && p.proprietaire != this) // on vérifie que la propriété est possédée et que l'on ne se paye pas soi-même
            {
                foreach (Joueur j in partie.joueurs)
                {
                    if (p.proprietaire == j)
                    {

                        double loy =p.calculeLoyer(j, this);
                        if( loy > this.argent)
                        {
                            this.defaiteJoueur();
                        }
                        else
                        {
                            Console.WriteLine("\nVous devez payer " + loy + " à " + j.nom_joueur);
                            j.argent += loy;
                            this.argent -= loy;
                            Console.WriteLine("\n" +j.nom_joueur + " a désormais " + j.argent);
                            Console.WriteLine("Vous avez désormais " + this.argent);
                            Console.ReadLine();
                            Console.Clear();
                        }
                        
                    }
                }
            }
        }

        public void payeImpot(Impot impot) // cases taxe de luxe et impôt sur le revenu
        {
            if (impot.prixAPayer > this.argent)
            {
                this.defaiteJoueur();
            }
            else
            {
                Console.WriteLine("\nVous devez payez une taxe de " + impot.prixAPayer + " euros");
                this.argent -= impot.prixAPayer;
                Console.WriteLine("Il vous reste : " + this.argent + " euros");
                
            }
            Console.ReadLine();
            Console.Clear();
        }


        public int avancer() // déplace le joueur
        {
            position += lanceDe();
            if (position >= 40)
            {
                position = position % 40;
                argent += 200; // pouvoir définir une valeur modifiable depuis le XML
            }
            return position;


        }

        public int lanceDe() // jet des dés et vérification des doubles
        {
            
            int de1 = r.Next(1,7);
            int de2 = r.Next(1,7);
            int total = de1 + de2;

            if (de1 == de2)
            {
                Console.WriteLine(nom_joueur + " a fait  un double  " + de1 + " ! " + "(" + 2 * de1 + ")");
                this.compteurDouble = 1;
                if (this.compteurDouble == 3)
                {
                    Console.WriteLine("3ème double ! ALLEZ EN PRISON NE PASSEZ PAS PAR LA CASE DÉPART.");
                    this.statut = Joueur.statutJoueur.enPrison;
                }
            }
            else
            {
                Console.WriteLine(nom_joueur + " a fait : " + total + " (" + de1 + "+" + de2 + ")");
                this.compteurDouble = -1;
            }
            dernierLanceDe = total;// on récupere le résultat de lancer de dé en cas de tomber sur une case de type compagnie necessitant de connaitre le lancer de dé afin d'établir le prix du loyer
            return total;
        }

        public void addCard(Cartes c) // ajoute la carte à la main du joueur (libéré de prison)
        {
            cartesDuJoueur.Add(c);

        }
  
        public void debiter(int somme) // retire une somme à un joueur
        {
            this.argent -= somme;
        }

        public int calculeNombreTerrainCouleur(Terrain t) //compte le nombre de terrain d'une couleur
        {

            int nbr = 0;
            foreach (Terrain p in proprieteDuJoueur)
            {
                if (p.Couleur == t.Couleur)
                {
                    nbr++;
                }
            }
            return nbr;

        }

        public int calculeNombreGares() // nombre de gares
        {
            int nbr = 0;
            {
                foreach (Propriete p in proprieteDuJoueur)
                {
                    if (p is Gare)
                    {
                        nbr++;
                    }
                }
            }
            return nbr;
        }

        public int calculeNombreCompagnies() // nombre de compagnies
        {
            int nbr = 0;
            {
                foreach (Propriete p in proprieteDuJoueur)
                {
                    if (p is Compagnie)
                    {
                        nbr++;
                    }
                }
            }
            return nbr;
        }

        public void construireMaison(Terrain t) // permet de construire une maison sur un terrain passé en paramètre
        {
            if (t.peutConstruireMaison(this))
            {
                 
                Console.Clear();
                Console.WriteLine("Voulez-vous construire une maison sur {0} pour {1} euros ? (o/n)", t.nom_case, t.prixMaison);
                ConsoleKeyInfo c;
                do
                {
                    c = Console.ReadKey();
                }
                while (c.KeyChar != 'o' && c.KeyChar != 'n');
                if (c.KeyChar == 'o')
                {
                    this.argent -= t.prixMaison;
                    t.nbMaisonConstruites++;
                    this.nbMaisonPossedes++;
                    Console.WriteLine("Vous avez construit une maison sur {0}", t.nom_case);
                }
            }
            
        }

        public void construireHotel(Terrain t) // permet de construire un hotel sur un terrain si l'on a 4 maisons 
        {
            if (t.peutConstruireHotel(this))
            {
                Console.Clear();
                Console.WriteLine("Voulez-vous construire un hôtel sur {0} pour {1} euros ? (o/n)", t.nom_case, t.prixHotel);
                ConsoleKeyInfo c;
                do
                {
                    c = Console.ReadKey();
                }
                while (c.KeyChar != 'o' && c.KeyChar != 'n');
                if (c.KeyChar == 'o')
                {
                    this.argent -= t.prixHotel;
                    t.nbHotelConstruits ++;
                    this.nbHotelPossedes++;
                    Console.WriteLine("Vous avez construit un hôtel sur {0}", t.nom_case);
                }
            }
        }

        public void defaiteJoueur() // lorsqu'un joueur ne peux plus payer
        {
            Console.WriteLine("Vous n'avez pas assez d'argent pour payer le loyer, vous avez perdu.");
            this.statut = statutJoueur.perdu;
            Console.ReadLine();
            Console.Clear();
        }

        public void tirerUneCarte (List<Cartes> l) // tire une carte dans une liste (communauté ou chance) et l'ajoute à la main du joueur si c'est libéré de prison
        {
            Cartes c = l[0];
            l.Remove(c);
            if (c.nomCarte == "Allez en prison.Avancez tout droit en prison.Ne passez pas par la case depart.Ne recevez pas 200e")// ameliorer en cherchant la classe plutot
            {
                cartesDuJoueur.Add(c);
            }
            else
            {

                l.Add(c);
                c.EffetCarte(this);
            }
        }

        public void infoJoueur() // permet d'afficher les infos relatives à un joueur
        {
            Console.Clear();
            Console.WriteLine("Nom du joueur : " + this.nom_joueur + "\nArgent : " + this.argent + "\nPosition : " + this.position);
            int i = 1;
            int taille=proprieteDuJoueur.Count;
            int taille1=cartesDuJoueur.Count;
            if (taille >0)
            {
            Console.WriteLine("\nListe des cartes de propriétés :");
            foreach (Propriete p in proprieteDuJoueur)
            {
                    if (p is Terrain )
                    {
                        Terrain t = p as Terrain;
                        Console.WriteLine(i + ": " + p.nom_case + "   Couleur : " + t.Couleur + "   Nb de Maisons : " + t.nbMaisonConstruites + "   Nb d'hôtels : " + t.nbHotelConstruits);
                    }
                    else
                    {
                        Console.WriteLine(i + ": " + p.nom_case);
                    }
                
                i++;
            }
            }
            if (taille1 > 0)
            {
                foreach (Cartes carte in cartesDuJoueur)
                {
                    Console.WriteLine("Carte :");
                    Console.WriteLine(carte.nomCarte);
                }
            }

            
            Console.WriteLine("\nVoulez vous en savoir plus sur une carte de Propriété  ? Si oui, taper le numéro correspondant, sinon taper 0");
            int c;
            bool erreur=true;
            do
            {
                try
                {
                    c = int.Parse(Console.ReadLine());

                    if (c != 0)
                    {
                        if (c < taille+1)
                        {
                            Propriete p1 = proprieteDuJoueur[c - 1];
                            p1.affiche_info_propriete();
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
      
    }
}


