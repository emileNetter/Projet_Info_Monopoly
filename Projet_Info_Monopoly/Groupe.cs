using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{
    class Groupe
    {
        private string categorie;
        public Groupe(string c)
        {
            categorie = c;
        }

        public string getCategorie()
        {
            return categorie;
        }
        
    }
}
