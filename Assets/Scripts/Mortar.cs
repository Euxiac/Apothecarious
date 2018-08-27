using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour {

    public Transform placePosition;
    public bool isFilled = false;
    public GameObject placedObj;

    public int maxObjects;
    public int numOfObjects;
    public bool full = false;

    

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


    public void DropObjInMortar(GameObject curHeldObj, Pickup pickupScript)
    {
        if(full == false)
        {
            curHeldObj.transform.SetParent(this.placePosition);
            curHeldObj.GetComponent<Collider>().enabled = true;
            curHeldObj.transform.localScale = Vector3.one;
            //curHeldObj.AddComponent<Rigidbody>();
            curHeldObj.transform.localPosition = placePosition.localPosition;
            curHeldObj.GetComponent<Interactable>().isPickedUp = false;
            isFilled = true;
            placedObj = curHeldObj;
            pickupScript.heldObject = null;
            numOfObjects++;
        }
        


    }

    public void PickupObjFromMortar()
    {
        /*if (mortar != null)
        {
            if (mortar.placedObj != null)
            {
                GameObject obj = mortar.placedObj;
            }
        }*/
    }


}
