using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverScript : MonoBehaviour
{
    public string sceneName;
    public GameObject bt_debug_object;
    private BtAndDebugScript bt_debug;

    public bool changeScene;
    public int sceneToChangeTo;

    // Start is called before the first frame update
    void Start()
    {
        bt_debug = bt_debug_object.GetComponent<BtAndDebugScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.rotation.x) >= 0.70f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
