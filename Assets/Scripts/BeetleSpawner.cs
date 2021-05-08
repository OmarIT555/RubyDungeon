using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeetleSpawner : MonoBehaviour
{
    public GameObject beetle;
    public float x1;
    public float y1;
    public float z1;

    public float x2;
    public float y2;
    public float z2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider collider)
    {
        GameObject g = Instantiate(beetle);
        g.GetComponent<NavMeshAgent>().Warp(new Vector3(x1, y1, z1)); //(34, 2, -18)
        Debug.Log("Beetle spawned" + g.transform.position);

        GameObject g2 = Instantiate(beetle);
        g2.GetComponent<NavMeshAgent>().Warp(new Vector3(x2, y2, z2)); //(58, 2, -47)
        Debug.Log("Beetle spawned");
        gameObject.GetComponent<BoxCollider>().enabled = false; 
    }
}
