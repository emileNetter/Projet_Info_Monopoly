using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Monopoly
{
    public class Des
    {
        private static Random r = new Random();
        protected int valeur1;
        public Des ()
        {
            
        }

        public int jetteDes()
        {
            valeur1 = r.Next(1, 7);
            
            return valeur1;

        }

        public int sommeDes(Des d)
        {
            int somme = valeur1 + d.valeur1;
            return somme;
        }

        public Boolean sontIdentiques(Des d)
        {
            if (valeur1 == d.valeur1)
            {
                return true;
            }
            return false;
        }

        public void afficheValeurDe()
        {
            Console.WriteLine(valeur1);
        }
    }
}
