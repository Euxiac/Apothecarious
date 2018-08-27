using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour {

    public Transform placePosition;
    public bool isFilled = false;
    public GameObject placedObj;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void DropObjInMortar(GameObject curHeldObj)
    {
        curHeldObj.transform.SetParent(this.placePosition);
        curHeldObj.GetComponent<Collider>().enabled = true;
        curHeldObj.transform.localScale = Vector3.one;
        //curHeldObj.AddComponent<Rigidbody>();
        curHeldObj.transform.localPosition = placePosition.localPosition;
        curHeldObj.GetComponent<Interactable>().isPickedUp = false;
        isFilled = true;
        placedObj = curHeldObj;
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
