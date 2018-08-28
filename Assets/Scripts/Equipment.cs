using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{

    public enum EquipmentType { mortar, cauldron, distiller };
    public EquipmentType curEquipType;

    public List<Transform> positionList = new List<Transform>();
    //public Transform placePosition;
    public bool isFilled = false;
    public GameObject placedObj;

    public int maxObjects;
    public int numOfObjects;
    public bool full = false;

    //public GameObject grindObj;

    public List<GameObject> itemList = new List<GameObject>();

    public GameObject tempPrefab;

    public GameObject particles;

    public Recipes recipeBook;


    // Use this for initialization
    void Start()
    {
        particles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfObjects >= maxObjects)
        {
            full = true;
        }
        else
        {
            full = false;
        }
    }


    public void DropObjInEquipment(GameObject curHeldObj, Pickup pickupScript)
    {
        if (full == false)
        {
            curHeldObj.transform.SetParent(positionList[itemList.Count]);
            curHeldObj.GetComponent<Collider>().enabled = true;
            curHeldObj.transform.localScale = Vector3.one;
            //curHeldObj.AddComponent<Rigidbody>();
            curHeldObj.transform.position = positionList[itemList.Count].position;

            curHeldObj.transform.rotation = Quaternion.identity;
            curHeldObj.GetComponent<Interactable>().isPickedUp = false;
            curHeldObj.GetComponent<Interactable>().curEquipment = this.gameObject;
            isFilled = true;
            placedObj = curHeldObj;
            pickupScript.heldObject = null;
            itemList.Add(curHeldObj);
            numOfObjects++;

            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].transform.position = positionList[i].position;
            }
        }
    }

    public void PickupObjFromEquipment(GameObject obj, Pickup pickupScript)
    {
        obj.transform.SetParent(pickupScript.hand);
        obj.GetComponent<Collider>().enabled = false;
        obj.transform.position = pickupScript.hand.position;
        //obj.transform.localScale = Vector3.one * pickupScript.shrinkScale;
        obj.transform.rotation = Quaternion.identity;
        pickupScript.heldObject = obj;
        obj.GetComponent<Interactable>().isPickedUp = true;
        obj.GetComponent<Interactable>().curEquipment = null;
        itemList.Remove(obj);
        numOfObjects--;
        particles.SetActive(false); //switch off active particles when picked up
    }

    public void ProcessItem()
    {
        Debug.Log("Mortar Grind" + itemList[0].name);
        GameObject processObj = itemList[0].gameObject;
        Interactable objInteract = processObj.GetComponent<Interactable>();
        objInteract.isRaw = false;

        //Make this a switch 

        if (curEquipType == EquipmentType.mortar)
        {
            Grind(objInteract);
        }
        else if (curEquipType == EquipmentType.cauldron)
        {
            Boil(objInteract);
        }
        else if (curEquipType == EquipmentType.distiller)
        {
            if (full == true)     //This is HARDCODED, could probs make a reference to max objects
            {
                CreatePotion();
            }
        }
    }

    public void Grind(Interactable objInteract) // grind, add particle efect here
    {
        if (objInteract.curState == IngredientState.raw)
        {
            objInteract.rawGFX.SetActive(false);
            objInteract.bowlGFX.SetActive(true);
            objInteract.isGround = true;
            objInteract.isRaw = false;
            objInteract.curState = IngredientState.ground;
        }
    }

    public void Boil(Interactable objInteract) // boil , add particle here
    {
        if (objInteract.curState == IngredientState.raw)
        {
            particles.SetActive(true);
            Debug.Log("boiling cauldern");
            objInteract.rawGFX.SetActive(false);
            objInteract.beakerGFX.SetActive(true);
            objInteract.isBoiled = true;
            objInteract.isRaw = false;
            objInteract.curState = IngredientState.boiled;
        }
    }

    public void CreatePotion()
    {

        //I'm pretty lost as to what to do now, Maaaaybe a new class or function where you pass each ingredient type
        //&& if statements for the potions, problem is you gotta pass in teh bools for boiled & ground
        //Maybe it would be easiest if it was an enum for ground & boiled??
        //Ok I did the enum, i'll leave the status bools for now, theyre not hurting anyone
        //honestly lets try else ifs for each potion????

        Debug.Log(itemList[0].GetComponent<Interactable>().ingredientType + " " + itemList[0].GetComponent<Interactable>().curState.ToString() +
        " + " + itemList[1].GetComponent<Interactable>().ingredientType + " " + itemList[1].GetComponent<Interactable>().curState.ToString() +
        " + " + itemList[2].GetComponent<Interactable>().ingredientType + " " + itemList[2].GetComponent<Interactable>().curState.ToString());

        CheckRecipe(itemList[0].GetComponent<Interactable>(), itemList[1].GetComponent<Interactable>(), itemList[2].GetComponent<Interactable>());
    }

    public void CheckRecipe(Interactable ing1, Interactable ing2, Interactable ing3)
    {
        //Bool to determine failure of the attempt, defaults to true
        bool isFailed = true;
        //Loop through each recipe in the provided book
        foreach (Recipe recipe in recipeBook.recipes)
        {
            //If the given ingredients (in the given order) match a recipe
            if (ing1.ingredientType == recipe.ingredientOne &&
                ing1.curState == recipe.stateOne &&
                ing2.ingredientType == recipe.ingredientTwo &&
                ing2.curState == recipe.stateTwo &&
                ing3.ingredientType == recipe.ingredientThree &&
                ing3.curState == recipe.stateThree)
            {
                Debug.Log(recipe.potionName + " Created!");
                //Set as a successful potion
                isFailed = false;
                //Grab Interactable script from tempPrefab, and initialise variables to recipe's resulting potion
                Interactable potionInteract = tempPrefab.GetComponent<Interactable>();
                potionInteract.ingredientType = recipe.potionName;
                potionInteract.curState = IngredientState.raw;
                potionInteract.isRaw = true;
                potionInteract.isGround = false;
                potionInteract.isBoiled = false;
                //Destroy the GameObjects in the list before clearing the list
                Destroy(itemList[0]);
                Destroy(itemList[1]);
                Destroy(itemList[2]);
                //Empty the list of items, and set numObjects to 0
                itemList.Clear();
                numOfObjects = 0;
                //Instantiate the potion prefab
                Instantiate(tempPrefab, positionList[positionList.Count - 1].position, Quaternion.identity);
            }
        }
        //Respond to failed potion
        if (isFailed)
        {
            //Will need to destroy ingredients in here if that is the intention
            Debug.Log("EXPLOSION!!");
        }
    }
}

