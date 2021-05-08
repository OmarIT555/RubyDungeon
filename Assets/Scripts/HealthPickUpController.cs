using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpController : MonoBehaviour
{
    public float regenAmount; // Assign negative values for player to gain health
    void Start()
    {
        Debug.Log("Heart spawned");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().damage(regenAmount);
            //Destroy(gameObject);
        }

    }*/
}
