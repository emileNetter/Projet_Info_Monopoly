using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{
    class Impot:Cases
    {
        string nom;
        double prixAPayer;
        public Impot(string n, double prix):base()
        {
            nom = n;
            prixAPayer = prix;
        }

    }
}
