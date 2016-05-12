using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{
    class Impot: Cases
    {
        
        double prixAPayer { get; set; }
        public Impot(string nom_case, double prix):base(nom_case)
        {
            prixAPayer = prix;
            
        }

        public override string ToString()
        {
            return base.ToString() + " Payez 200 euros à la banque";
        }

    }
}
