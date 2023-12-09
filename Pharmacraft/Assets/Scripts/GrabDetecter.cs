using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetecter : MonoBehaviour
{
    public Transform GrabDetect;
    public Transform ObjHolder;
    public float rayDist;
    private bool isHoldingItem = false;

    public bool IsHoldingItem()
    {
        return isHoldingItem;
    }

    public GameObject GetHeldItem()
    {
        if (ObjHolder.childCount > 0)
        {
            return ObjHolder.GetChild(0).gameObject;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(GrabDetect.position, Vector2.right * transform.localScale, rayDist);

        if (grabCheck.collider != null && grabCheck.collider.tag == "ItemPegavel")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (!isHoldingItem)
                {
                    grabCheck.collider.gameObject.transform.parent = ObjHolder;
                    grabCheck.collider.gameObject.transform.position = ObjHolder.position;
                    grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    isHoldingItem = true;
                }
            }
            else if (isHoldingItem && !Input.GetKey(KeyCode.Space))
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                isHoldingItem = false;
            }
        }
    }

    public void getItemFromMachine(GameObject item)
    {
        item.transform.parent = ObjHolder;
        item.transform.position = ObjHolder.position;
        item.GetComponent<Rigidbody2D>().isKinematic = true;
        isHoldingItem = true;
    }
}
