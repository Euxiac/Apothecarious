using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Combination struct used in list to define potion combinations
//Struct is comprised of three Ingredients and a potion name. Is a storage medium only
[System.Serializable]
public struct Recipe
{
    public Ingredients potionName;
    public Ingredients ingredientOne;
    public IngredientState stateOne;
    public Ingredients ingredientTwo;
    public IngredientState stateTwo;
    public Ingredients ingredientThree;
    public IngredientState stateThree;
}

//CombinationDemo class implements a public list of combinations. This allows designers to specify potion combinations without needing to hard-code them.
public class Recipes : MonoBehaviour
{
    //List of potions
    public List<Recipe> recipes;

    // Use this for initialization
    // Specifically, for the Recipe Book object when designed
    void Start()
    {

    }

    // Update is called once per frame
    // Specifically, update Recipe Book object when designed
    void Update()
    {

    }
}
