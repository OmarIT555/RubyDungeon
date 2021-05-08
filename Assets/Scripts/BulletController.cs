using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float b;
    public Vector3 tossVel;
    Rigidbody r;
    float time = 0;
    // Start is called before the first frame update
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
            Debug.Log("Bullet destroyed by time");
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            Debug.Log("Bullet destroyed");
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("Bullet destroyed");
        }

    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "arrow")
        {
            //myAudio.PlayOneShot(hit);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
