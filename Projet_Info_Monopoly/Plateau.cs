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
using System.Xml;
using System.Xml.Linq;
namespace Projet_Info_Monopoly
{
    public class Plateau
    {
        public Cases [] cases { get; set; }
        public List<Cartes> cartesChance {get; set; }
        public List<Cartes> cartesCommunaute { get; set; }
        

        public Plateau()
        {
            cases = new Cases[40];
        cartesChance = new List<Cartes>();
        cartesCommunaute = new List<Cartes>();
            generePlateau();

            
        }

        public void generePlateau ()
        {
            XDocument doc = XDocument.Load("Plateau.xml");
            var jeu = doc.Descendants("jeu").First();
            var plateau = doc.Descendants("plateau").First();
            var groupe = jeu.Descendants("groupe");
            var gares = jeu.Descendants("gare").First();
            var compagnie = jeu.Descendants("compagnie").First();
            var cartes = doc.Descendants("cartes").First();

            foreach (var g in groupe)
            {
                var terrain = g.Descendants("terrain");
                foreach (var t in terrain)
                {
                    cases[(int)t.Attribute("id")] = new Terrain((double)g.Attribute("maison"), 1000, (string)t.Attribute("nom"), (double)t.Attribute("prix"), (double)t.Attribute("t0"), (double)t.Attribute("t1"), (double)t.Attribute("t2"), (double)t.Attribute("t3"), (double)t.Attribute("t4"), (double)t.Attribute("t5"),(double)t.Attribute("hyp"), (Terrain.couleur)Enum.Parse(typeof(Terrain.couleur), (string)g.Attribute("couleur")));
                }
            }

            var gare = plateau.Descendants("gare");
            foreach (var ga in gare)
            {
                cases[(int)ga.Attribute("id")] = new Gare((string)ga.Attribute("nom"), (double)gares.Attribute("prix"), (double)gares.Attribute("t0"), (double)gares.Attribute("hyp"));
            }
            var impot = plateau.Descendants("impot");
            foreach (var t in impot)
            {
                cases[(int)t.Attribute("id")] = new Impot((string)t.Attribute("nom"), (double)t.Attribute("prix"));
            }
            var compagnies = plateau.Descendants("compagnie");
            foreach (var c in compagnies)
            {
                cases[(int)c.Attribute("id")] = new Compagnie((string)c.Attribute("nom"), (double)compagnie.Attribute("prix"), (double)compagnie.Attribute("mul1"), (double)compagnie.Attribute("hyp"));
            }
            cases[20] = new ParcGratuit();
            cases[30] = new Police();
            cases[10] = new Prison();
            cases[0] = new Depart();


             var caseCarte = plateau.Descendants("carte");
        foreach(var c in caseCarte)
        {
            if ((string)c.Attribute("type")=="Communaute")
            {
                cases[(int)c.Attribute("id")] = new CasesCommunautes();
                
            }
            else
            {

                cases[(int)c.Attribute("id")] = new CasesChances();
            }
        }
        

        var caisseCom = cartes.Descendants("paquet");
        

        foreach (var p in caisseCom)
        {
            var carte = p.Descendants("carte");

            if ((string)p.Attribute("type")=="Communaute") // creation des cartes communaute
            {
                
                foreach (var c in carte)
                {
                    


                    if ((string)c.Attribute("type") == "transaction")
                    {
                        Transaction nvCarte = new Transaction(Cartes.typeCarte.communaute, (string)c.Attribute("nom"), (double)c.Attribute("valeur")); 
                        cartesCommunaute.Add(nvCarte);
                    }
                    
                    else if ((string)c.Attribute("type") == "deplacement")
                    {
                        Deplacement nvCarte = new Deplacement(Cartes.typeCarte.communaute, (string)c.Attribute("nom"), (int)c.Attribute("dep"), (int)c.Attribute("id"));
                        cartesCommunaute.Add(nvCarte);
                    }
                    
                    /*else if ((string)c.Attribute("type") == "libere") TODO 
                    {
                        LibereDePrison newCarte = new LibereDePrison(Cartes.typeCarte.communaute, (string)c.Attribute("nom"));
                        cartesCommunaute(newCarte);
                    }*/
                }
                
            }
            else if ((string )p.Attribute("type") == "Chance") //creation des cartes chance
            {
                foreach (var c in carte)
                {
                    if ((string)c.Attribute("type") == "argent")
                    {
                        Transaction nvCarte = new Transaction(Cartes.typeCarte.chance, (string)c.Attribute("nom"), (double)c.Attribute("valeur"));
                        cartesChance.Add(nvCarte);
                    }
                        else if ((string)c.Attribute("type") == "deplacement")
                    {
                        Deplacement nvCarte = new Deplacement(Cartes.typeCarte.chance, (string)c.Attribute("nom"), (int)c.Attribute("dep"), (int)c.Attribute("id"));
                        cartesChance.Add(nvCarte);
                    }
                    /*else if ((string)c.Attribute("type") == "libere")
                    {
                        LibereDePrison nvCarte = new LibereDePrison(Cartes.TypeC.chance, (string)c.Attribute("nom")); TODO 
                        addCartesChance(nvCarte);
                    }*/
                    
                }
            }
        }
        
    }

             
        

       
    }
}


