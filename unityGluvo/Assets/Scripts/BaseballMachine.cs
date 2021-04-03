using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballMachine : MonoBehaviour
{
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnBalls());
    }

    IEnumerator spawnBalls()
    {
        while(true)
        {
            GameObject new_ball = Instantiate(ball, transform.position, Quaternion.Inverse(transform.rotation));
            new_ball.GetComponent<Rigidbody>().AddForce(Vector3.back * 3f);
            yield return new WaitForSeconds(1);
        }
        
    }
}
