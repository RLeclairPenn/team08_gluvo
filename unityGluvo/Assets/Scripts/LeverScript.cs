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
    public bool isThrust;
    public int sceneToChangeTo;
    public float speedScale;

    // Start is called before the first frame update
    void Start()
    {
        bt_debug = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BtAndDebugScript>();
        boat = GameObject.FindGameObjectWithTag("Boat");
        rb = GetComponent<Rigidbody>();
        relativePos = transform.localPosition;
    }

    // private void FixedUpdate() {
    //     Vector3 newPos = new Vector3();
    //     // var parentPos = boat.GetComponent<Rigidbody>().position;
    //     newPos = boat.transform.TransformPoint(relativePos);
    //     rb.position = newPos;
    // }

    // Update is called once per frame
    void Update()
    {
        if (changeScene) {
            if (Mathf.Abs(transform.rotation.x) >= 0.70f)
            {
                SceneManager.LoadScene(sceneToChangeTo);
            }
        }
        if (isThrust && boat != null) {
            boat.GetComponent<Rigidbody>().velocity = (Vector3.forward * transform.rotation.x) * speedScale;
        }
        
    }
}
