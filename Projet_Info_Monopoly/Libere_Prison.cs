using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Info_Monopoly
{
    public class Libere_Prison : Cartes
    {
         public Libere_Prison( typeCarte type, string fct):base(type, fct)
    {
          
    }

    public override void EffetCarte(Joueur j)
    {
        
        if (j.statut ==Joueur.statutJoueur.enPrison)
        {
            Console.WriteLine("Voulez vous utilisez votre carte pour vous libérer de prison ? o/n");
            ConsoleKeyInfo c;
                do
                {
                    c = Console.ReadKey();
                }
                while (c.KeyChar != 'o' && c.KeyChar != 'n');
                if (c.KeyChar == 'o')
                {
                    j.statut = Joueur.statutJoueur.vivant;
                    Console.WriteLine("Vous êtes libéré(e) de prison");
                }
            

        }
        else
        {
            Console.WriteLine("Vous n'êtes pas en prison, conservez votre carte");
        }
        
    }
    }
}
