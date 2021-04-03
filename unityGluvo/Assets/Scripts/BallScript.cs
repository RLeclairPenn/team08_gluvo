using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMe", 15);
    }

    void DestroyMe()
    {
        Destroy(transform.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Vector3 directionHit = collision.gameObject.transform.position - transform.position;
        //Vector3 directionHit = collision.contacts[0].point - transform.position;
        gameObject.GetComponent<Rigidbody>().AddForce(collision.transform.forward * 10f);
        
    }
}
