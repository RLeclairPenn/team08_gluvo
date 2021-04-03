using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public bool keepObject;
    // Start is called before the first frame update
    void Start()
    {
        if (keepObject)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
