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
    public class Gare : Propriete
    {


        public Gare(string nom_case, double prix, double prixL, double valHyp) : base(nom_case, prix, prixL, valHyp)
        {

        }

        public override double calculeLoyer(Joueur j, Joueur TombeSurCase)
        {
            int nombreGares = j.calculeNombreGares();
            prixLoyer = 25 * nombreGares;
            return prixLoyer;
        }

        public void affiche_info_gare()
        {
            
                Console.Clear();
                Console.WriteLine("Nom : " + nom_case + "\nPrix : " + prixAchat + "\nTerrain nu : " + prixLoyer + "\nAvec 2 gares : " + prixLoyer * 2 + "\nAvec 3 gares : " + prixLoyer * 3 + "\nAvec 4 gares : " + prixLoyer * 4);

            


        }

    }
}

