using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{//honestly this should be named ingredients

    public bool canPickup;
    public bool isPickedUp;

    public IngredientState curState;

    public Ingredients ingredientType;
    public GameObject bowlGFX;

    public GameObject beakerGFX;

    public GameObject rawGFX;


    public GameObject curEquipment;

    public bool isRaw;
    public bool isGround;
    public bool isBoiled;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
