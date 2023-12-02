using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetecter : MonoBehaviour
{
 public Transform GrabDetect;
    public Transform ObjHolder;
    public float rayDist;
    

    // Update is called once per frame
    void Update()
    {
       RaycastHit2D grabCheck = Physics2D.Raycast(GrabDetect.position, Vector2.right * transform.localScale, rayDist);

       if(grabCheck.collider != null && grabCheck.collider.tag == "ItemPegavel")
       {
            if(Input.GetKey(KeyCode.G))
            {
               grabCheck.collider.gameObject.transform.parent = ObjHolder;
               grabCheck.collider.gameObject.transform.position = ObjHolder.position;
               grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }else{
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
       }
    }
}
