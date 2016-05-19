using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{
    class Police : Cases
    {
        public Police():base("Police")
        {
        }

        public void arrestationPolice(Joueur j)
        {

            Console.WriteLine("Vous êtes arrétés par la police, aller en prison, ne passer pas par la case depart");
            j.position = 10;
            j.statut = Joueur.statutJoueur.enPrison;
        }
    }
}
