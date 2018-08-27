using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;




public class Pickup : MonoBehaviour {

	public Camera cam;
	public Transform hand;
    public float shrinkScale = .25f;
    public GameObject heldObject;
    public Vector3 dropPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{

        RaycastHit hit = ShootRay();
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }

        if (Input.GetKeyDown(KeyCode.E))
		{


            if (hit.collider != null)
            {
                Collider collider = hit.collider;
                // update shader

                if (heldObject == null)
                {

                    PickupObj(collider);

                    if (collider.gameObject.GetComponent<Equipment>() != null)
                    {
                        Equipment equipment = collider.gameObject.GetComponent<Equipment>();
                        if(equipment.itemList.Count >0)
                        {
                            
                            equipment.ProcessItem();
                        }

                    }


                }
                else if (heldObject != null)
                {
                    dropPosition = hit.point;


                    if (collider.gameObject.GetComponent<Equipment>() != null)
                    {

                        Debug.Log("Looking at the Equipment");
                        Equipment equipment = collider.gameObject.GetComponent<Equipment>();

                        if (heldObject != null)
                        {
                            equipment.DropObjInEquipment(heldObject, this);
                        }
                        else if (equipment.placedObj != null)
                        {
                            if(collider.GetComponent<Interactable>() != null)
                            {
                                PickupObj(collider);
                            }
                        }
                        

                    }
                    else
                    {
                        DropObj(heldObject);
                    }
                }
            }

        }
		
		
		
	}

    public RaycastHit ShootRay()
    {
        RaycastHit hit;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, 10f))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);
            



           
            

        }
        else
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);

        }

        return hit;

    }

    public void SetTransfom(Collider collider)
    {

        if(collider.gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(collider.gameObject.GetComponent<Rigidbody>());
        }

        collider.enabled = false;
        collider.transform.position = hand.position;
        collider.transform.localScale = Vector3.one * shrinkScale;
        collider.gameObject.transform.SetParent(hand);
        heldObject = collider.gameObject;
        collider.gameObject.GetComponent<Interactable>().isPickedUp = true;
    }

    public void DropObj(GameObject curHeldObj)
    {
        curHeldObj.transform.SetParent(null);
        curHeldObj.GetComponent<Collider>().enabled = true;
        curHeldObj.transform.localScale = Vector3.one;
        curHeldObj.AddComponent<Rigidbody>();
        curHeldObj.transform.localPosition = dropPosition + new Vector3(0,1,0);
        curHeldObj.GetComponent<Interactable>().isPickedUp = false;

        heldObject = null;
    }


    /*public void PickupObjFromMortar(Mortar mortar)
    {
        if (mortar != null)
        {
            if (mortar.placedObj != null)
            {
                GameObject obj = mortar.placedObj;
            }
        }
    }*/

    public void PickupObj(Collider collider)
    {
        if (collider != null)
        {
            if (collider.gameObject.GetComponent<Interactable>() != null)
            {
                Interactable hitInteract = collider.gameObject.GetComponent<Interactable>();

                if (hitInteract.canPickup == true)
                {
                    if(hitInteract.curEquipment == null)
                    {
                        SetTransfom(collider);
                    }else if(hitInteract.curEquipment.GetComponent<Equipment>())
                    {
                        hitInteract.curEquipment.GetComponent<Equipment>().PickupObjFromEquipment(collider.gameObject, this);

                    }
                }

            }
            else
            {
                return;
            }
        }
        
    }

    



	
}
