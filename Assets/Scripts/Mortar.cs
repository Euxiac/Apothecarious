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

    public List<GameObject> itemList = new List<GameObject>();

    

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
            curHeldObj.transform.rotation = Quaternion.identity;
            curHeldObj.GetComponent<Interactable>().isPickedUp = false;
            curHeldObj.GetComponent<Interactable>().curEquipment = this.gameObject;
            isFilled = true;
            placedObj = curHeldObj;
            pickupScript.heldObject = null;
            itemList.Add(curHeldObj);
            numOfObjects++;
        }
        


    }

    public void PickupObjFromMortar(GameObject obj, Pickup pickupScript)
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


}
