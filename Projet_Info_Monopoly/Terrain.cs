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
    public class Terrain : Propriete
    {
        protected double prixMaison { get; set; }
        protected double prixHotel { get; set; }
        protected int nbMaisonConstruites { get; set; }
        protected int nbHotelConstruits { get; set; }
        protected double prix1Maison{get;set;}
        protected double prix2Maison { get; set; }
        protected double prix3Maison { get; set; }
        protected double prix4Maison { get; set; }
        protected double prix1Hotel { get; set; }
        public enum couleur { bleu,cyan,rose, marron,orange,rouge,jaune,vert};
        public couleur Couleur { get; set; }

        public Terrain(double prixM, double prixH, string nom_case, double prix, double prixL,double m1,double m2,double m3,double m4, double h1, double valHyp, couleur c):base(nom_case,prix,prixL,valHyp)
        {
            prixMaison = prixM;
            prixHotel = prixH;
            nbMaisonConstruites = 0;
            nbHotelConstruits = 0;
            Couleur = c;
            prix1Maison = m1;
            prix2Maison = m2;
            prix3Maison = m3;
            prix4Maison = m4;
            prix1Hotel = h1;

        }

        public int nbreTerrainCouleur(Plateau p)
        {
            int nb=0;
                 foreach( Terrain t in p.cases) 
                {
                     if (t.Couleur==this.Couleur)
                     {
                         nb++;
                     }
                 }
            return nb;
                
        }





        public bool peutConstruireMaison(Joueur j, Plateau p)
        {
            if (j.calculeNombreTerrainCouleur(this) == nbreTerrainCouleur(p))
            {
                return true;
            }
            return false;
        }

            

        public bool peutConstruireHotel(Joueur j)
        {
            if (nbMaisonConstruites == 4)
            { return true; }
            return false;
        }

        public override double calculeLoyer(Joueur j)
        {
            int nb = this.nbMaisonConstruites;
            if (nb == 1)
            {
                prixLoyer = prix1Maison;
            }
            else if (nb == 2)
            {
                prixLoyer = prix2Maison;
            }
            else if (nb == 3)
            {
                prixLoyer = prix3Maison;
            }
            else if (nb == 4)
            {
                prixLoyer = prix4Maison;
            }
            else if (nbHotelConstruits == 1)
            {
                prixLoyer = prixHotel;
            }
            return prixLoyer;
        }


            
        

        public void affiche_info_terrain()
        {
<<<<<<< HEAD
                       
                Console.Clear();
                Console.WriteLine("Groupe de couleur : " + Couleur + "\nNom : " + nom_case + "\nPrix : " + prixAchat + "\nTerrain nu : " + prixLoyer + "\nAvec 1 maison : " + prix1Maison + "\nAvec 2 maisons : " + prix2Maison + "\nAvec 3 maisons : " + prix3Maison + "\nAvec 4 maisons : " + prix4Maison + "\nAvec hôtel : " + prix1Hotel);

=======
            
                Console.Clear();
                Console.WriteLine("Groupe de couleur : " + Couleur + "\nNom : " + nom_case + "\nPrix : " + prixAchat + "\nTerrain nu : " + prixLoyer + "\nAvec 1 maison : " + prix1Maison + "\nAvec 2 maisons : " + prix2Maison + "\nAvec 3 maisons : " + prix3Maison + "\nAvec 4 maisons : " + prix4Maison + "\nAvec hôtel : " + prix1Hotel);

            

>>>>>>> 22e551a695603c40b829b6d5a5f13625c4440a2d
        }
      
    }
}


