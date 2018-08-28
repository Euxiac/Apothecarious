using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    public enum EquipmentType {mortar, cauldron, distiller};
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

    public GameObject DeathPotionPrefab; //this is where teh potion gameobject comes from, replace this in another script

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(numOfObjects >= maxObjects)
        {
            full = true;
        }else
        {
            full = false;
        }

        

		
	}


    public void DropObjInEquipment(GameObject curHeldObj, Pickup pickupScript)
    {
        if(full == false)
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

            for (int i = 0; i < itemList.Count;i++)
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

        

    }

    public void ProcessItem()
    {
        GameObject processObj = itemList[0].gameObject;
        Interactable objInteract = processObj.GetComponent<Interactable>();
        objInteract.isRaw = false;

        //Make this a switch 

        if(curEquipType == EquipmentType.mortar)
        {
            Grind(objInteract);
        }
        else if(curEquipType == EquipmentType.cauldron)
        {
            Boil(objInteract);
        }else if(curEquipType == EquipmentType.distiller)
        {
            if(full == true)     //This is HARDCODED, could probs make a reference to max objects
            {
                CreatePotion();   

            }
        }


    }

    public void Grind(Interactable objInteract)
    {
        if(objInteract.curState == Interactable.IngredientState.raw)
        {
            objInteract.rawGFX.SetActive(false);
            objInteract.bowlGFX.SetActive(true);
            objInteract.isGround = true;
            objInteract.curState = Interactable.IngredientState.ground;
        }
        

    }

    public void Boil(Interactable objInteract)
    {        
        if(objInteract.curState == Interactable.IngredientState.raw)
        {
            objInteract.rawGFX.SetActive(false);
            objInteract.beakerGFX.SetActive(true);
            objInteract.isBoiled = true;
            objInteract.curState = Interactable.IngredientState.boiled;
        }
        

    } 

    public void CreatePotion()
    {

        GameObject lePotion = CheckRecipe(itemList[0].GetComponent<Interactable>(),itemList[1].GetComponent<Interactable>(),itemList[2].GetComponent<Interactable>());

        Instantiate(lePotion, positionList[positionList.Count-1].position,Quaternion.identity);


        //I'm pretty lost as to what to do now, Maaaaybe a new class or function where you pass each ingredient type
        //&& if statements for the potions, problem is you gotta pass in teh bools for boiled & ground
        //Maybe it would be easiest if it was an enum for ground & boiled??
        //Ok I did the enum, i'll leave the status bools for now, theyre not hurting anyone
        //honestly lets try else ifs for each potion????

        Debug.Log(itemList[0].GetComponent<Interactable>().ingredientType +" " + itemList[0].GetComponent<Interactable>().curState.ToString() + 
        " + " + itemList[1].GetComponent<Interactable>().ingredientType +" " + itemList[1].GetComponent<Interactable>().curState.ToString() +
        " + " + itemList[2].GetComponent<Interactable>().ingredientType +" " + itemList[2].GetComponent<Interactable>().curState.ToString());

        Debug.Log(lePotion.name);

        //CheckRecipe(itemList[0].GetComponent<Interactable>(),itemList[1].GetComponent<Interactable>(),itemList[2].GetComponent<Interactable>());

        Destroy(itemList[0]); 
        Destroy(itemList[1]);
        Destroy(itemList[2]);
    }

    public GameObject CheckRecipe(Interactable ing1, Interactable ing2, Interactable ing3 )
    {
        if(ing1.ingredientType == Ingredients.Deathcap_Mushroom &&
         ing1.curState == Interactable.IngredientState.ground && 
         ing2.ingredientType == Ingredients.Sleep_Potion &&
         ing2.curState == Interactable.IngredientState.raw &&
         ing3.ingredientType == Ingredients.White_Lily &&
         ing3.curState == Interactable.IngredientState.boiled)
        {
            Debug.Log("WOOOOOOOO");
        }
        return DeathPotionPrefab;
    }


}

