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
            Console.WriteLine("Vous êtes arrété par la police. Allez en prison, ne passez pas par la case départ");
            j.position = 10;
            j.statut = Joueur.statutJoueur.enPrison;
        }
    }
}
