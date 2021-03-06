using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBossCannonController : MonoBehaviour
{
    public GameObject bullet;
    Transform playerTran;
    float timer;
    public float spawnTime;
    bool DamageRed = false;
    float time = 0;
    Vector3 v;
    public float health = 20;
    public GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTime;
        playerTran = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Spawn();
            timer = spawnTime;
        }

        v = playerTran.position - transform.position;
        v.y = 0;
        transform.forward = v;
        if (health <= 0)
        {
            //gameObject.GetComponent<PlayerController>().healthDrop();
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
        // When the enemy gets hit, the enemy turns red for a brief moment and goes back to normal
        float h = UnityEngine.Random.Range(1, 6);
        if(h == 1)
        {
            gameObject.transform.position = new Vector3(15.96f, 60.592f, -153.39f);
        }
        else if (h == 2)
        {
            gameObject.transform.position = new Vector3(15.96f, 60.592f, -172.04f);
        }
        else if (h == 3)
        {
            gameObject.transform.position = new Vector3(43.62f, 60.592f, -172.04f);
        }
        else if (h == 4)
        {
            gameObject.transform.position = new Vector3(43.62f, 60.592f, -153.49f);
        }
        else
        {
            gameObject.transform.position = new Vector3(30, 60.592f, -162.47f);
        }
        
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

    void Spawn()
    {
        GameObject g = Instantiate(bullet);
        g.transform.parent = transform;
        g.transform.localPosition = new Vector3(0, 0, .6f);
        g.transform.forward = transform.forward;
        g.transform.parent = null;
        Debug.Log("bullet rotation" + v.y);
    }


}
