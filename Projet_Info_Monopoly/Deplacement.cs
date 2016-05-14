﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Projet_Info_Monopoly
{
    public class Deplacement : Cartes
    {
        public int mouvement; //deplacement d'un nombre précis de cases
        public int deplacementVersCase; //deplacement vers une case précise

        public Deplacement(typeCarte t, string nom, int mouv, int deplacementCase):base(t,nom)
        {
            mouvement = mouv;
            deplacementVersCase = deplacementCase;

        }

        public override void EffetCarte(Joueur j)
        {
            base.EffetCarte(j);
            int anciennePosition=j.position; //on  stocke l'ancienne position du joueur.            
            if (this.mouvement == 0)
            {
                j.position = deplacementVersCase; // on va sur la case donnée par la carte grâce à l'id porté par deplacementVersCase

                if (j.position == 10)// case de la prison
                {
                    j.statut = Joueur.statutJoueur.enPrison;
                }
                else
                {
                    if (j.position < anciennePosition)// on passe par la case départ que si on arrive sur une case dont le numéro de case est inférieur à celui de la case sur laquelle on était
                    {
                        j.argent += 200;//définir dans xml l'argent qu'on gagne lorsqu'on passe par case départ
                    }
                }

            }
            else
            {
                j.position += mouvement;
                if (j.position >= 40)
                {
                    j.position = j.position % 40;
                    j.argent += 200;//définir dans xml l'argent qu'on gagne lorsqu'on passe par case départ
                }
            }
        }
    

            
               
        
    }
}


