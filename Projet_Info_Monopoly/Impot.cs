using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{
<<<<<<< HEAD
    public class Impot:Cases
=======
    class Impot: Cases
>>>>>>> 8a16a6a69149ce5e7c412f2576961ae5d71505ba
    {
        
        public double  prixAPayer { get; set; }
        public Impot(string nom_case, double prix):base(nom_case)
        {
            prixAPayer = prix;
            
        }

<<<<<<< HEAD
        
=======
        public override string ToString()
        {
            return base.ToString() + " Payez 200 euros à la banque";
        }
>>>>>>> 8a16a6a69149ce5e7c412f2576961ae5d71505ba

    }
}
