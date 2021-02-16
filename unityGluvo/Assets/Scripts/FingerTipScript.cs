using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using System.Runtime.InteropServices;

public class FingerTipScript : MonoBehaviour
{
    public GameObject bt_object;
    public GameObject right_hand_obj;
    public GameObject index_sphere;

    private BtAndDebugScript bt_debug;

    private OVRHand right_hand_ovr_hand;
    private OVRSkeleton right_hand_skeleton_info;
    private IList<OVRBone> right_bones;
    private int index_finger;
    
    // Start is called before the first frame update
    void Start()
    {
        // getting bluetooth and debug script from object
        bt_debug = (BtAndDebugScript) bt_object.GetComponent<BtAndDebugScript>();

        // get right hand OVRHand script
        right_hand_ovr_hand = (OVRHand) right_hand_obj.GetComponent<OVRHand>();

        // get right hand skeleton script, then get the index finger's tip id
        right_hand_skeleton_info = (OVRSkeleton) right_hand_obj.GetComponent<OVRSkeleton>();
        right_bones = right_hand_skeleton_info.Bones;
        index_finger = (int) OVRSkeleton.BoneId.Hand_IndexTip;

    }

    private Transform GetFingerTransform(int bone_id)
    {
        return right_bones[bone_id].Transform;
    }

    // Update is called once per frame
    void Update()
    {
        Transform right_index_tip = GetFingerTransform(index_finger);

        Transform index_transform = index_sphere.GetComponent<Transform>();
        index_transform.position = Vector3.MoveTowards(index_transform.position, right_index_tip.position, (float) .03);
        //bt_debug.DisplaySingleLine(right_index_tip.position.ToString());
        //bt_debug.DisplaySingleLine(right_bones.Count.ToString());
        
        //int indexTip = skeletonInfo.GetCurrentStartBoneId() + skeletonInfo.GetCurrentNumSkinnableBones() + 1;
        //bt_debug.DisplaySingleLine(indexTip.ToString());
    }

    

    
    void OnCollisionEnter(Collision collision)
    {
        string msg = collision.contacts[0].otherCollider.transform.gameObject.name + " has collided";
        bt_debug.AppendToMessage(msg);
        bt_debug.sendMessage("_i_");
    }

    void OnCollisionExit(Collision collision)
    {
        bt_debug.ResetMsg();
        string msg = collision.contacts[0].otherCollider.transform.gameObject.name + " has exited collision";
        bt_debug.AppendToMessage(msg);
    }
    
}
