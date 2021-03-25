using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluvoGrabbable : MonoBehaviour
{

    // NOTE: REQUIRES RIGIDBODY COMPONENT

    // Temporary solutions, fixes the grabbed object to this location
    // Find a programmatic way to get these later
    public GameObject grabAnchor;
    public GameObject bt_debug_obj;
    private BtAndDebugScript bt_debug;


    // Some variables for customization
    public bool isStatic;
    public bool freezeXRotation;
    public bool freezeYRotation;
    public bool freezeZRotation;

    // Keep track of fingers being collided, we need both to be
    // in collision for grabbing
    /// fingerCollision indices for reference:
    /// right_thumb = 0;
    /// right_index = 1;
    /// right_middle = 2;
    /// right_ring = 3;
    /// right_pinky = 4;
    private bool[] fingerCollisions = { false, false, false, false, false };
    private bool isHolding = false;

    private Vector3 prev_pos;
    private Vector3 curr_pos;
    private Vector3 curr_vel;


    private void Start()
    {
        prev_pos = Vector3.zero;
        curr_pos = Vector3.zero;
        curr_vel = Vector3.zero;


        bt_debug = bt_debug_obj.GetComponent<BtAndDebugScript>();
    }


    void Update()
    {
        curr_pos = transform.position;
        curr_vel = (curr_pos - prev_pos) / Time.deltaTime;
        CheckAndGrabObject();
        prev_pos = curr_pos;
    }


    // See if the object should be grabbed or dropped,
    // rn only check for collision, can implement more functionality
    void CheckAndGrabObject()
    {
        if (fingerCollisions[0] & (fingerCollisions[1] || fingerCollisions[2] || fingerCollisions[3] || fingerCollisions[4]))       
        {
            transform.parent = grabAnchor.transform;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isHolding = true;
            bt_debug.DisplaySingleLine(curr_vel.ToString());
        } 
        else if (isHolding)
        {
            isHolding = false;
            transform.parent = null;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            if (!isStatic)
            {
                rb.useGravity = true;
                rb.AddForce(curr_vel, ForceMode.Impulse);
            }
         
            // rb.angularVelocity = grabAnchor.GetComponent<Rigidbody>().angularVelocity;
        }
        // Need to reset velocity or else whacky behavior
        // GetComponent<Rigidbody>().velocity = Vector3.zero;
        // GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
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
