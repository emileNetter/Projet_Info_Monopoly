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
            Des d1 = new Des();
            Des d2 = new Des();
            d1.jetteDes();
            d2.jetteDes();
            d1.afficheValeurDe();
            d2.afficheValeurDe();
           int result=d1.sommeDes(d2);
            Console.WriteLine(result);
            Console.WriteLine(d1.sontIdentiques(d2));

        }
    }
}
