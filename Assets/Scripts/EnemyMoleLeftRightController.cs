using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoleLeftRightController : MonoBehaviour
{
    public Animator myAnim;
    float a;
    float b;
    public float health = 10;
    bool DamageRed = false;
    public GameObject heart;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        a = 0;
        b = -1;
        Debug.Log("Enemy a & b assigned");
        myAnim.SetBool("YAXIS", true);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * 2 * b * Time.deltaTime);

        if (b < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (b > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
        if (collision.gameObject.tag == "Wall")
        {
            b = b * -1;
            Debug.Log("Enemy Mole hit wall " + b);
            
        }




    }

}
