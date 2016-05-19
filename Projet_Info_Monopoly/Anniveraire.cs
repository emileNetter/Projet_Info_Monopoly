using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Info_Monopoly
{
    public class Anniveraire : Transaction 
    {
        public Anniveraire(typeCarte type, string nom, double value)
            : base(type, nom, value)
        {

        }

        public override void EffetCarte(Joueur j)
        {
            foreach (Joueur autrej in j.partie.joueurs)
            {
                if (j != autrej)
                {
                    autrej.argent -= valeur;
                    j.argent += valeur;
                }
                Console.WriteLine("Vous avez désormais " + j.argent);
            }
            Console.ReadLine();
            Console.Clear();
        }
    }
}
