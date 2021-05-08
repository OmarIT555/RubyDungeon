using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossBeetleController : MonoBehaviour
{
    Transform playerTran;
    CharacterController myCC;
    NavMeshAgent myAgent;
    public float health;
    public float speed;
    bool DamageRed = false;
    float time = 0;
    public GameObject heart;
    Vector3 v;

    void Start()
    {
        playerTran = GameObject.Find("Player").transform;
        myCC = GetComponent<CharacterController>();
        //myAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        v = playerTran.position - transform.position;
        v.y = 0;
        transform.forward = v;
        myCC.Move(transform.forward * Time.deltaTime * speed);

        //myAgent.SetDestination(playerTran.position);

        if (health <= 0)
        {
            GameObject g = Instantiate(heart);
            g.transform.parent = transform;
            g.transform.localPosition = new Vector3(0, 0, 0);
            g.transform.parent = null;
            Destroy(gameObject);
        }

        if (DamageRed == true)
        {
            time = time + 1 * Time.deltaTime;
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

            if (time >= .3)
            {
                DamageRed = false;
                gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                time = 0;
            }
        }

        

    }

    public void subtractHealth(float damage)
    {
        health = health - damage;
        DamageRed = true;
    }
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "arrow")
        {
            //myAudio.PlayOneShot(hit);
            Destroy(collision.gameObject);
            subtractHealth(5);
        }
    }
}
