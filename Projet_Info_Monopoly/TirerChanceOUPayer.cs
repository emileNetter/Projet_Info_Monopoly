﻿using System;
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
            base.EffetCarte(j);
            Console.WriteLine("Que faites vous ? 'p' pour payer 't' pour tirer carte chance");
            ConsoleKeyInfo c;
                do
                {
                    c = Console.ReadKey();
                }
                while (c.KeyChar != 'p' && c.KeyChar != 't');
                if (c.KeyChar == 'p')
                {
                    j.argent += valeur;
                    Console.WriteLine("Vous avez désormais" + j.argent);
                }
                else
                {
                    j.tirerUneCarte(j.plateau.cartesChance);
                }

        }
    }
}
