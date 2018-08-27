using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Combination struct used in list to define potion combinations
//Struct is comprised of three Ingredients and a potion name. Is a storage medium only
[System.Serializable]
public struct Combination
{
    public string potionName;
    public Ingredients ingredientOne;
    public Ingredients ingredientTwo;
    public Ingredients ingredientThree;
}

//CombinationDemo class implements a public list of combinations. This allows designers to specify potion combinations without needing to hard-code them.
public class CombinationDemo : MonoBehaviour {
    //List of potions
    public List<Combination> potions;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
