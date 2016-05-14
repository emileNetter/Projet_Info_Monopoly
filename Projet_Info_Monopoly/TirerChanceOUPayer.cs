using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_Info_Monopoly
{
    public class TirerChanceOUPayer : Transaction
    {
        public TirerChanceOUPayer(typeCarte type, string nom, double value)
            : base(type, nom, value)
        {
        }

        public override void EffetCarte(Joueur j)
        {
            Console.WriteLine("Que faites vous ? 'o' pour payer 'n' pour tirer carte chance");
            ConsoleKeyInfo c;
                do
                {
                    c = Console.ReadKey();
                }
                while (c.KeyChar != 'o' && c.KeyChar != 'n');
                if (c.KeyChar == 'o')
                {
                    j.argent += valeur;
                    Console.WriteLine("Vous avez desormais" + j.argent);
                }
                else
                {
                    j.tirerUneCarte(j.plateau.cartesChance);
                }

        }
    }
}
