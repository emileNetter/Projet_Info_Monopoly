using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{

    public class Impot:Cases


    {
        
        public double  prixAPayer { get; set; }

        public Impot(string nom_case, double prix):base(nom_case) //constructeur
        {
            prixAPayer = prix;
            
        }



    }
}
