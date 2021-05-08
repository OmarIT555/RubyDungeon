using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour

{
    public GameObject arrow;
    Vector3 v;
    public float healthNum;
    public float arrowNum;
    public Animator myAnim;
    public AudioClip hit;
    public AudioClip health;
    public AudioClip arrowP;
    public AudioClip arrowShot;
    public AudioClip punch1;
    public AudioClip punch2;
    AudioSource myAudio;
    [SerializeField] Text Health;
    float time = 0;
    float time2 = 0;
    bool playerDamageRed = false;
    bool attackMode = false;
    bool gameover = false;
    //int healthDropCounter = 0;
    //public GameObject heart;

    Rigidbody myBod;
    Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        myBod = GetComponent<Rigidbody>();
        myAudio = GetComponent<AudioSource>();

        gameOverText = GameObject.Find("GameOver").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] boss1 = GameObject.FindGameObjectsWithTag("Boss mole");
        GameObject[] boss2 = GameObject.FindGameObjectsWithTag("BossBeetle");
        GameObject[] boss3 = GameObject.FindGameObjectsWithTag("BossCannon");

        if (boss1.Length == 0 && boss2.Length == 0 && boss3.Length == 0)
        {
            Time.timeScale = 0;
            gameover = true;
            gameOverText.text = "You Win! \nPress Space To Start Over";
        }

        Health.text = "Health: " + healthNum + "\nArrows: " + arrowNum;

        float a = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Vertical");
        
        // Old Method of movement (Do not delete)
        /* transform.position = transform.position +
            (Vector3.right * 2 * a * Time.deltaTime) + (Vector3.forward * 2 * b * Time.deltaTime);*/

        // New method of movement, prevents the player from moving diagonally
        if (Input.GetKey("w"))
        {
            myBod.MovePosition(myBod.position + Vector3.forward * 3 * b * Time.deltaTime);
        }
        else if (Input.GetKey("a"))
        {
            myBod.MovePosition(myBod.position + Vector3.right * 3 * a * Time.deltaTime);
        }
        else if (Input.GetKey("s"))
        {
            myBod.MovePosition(myBod.position + Vector3.forward * 3 * b * Time.deltaTime);
        }
        else if (Input.GetKey("d"))
        {
            myBod.MovePosition(myBod.position + Vector3.right * 3 * a * Time.deltaTime);
        }

        myAnim.SetFloat("XAXIS", a);
        myAnim.SetFloat("YAXIS", b);

        // Flips the player by x-axis to make the player face the right way
        if(a < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (a > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //print("Vertical Axis: " + b);
        //print("Horizontal Axis: " + a);
        myAnim.SetBool("ATTACKDOWN", false);
        myAnim.SetBool("ATTACKUP", false);
        myAnim.SetBool("ATTACKRIGHT", false);
        myAnim.SetBool("ATTACKBOWBACK", false);
        myAnim.SetBool("ATTACKBOWFRONT", false);
        if (!PauseMenuController.paused)
        {
            if (Input.GetButtonDown("Fire1") && b < -.50 && a == 0)
            {
                myAnim.SetBool("ATTACKDOWN", true);
                float h = UnityEngine.Random.Range(1, 10);
                if (h > 2)
                {
                    myAudio.PlayOneShot(punch1);
                }
                else
                {
                    myAudio.PlayOneShot(punch2);
                }
                //make hitbox  do the attack thing
                Debug.Log("player attacked down " + b);

                attackMode = true;
            }
            else if (Input.GetButtonDown("Fire1") && b > -.50 && a == 0)
            {
                myAnim.SetBool("ATTACKUP", true);
                float h = UnityEngine.Random.Range(1, 10);
                if (h > 5)
                {
                    myAudio.PlayOneShot(punch1);
                }
                else
                {
                    myAudio.PlayOneShot(punch2);
                }
                //make hitbox  do the attack thing

                Debug.Log("player attacked up " + b);

                attackMode = true;
            }
            else if (Input.GetButtonDown("Fire1") && a > .5)
            {
                myAnim.SetBool("ATTACKRIGHT", true);
                float h = UnityEngine.Random.Range(1, 10);
                if (h > 5)
                {
                    myAudio.PlayOneShot(punch1);
                }
                else
                {
                    myAudio.PlayOneShot(punch2);
                }
                //make hitbox  do the attack thing

                Debug.Log("player attacked right " + a);

                attackMode = true;
            }
            else if (Input.GetButtonDown("Fire1") && a < -.50)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                myAnim.SetBool("ATTACKRIGHT", true);
                myAudio.PlayOneShot(punch1);
                //make hitbox  do the attack thing

                Debug.Log("player attacked left " + a);

                attackMode = true;
            }
            else if (Input.GetButtonDown("Fire1") && (b == 0 && a == 0))
            {
                myAnim.SetBool("ATTACKDOWN", true);
                float h = UnityEngine.Random.Range(1, 10);
                if (h > 5)
                {
                    myAudio.PlayOneShot(punch1);
                }
                else
                {
                    myAudio.PlayOneShot(punch2);
                }

                //make hitbox  do the attack thing
                attackMode = true;
            }


            if (arrowNum < 0)
            {
                arrowNum = 0;
            }

            //When the player right clicks to use his bow
            if (Input.GetButtonDown("Fire2") && Input.GetKey("s") && arrowNum > 0)
            {
                arrowNum--;
                transform.localScale = new Vector3(1, 1, 1);
                myAnim.SetBool("ATTACKBOWFRONT", true);
                myAudio.PlayOneShot(arrowShot);
                SpawnArrow(90, 0, 0, -1);
                Debug.Log("player attacked down " + b);
            }
            else if (Input.GetButtonDown("Fire2") && Input.GetKey("w") && arrowNum > 0)
            {
                arrowNum--;
                transform.localScale = new Vector3(1, 1, 1);
                myAnim.SetBool("ATTACKBOWBACK", true);
                myAudio.PlayOneShot(arrowShot);
                SpawnArrow(270, 0, 0, 1);
                Debug.Log("player attacked up " + b);
            }
            else if (Input.GetButtonDown("Fire2") && Input.GetKey("d") && arrowNum > 0)
            {
                arrowNum--;
                transform.localScale = new Vector3(1, 1, 1);
                myAnim.SetBool("ATTACKRIGHT", true);
                myAudio.PlayOneShot(arrowShot);
                SpawnArrow(0, 1, 0, -.25f);
                Debug.Log("player attacked right " + a);
            }
            else if (Input.GetButtonDown("Fire2") && Input.GetKey("a") && arrowNum > 0)
            {
                arrowNum--;
                transform.localScale = new Vector3(-1, 1, 1);
                myAnim.SetBool("ATTACKRIGHT", true);
                myAudio.PlayOneShot(arrowShot);
                SpawnArrow(180, 1, 0, -.25f);
                Debug.Log("player attacked left " + a);
            }
            else if (Input.GetButtonDown("Fire2") && (b == 0 && a == 0) && arrowNum > 0)
            {
                arrowNum--;
                transform.localScale = new Vector3(1, 1, 1);
                myAnim.SetBool("ATTACKBOWFRONT", true);
                myAudio.PlayOneShot(arrowShot);
                SpawnArrow(90, 0, 0, -1);
            }
        }
        



        if (attackMode == true)
        {
            time2 = time2 + 1 * Time.deltaTime;
            if(time2 >= 1)
            {
                attackMode = false;
                time2 = 0;
            }
        }

        // When the player gets hit, the player turns red for a brief moment and goes back to normal
        if (playerDamageRed == true)
        {
            time = time + 1 * Time.deltaTime;
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

            if (time >= .3)
            {
                playerDamageRed = false;
                gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                time = 0;
            }
        }

        if(Time.timeScale == 0 && gameover == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Time.timeScale = 1;
                gameover = false;
                SceneManager.LoadScene(2);
            }
            
        }

    }

    // Collisions that affect the player
    public void OnCollisionEnter(Collision collision)
    {
        // If the player touches the Enemy Mole
        if(collision.gameObject.tag == "mole")
        {
            myAudio.PlayOneShot(hit);
            damage(5);
            playerDamageRed = true;
        }

        // If the player touches the Enemy Mole
        if (collision.gameObject.tag == "mole2")
        {
            myAudio.PlayOneShot(hit);
            damage(5);
            playerDamageRed = true;
        }

        // If the player gets hit by the Boss Mole
        if (collision.gameObject.tag == "Boss mole")
        {
            myAudio.PlayOneShot(hit);
            damage(10);
            playerDamageRed = true;
        }

        // If the player gets hit by the bullet
        if (collision.gameObject.tag == "bullet")
        {
            myAudio.PlayOneShot(hit);
            damage(10);
            playerDamageRed = true;
        }

        // If the player gets hit by the bullet
        if (collision.gameObject.tag == "spikeball")
        {
            myAudio.PlayOneShot(hit);
            damage(10);
            playerDamageRed = true;
            Destroy(collision.gameObject);
        }

        // If the player gets hit by the Enemy beetle
        if (collision.gameObject.tag == "beetle")
        {
            myAudio.PlayOneShot(hit);
            damage(1);
            playerDamageRed = true;

            Rigidbody rbody = collision.collider.GetComponent<Rigidbody>();
            if(rbody != null)
            {
                rbody.isKinematic = false;
                Vector3 direction = collision.transform.position - transform.position;
                rbody.AddForce(direction.normalized * 3.5f, ForceMode.Impulse);
                StartCoroutine(KnockDelay(rbody));
            }
             
        }

        // If the player gets hit by the Enemy boss beetle
        if (collision.gameObject.tag == "BossBeetle")
        {
            myAudio.PlayOneShot(hit);
            damage(5);
            playerDamageRed = true;

            Rigidbody rbody = collision.collider.GetComponent<Rigidbody>();
            if (rbody != null)
            {
                rbody.isKinematic = false;
                Vector3 direction = collision.transform.position - transform.position;
                rbody.AddForce(direction.normalized * 20, ForceMode.Impulse);
                StartCoroutine(KnockDelay(rbody));
            }

        }

    }

    public void OnTriggerEnter(Collider collider)
    {
        // If the player touches a heart
        if (collider.gameObject.tag == "heart")
        {
            myAudio.PlayOneShot(health);
            damage(collider.gameObject.GetComponent<HealthPickUpController>().regenAmount);
            Destroy(collider.gameObject);
        }

        // If the player touches a boss heart
        if (collider.gameObject.tag == "Bheart")
        {
            myAudio.PlayOneShot(health);
            if(healthNum < 100)
            {
                healthNum = 100;
            }
            else
            {
                damage(collider.gameObject.GetComponent<HealthBossPickUpController>().regenAmount);
            }
            gameObject.GetComponent<Transform>().position = new Vector3(0, 2, -5); //Teleports player back to spawn
            Destroy(collider.gameObject);
        }

        // If the player touches arrow pickup
        if (collider.gameObject.tag == "arrowPickup")
        {
            myAudio.PlayOneShot(arrowP);
            addArrow(collider.gameObject.GetComponent<ArrowPickUp>().arrowAmount);
            Destroy(collider.gameObject);
        }

        //Enemy with tag mole
        if (attackMode == true && collider.gameObject.tag == "mole")
        {
            //Destroy(collider.gameObject);
            //collider.gameObject.GetComponent<EnemyMoleLeftRightController>().subtractHealth(5);
            collider.gameObject.GetComponent<EnemyMoleUpDownController>().subtractHealth(5);
            //Debug.Log("Health of mole H " + collider.gameObject.GetComponent<EnemyMoleLeftRightController>().health);
            //Debug.Log("Health of mole H " + collider.gameObject.GetComponent<EnemyMoleUpDownController>().health);
        }

        //Enemy with tag mole
        if (attackMode == true && collider.gameObject.tag == "mole2")
        {
            //Destroy(collider.gameObject);
            collider.gameObject.GetComponent<EnemyMoleLeftRightController>().subtractHealth(5);
            //Debug.Log("Health of mole H " + collider.gameObject.GetComponent<EnemyMoleLeftRightController>().health);
        }

        //Enemy with tag Bossmole
        if (attackMode == true && collider.gameObject.tag == "Boss mole")
        {
            //Destroy(collider.gameObject);
            collider.gameObject.GetComponent<EnemyBossMoleController>().subtractHealth(5);
            Debug.Log("Health of Boss Mole " + collider.gameObject.GetComponent<EnemyBossMoleController>().health);
        }

        //Enemy with tag canon
        if (attackMode == true && collider.gameObject.tag == "canon")
        {
            //Destroy(collider.gameObject);
            collider.gameObject.GetComponent<EnemyCannonController>().subtractHealth(5);
            Debug.Log("Health of Cannon " + collider.gameObject.GetComponent<EnemyCannonController>().health);
        }

        //Enemy with tag canon
        if (attackMode == true && collider.gameObject.tag == "BossCannon")
        {
            //Destroy(collider.gameObject);
            collider.gameObject.GetComponent<EnemyBossCannonController>().subtractHealth(5);
        }

        //Enemy with tag Beetle
        if (attackMode == true && collider.gameObject.tag == "beetle")
        {
            //Destroy(collider.gameObject);
            collider.gameObject.GetComponent<EnemyBeetle>().subtractHealth(5);
            Debug.Log("Health of beetle " + collider.gameObject.GetComponent<EnemyBeetle>().health);
        }

        //Enemy with tag Beetle
        if (attackMode == true && collider.gameObject.tag == "BossBeetle")
        {
            //Destroy(collider.gameObject);
            collider.gameObject.GetComponent<EnemyBossBeetleController>().subtractHealth(5);
            Debug.Log("Health of Boss beetle " + collider.gameObject.GetComponent<EnemyBossBeetleController>().health);
        }

        //When player walks in the teleporter within the exit of the first maze
        if (collider.gameObject.tag == "Teleport1")
        {
            gameObject.GetComponent<Transform>().position = new Vector3(-8, 58, -95);
        }

        //When player walks in the teleporter within the exit of the second maze
        if (collider.gameObject.tag == "Teleport3")
        {
            gameObject.GetComponent<Transform>().position = new Vector3(46, 58, -161);
        }

        //When player walks in the teleporter within the exit of the third maze
        if (collider.gameObject.tag == "Teleport2")
        {
            gameObject.GetComponent<Transform>().position = new Vector3(86, 58, -102);
        }

        //Currently temporary, when the player walks into the teleport back cube
        if (collider.gameObject.tag == "TPback1")
        {
            gameObject.GetComponent<Transform>().position = new Vector3(0, 2, -5);
        }

    }


    private IEnumerator KnockDelay(Rigidbody other)
    {
        if(other != null)
        {
            yield return new WaitForSeconds(1);
            other.velocity = Vector3.zero;
            other.isKinematic = true;
        }
    }


    public void damage(float d)
    {
        healthNum -= d;
        if (healthNum <= 0)
        {
            //dead
            Time.timeScale = 0;
            gameover = true;
            gameOverText.text = "Game Over! \nPress Space To Start Over";
        }

    }

    public void addArrow(float d)
    {
        arrowNum += d;
    }

    void SpawnArrow(float r, float x, float y, float z)
    {
        GameObject g = Instantiate(arrow);
        g.transform.parent = transform;
        g.transform.localPosition = new Vector3(x, y, z); 
        g.transform.Rotate(0, r, 0);
        //g.transform.forward = transform.forward;
        g.transform.parent = null;
    }

    /*public void healthDrop() {
        healthDropCounter++;
        if (healthDropCounter % 3 == 0 && healthDropCounter != 0) {
            GameObject g = Instantiate(heart);

            //change heart position to space in front of player
            g.transform.position = myBod.position;
            Debug.Log("heart spawn" + g.transform.position);
        }
    
    }*/

}
