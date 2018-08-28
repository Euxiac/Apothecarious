using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ingredients enum contains definitions for each of the ingredients used in potions
//These ingredients need to be renamed
//We can add extra enums for use with processed ingredients etcetera
//Ingredients enum can be used in various sections of code to ensure consistency and prevent bugs/crashing
//For example, it can be used in the Combinations class to determine what ingredients compose potions, and it can be used by the Pestle object to cross-reference attached ingredients
//Ingredient objects should also reference the Ingredients enum internally to determine their own name.
//This is to ensure that all objects/scripts use the same reference material
public enum Ingredients
{
    Distilled_Water,
    Willow_Bark,
    Rock_Salt,
    Deathcap_Mushroom,
    White_Lily,
    Cuttlefish_Ink,
    Mandrake_Root,
    Pearl,
    Quicksilver,
    Unicorn_Horn,
    Phoenix_Ashes,
    Blood,
    Sleep_Potion
}


