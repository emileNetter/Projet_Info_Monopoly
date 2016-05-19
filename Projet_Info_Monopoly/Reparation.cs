using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Info_Monopoly
{
    public class Reparation : Transaction
    {
        public Reparation(typeCarte type, string nom, double valueMaison, double valueHotel)
            : base(type, nom,valueMaison)
        {
        }

        public override void EffetCarte(Joueur j)
        {
            
            int nbMaison = j.nbMaisonPossedes;
            int nbHotel = j.nbHotelPossedes;
            valeur = 25 * nbMaison + 100 * nbHotel;
            j.argent -= valeur;
            Console.WriteLine(nomCarte);
            Console.WriteLine("Vous avez {0} maison(s) et {1} hotel(s). Vous payez donc : {2} . Il vous reste desormais {3}.", nbMaison, nbHotel, valeur, j.argent);
            Console.ReadLine();
            Console.Clear();

        }
            
    }
}
