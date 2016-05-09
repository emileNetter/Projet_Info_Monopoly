using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{
    class Program
    {
        static void Main(string[] args)
        {


            //LinkedList<Joueur> l = new LinkedList<Joueur>();
            //l.AddLast(new Joueur("Emile"));
            //l.AddLast(new Joueur("Thomas"));
            //Plateau p = new Plateau(l);
            Partie partie1 = new Partie();
            partie1.partie();

        }
    }
}
