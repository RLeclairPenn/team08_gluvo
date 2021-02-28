using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that on collision calls a function in it's parent
/// we need to set the 'fingerNum' variable to the appropriate finger
/// for it to work and update whether it is colliding on the parent correctly
/// for reference:
/// right_thumb = 0;
/// right_index = 1;
/// right_middle = 2;
/// right_ring = 3;
/// right_pinky = 4;
/// </summary>
public class FingerScript : MonoBehaviour
{

    public int fingerNum;

    private HandControlScript parent_script;

    // Start is called before the first frame update
    void Start()
    {
        // This disables collisions with other fingers, it is based on layer 8
        Physics.IgnoreLayerCollision(8, 8, true);
        parent_script = transform.parent.GetComponent<HandControlScript>(); 
    }

    void OnTriggerEnter(Collider collide)
    {
        parent_script.OnTriggerFingerEnter(fingerNum, gameObject.transform, collide);

    }

    void OnTriggerExit(Collider collide)
    {
        parent_script.OnTriggerFingerExit(fingerNum, gameObject.transform, collide);
    }
}
