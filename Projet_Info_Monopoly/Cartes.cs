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
    public abstract class Cartes
    {
        public string nomCarte;
        public enum typeCarte { communaute, chance }
        public typeCarte type { get; set; }

        public Cartes(typeCarte t, string nom)
        {
            nomCarte = nom;
            type = t;

        }

        public abstract void EffetCarte(Joueur j);
        
        
       
    }
}


