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

public class AAcheter : Cartes
{
	public int prixAchat
	{
		get;
		set;
	}

	public virtual string nom_carte
	{
		get;
		set;
	}

	public virtual int valHypothec
	{
		get;
		set;
	}

	public virtual bool estPossedee
	{
		get;
		set;
	}

	public virtual int prixAPayer
	{
		get;
		set;
	}
    public AAcheter(int prix, string nom, bool estPoss, int aPayer)
    {
        prixAchat = prix;
        nom_carte = nom;
        estPossedee = estPoss;
        prixAPayer = aPayer;
    }

}

