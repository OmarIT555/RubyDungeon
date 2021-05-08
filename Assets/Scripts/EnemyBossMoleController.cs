using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossMoleController : MonoBehaviour
{
    public Animator myAnim;
    float a;
    float b;
    bool change;
    float time = 0;
    float time2 = 0;
    public float health;
    bool DamageRed = false;
    float time3 = 0;
    public GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        a = 0;
        b = -5;
        Debug.Log("Enemy a & b assigned");
        change = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(change == false)
        {
            transform.position = transform.position + (Vector3.forward * 2 * b * Time.deltaTime);
        }

        if (change == true)
        {
            transform.position = transform.position + (Vector3.left * 2 * b * Time.deltaTime);
        }

        //  -=  Time Stuff  =-  \\
        if (change == false)
        {
            time = time + 1 * Time.deltaTime;
            if (time >= 3)
            {
                change = true;
                time2 = 0;
            }
        }

        if(change == true)
        {
            time2 = time2 + 1 * Time.deltaTime;
            if (time2 >= 3)
            {
                change = false;
                time = 0;
            }
        }




        if (change == false)
        {
           if (a < 0)
           {
           transform.localScale = new Vector3(-3, 3, 3);
           }

           else if (a > 0)
           {
               transform.localScale = new Vector3(3, 3, 3);
           }
        }

        if (change == true)
        {

            if (b < 0)
            {
               transform.localScale = new Vector3(3, 3, 3);
            }
            else if (b > 0)
            {
                transform.localScale = new Vector3(-3, 3, 3);
            }
        }

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
            time3 = time3 + 1 * Time.deltaTime;
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

            if (time3 >= .3)
            {
                DamageRed = false;
                gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                time3 = 0;
            }
        }

        if (gameObject.transform.position.x < -23.8f ||
            gameObject.transform.position.x > -4.6f ||
            gameObject.transform.position.y < -55.35f ||
            gameObject.transform.position.z < -104.9f ||
            gameObject.transform.position.z > -86.09f)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(-14, 59, -95);
        }

    }

    public void subtractHealth(float damage)
    {
        health = health - damage;
        // When the enemy gets hit, the enemy turns red for a brief moment and goes back to normal
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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Wall" && change == false)
        {
            b = b * -1;
            Debug.Log("Enemy Mole hit wall " + b);
            myAnim.SetFloat("XAXIS", b);
        }

        if (collision.gameObject.tag == "Wall" && change == true)
        {
            b = b * -1;
            Debug.Log("Enemy Mole hit wall " + b);
            myAnim.SetBool("YAXIS", true);
        }

    }

}
