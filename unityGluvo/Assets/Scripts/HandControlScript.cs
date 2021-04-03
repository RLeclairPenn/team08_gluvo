using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using System.Runtime.InteropServices;


/// <summary>
/// This script handles:
///     - Receives when a specific finger is colliding and when it stops colliding
///     - Sends a message through the bluetooth manager to collide a hand
///     - TODO: Detect grabbing... @Efe
///     - TODO: With grabbing detection, it attaches the object it grabbed to the first finger that said object collided with
/// </summary>
/// 

public class HandControlScript : MonoBehaviour
{
    // Defines the index in which each finger is found on the rightArray
    const int right_thumb = 0;
    const int right_index = 1;
    const int right_middle = 2;
    const int right_ring = 3;
    const int right_pinky = 4;

    // Contains information on collisions on the right hand
    private int[] rightArray = { 0, 0, 0, 0, 0 };

    // We define the GameObjects we need here, they are assigned manually in the editor...
    // Note that if we change the name of those preset objects, we will have to reassign them
    // I note that because it is a very common bug
    //private GameObject bt_object; // This is the object we use to send bluetooth messages and show things on the debug screen
    public GameObject right_hand_obj; // This is the right hand object as defined by Unity
    // The following are the collision spheres that go to each finger individually
    public GameObject right_thumb_sphere;
    public GameObject right_index_sphere;
    public GameObject right_middle_sphere;
    public GameObject right_ring_sphere;
    public GameObject right_pinky_sphere;

    // This is the script we obtain from the bluetooth game object
    private BtAndDebugScript bt_debug;

    // These are information we need to obtain transforms from the Oculus game object
    private OVRSkeleton right_hand_skeleton_info;
    private IList<OVRBone> right_bones; // a list of all the bones on the right hand

    // These are the ids we obtain from the skeleton to access the transforms of  
    // the finger tips 
    private int right_thumb_finger_id;
    private int right_index_finger_id;
    private int right_middle_finger_id;
    private int right_ring_finger_id;
    private int right_pinky_finger_id;

    // Start is called before the first frame update
    void Start()
    {
        // getting bluetooth and debug script from object
        
        bt_debug = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BtAndDebugScript>();

        // get right hand skeleton script, then get the list of bones
        right_hand_skeleton_info = (OVRSkeleton) right_hand_obj.GetComponent<OVRSkeleton>();
        right_bones = right_hand_skeleton_info.Bones;

        right_thumb_finger_id = (int)OVRSkeleton.BoneId.Hand_ThumbTip;
        right_index_finger_id = (int) OVRSkeleton.BoneId.Hand_IndexTip;
        right_middle_finger_id = (int) OVRSkeleton.BoneId.Hand_MiddleTip;
        right_ring_finger_id = (int) OVRSkeleton.BoneId.Hand_RingTip;
        right_pinky_finger_id = (int) OVRSkeleton.BoneId.Hand_PinkyTip;
    }

    // This function returns the transform of a specific bone based on the bone_id
    private Transform GetFingerTransform(int bone_id)
    {
        return right_bones[bone_id].Transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Here we are getting all of the transforms at the tips of the fingers
        Transform right_thumb_tip = GetFingerTransform(right_thumb_finger_id);
        Transform right_index_tip = GetFingerTransform(right_index_finger_id);
        Transform right_middle_tip = GetFingerTransform(right_middle_finger_id);
        Transform right_ring_tip = GetFingerTransform(right_ring_finger_id);
        Transform right_pinky_tip = GetFingerTransform(right_pinky_finger_id);

        // Here we get the transforms of the spheres we have that are colliding at each of the tips
        Transform right_thumb_transform = right_thumb_sphere.GetComponent<Transform>();
        Transform right_index_transform = right_index_sphere.GetComponent<Transform>();
        Transform right_middle_transform = right_middle_sphere.GetComponent<Transform>();
        Transform right_ring_transform = right_ring_sphere.GetComponent<Transform>();
        Transform right_little_transform = right_pinky_sphere.GetComponent<Transform>();

        // What we are doing in this block is make the spheres follow the tips of the fingers at a very fast rate
        right_index_transform.position = Vector3.MoveTowards(right_index_transform.position, right_index_tip.position, (float) .03);
        right_middle_transform.position = Vector3.MoveTowards(right_middle_transform.position, right_middle_tip.position, (float).03);
        right_ring_transform.position = Vector3.MoveTowards(right_ring_transform.position, right_ring_tip.position, (float).03);
        right_little_transform.position = Vector3.MoveTowards(right_little_transform.position, right_pinky_tip.position, (float).03);
        right_thumb_transform.position = Vector3.MoveTowards(right_thumb_transform.position, right_thumb_tip.position, (float).03);
    }

    // Can only be called from the colliders at the finger tips, it is called when said collider collides with something
    // it gives us information about: the finger (following the numbers we set up at the start!), the transform of the collider 'fingerPoint' 
    // and the thing it collided with 'collideWith'
    public void OnTriggerFingerEnter(int finger, Transform fingerPoint, Collider collideWith)
    {
        // Color triggerColor = Color.red;
        rightArray[finger] = 1;
        bt_debug.DisplayRightHandStatus(rightArray);

        // collideWith.GetComponent<Renderer> ().material.color = triggerColor;
        // collideWith.attachedRigidbody.useGravity = true;
    }

    // Same as above, only difference is this is called when one of those colliders exits a collision
    public void OnTriggerFingerExit(int finger, Transform fingerPoint, Collider collideWith)
    {
        rightArray[finger] = 0;
        bt_debug.DisplayRightHandStatus(rightArray);
    }


}
