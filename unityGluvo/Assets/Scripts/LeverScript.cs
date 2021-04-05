using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverScript : MonoBehaviour
{
    public string sceneName;

    private BtAndDebugScript bt_debug;
    private GameObject boat;
    private Rigidbody rb;
    private Vector3 relativePos; 

    public bool changeScene;
    public int sceneToChangeTo;

    public bool isThrust;
    public float speedScale;
    public Transform thrustAnchor;

    public bool isWheel;
    public float rotationScale;
    public Transform wheelAnchor;
    private float initialRotation;

    private HingeJoint hingeJoint;

    // Start is called before the first frame update
    void Start()
    {
        bt_debug = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BtAndDebugScript>();
        boat = GameObject.FindGameObjectWithTag("Boat");
        rb = GetComponent<Rigidbody>();
        relativePos = transform.localPosition;
        hingeJoint = GetComponent<HingeJoint>();
        initialRotation = transform.rotation.eulerAngles.x;
    }

    private void FixedUpdate() {
        //bt_debug.DisplaySingleLine($"{transform.position}");
        // Vector3 newPos = new Vector3();
        // var parentPos = boat.GetComponent<Rigidbody>().position;
        // newPos = boat.transform.TransformPoint(relativePos);
        // rb.position = newPos;
        if (isThrust && boat != null)
        {
            boat.transform.Translate(Vector3.forward * transform.rotation.x * speedScale);
            //boat.transform.Translate(boat.transform.forward * 0.05f);
            //boat.GetComponent<Rigidbody>().velocity = (boat.transform.forward * transform.rotation.x) * speedScale;
            //GetComponent<HingeJoint>().connectedAnchor = thrustAnchor.position;
            hingeJoint.connectedAnchor = Vector3.MoveTowards(hingeJoint.connectedAnchor, thrustAnchor.position, (float)0.8);
            //bt_debug.DisplaySingleLine($"Lever: {transform.position}, Anchor {thrustAnchor.position}");

        }
        if (isWheel && boat != null)
        {
            boat.transform.RotateAround(boat.transform.position, boat.transform.up, (transform.rotation.eulerAngles.x - initialRotation) * rotationScale);
            hingeJoint.connectedAnchor = Vector3.MoveTowards(hingeJoint.connectedAnchor, wheelAnchor.position, (float)0.8);
            bt_debug.DisplaySingleLine($"{transform.rotation.eulerAngles}");
            //bt_debug.DisplaySingleLine($"{hingeJoint.connectedAnchor}, {transform.position}, {wheelAnchor.position}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (changeScene) {
            if (Mathf.Abs(transform.rotation.x) >= 0.70f)
            {
                SceneManager.LoadScene(sceneToChangeTo);
            }
        }
        //if (isThrust && boat != null) {
          //  
        //}
        
    }
}
