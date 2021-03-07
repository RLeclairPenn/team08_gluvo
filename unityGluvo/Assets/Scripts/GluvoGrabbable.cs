using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluvoGrabbable : MonoBehaviour
{

    // NOTE: REQUIRES RIGIDBODY COMPONENT

    // Temporary solutions, fixes the grabbed object to this location
    public GameObject grabAnchor;

    // Keep track of fingers being collided, we need both to be
    // in collision for grabbing
    /// fingerCollision indices for reference:
    /// right_thumb = 0;
    /// right_index = 1;
    /// right_middle = 2;
    /// right_ring = 3;
    /// right_pinky = 4;
    private bool[] fingerCollisions = { false, false, false, false, false };


    void Update()
    {
        CheckAndGrabObject(); 
    }

    // See if the object should be grabbed or dropped,
    // rn only check for collision, can implement more functionality
    void CheckAndGrabObject()
    {
        if (fingerCollisions[0] & (fingerCollisions[1] || fingerCollisions[2] || fingerCollisions[3] || fingerCollisions[4]))       
        {
            transform.parent = grabAnchor.transform;
        } 
        else 
        {
            transform.parent = null;
        }
        // Need to reset velocity or else whacky behavior
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }


    void OnTriggerEnter(Collider other)
    {
        // Update finger collisions
        fingerCollisions[other.gameObject.GetComponent<FingerScript>().fingerNum] = true;
        // Only need to check for grab after updates
        CheckAndGrabObject();
    }


    void OnTriggerExit(Collider other)
    {
        // See if some fingers not colliding anymore
        fingerCollisions[other.gameObject.GetComponent<FingerScript>().fingerNum] = false;
        // Only need to check for grabs after updates
        CheckAndGrabObject();
    }
}
