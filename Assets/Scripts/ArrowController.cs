using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    float b;
    public Vector3 tossVel;
    Rigidbody r;
    float time = 0;

    void Start()
    {
        b = 2;
        r = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1 * Time.deltaTime;
        r.constraints = RigidbodyConstraints.FreezeRotation;
        r.velocity = tossVel.z * transform.forward + tossVel.y * Vector3.up;

        if (time >= 3)
        {
            Destroy(gameObject);
            Debug.Log("Arrow destroyed by time");
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            Debug.Log("Arrow destroyed");
        }

        

    }

}
