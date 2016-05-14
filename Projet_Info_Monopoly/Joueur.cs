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
        private List<Cartes> cartesDuJoueur; // 
        public List<Propriete> proprieteDuJoueur;
        private static Random r = new Random();
        public Plateau plateau;
        public int dernierLanceDe;
        public int nbTourEnPrison;
        public int nbMaisonPossedes;
        public int nbHotelPossedes;
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
                    //this.addCard(carte qui correspond à la propriete)
                }

            }
            else if (this.argent < p.prixAchat)
            {
                Console.WriteLine("Vous n'avez pas assez d'argent pour acheter cette propriété");
                Console.Clear();
            }

        }

        public void paye_loyer(Propriete p, Partie partie)
        {
            
            if (p.estPossedee == true)
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
                            Console.WriteLine("Vous devez payer " + loy + " à " + j.nom_joueur);
                            j.argent += loy;
                            this.argent -= loy;
                            Console.WriteLine(j.nom_joueur + " a désormais " + j.argent);
                            Console.WriteLine("Vous avez désormais " + this.argent);
                            Console.ReadLine();
                            Console.Clear();
                        }
                        
                    }
                }
            }
        }

        public void payeImpot(Impot impot)
        {
            if (impot.prixAPayer > this.argent)
            {
                this.defaiteJoueur();
            }
            else
            {
                Console.WriteLine("Vous devez payez une taxe de " + impot.prixAPayer);
                this.argent -= impot.prixAPayer;
                Console.WriteLine("Il vous reste : " + this.argent);
            }
            
        }


        public int avancer()
        {
            position += lanceDe();
            if (position >= 40)
            {
                position = position % 40;
                argent += 200; // pouvoir définir une valeur modifiable depuis le XML
            }
            return position;


        }

        public int lanceDe()
        {
            int compteurDouble = 0;
            int de1 = r.Next(1,3);
            int de2 = r.Next(1,3);
            int total = de1 + de2;

            if (de1 == de2)
            {
                Console.WriteLine(nom_joueur + " a fait  un double  " + de1 + " ! " + "(" + 2 * de1 + ")");
                compteurDouble++;
            }
            else
            {
                Console.WriteLine(nom_joueur + " a fait : " + total + " (" + de1 + "+" + de2 + ")");
            }
            dernierLanceDe = total;// on récupere le résultat de lancer de dé en cas de tomber sur une case de type compagnie necessitant de connaitre le lancer de dé afin d'établir le prix du loyer
            return total;
        }

        public void addCard(Cartes c)
        {
            cartesDuJoueur.Add(c);

        }
        public void removeCard(Cartes c)
        {

        }
        public void debiter(int somme)
        {
            this.argent -= somme;
        }

        public int calculeNombreTerrainCouleur(Terrain t)
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

        public int calculeNombreGares()
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

        public int calculeNombreCompagnies()
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

        public void construireMaison(Terrain t)
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

        public void construireHotel(Terrain t)
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

        public void defaiteJoueur()
        {
            Console.WriteLine("Vous n'avez pas assez d'argent pour payer le loyer, vous avez perdu.");
            this.statut = statutJoueur.perdu;
            Console.ReadLine();
            Console.Clear();
        }

        public void tirerUneCarte (List<Cartes> l)
        {
            Cartes c = l[0];
            l.Remove(c);
            if (c.nomCarte == "Libere de prison")
            {
                cartesDuJoueur.Add(c);
            }
            else
            {

                l.Add(c);
                c.EffetCarte(this);
            }
        }
      
    }
}


