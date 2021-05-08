using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignController : MonoBehaviour
{
    public GameObject textBox;
    public Text textWord;

    public string dialogue;
    public bool dialogueOn;

    // Start is called before the first frame update
    void Start()
    {


    }

    //Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1") && dialogueOn)
        {
            if (textBox.activeInHierarchy)
            {
                textBox.SetActive(false);
            }
            else
            {
                textBox.SetActive(true);
                textWord.text = dialogue;
            }
        }
        
    }


    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            dialogueOn = true;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            dialogueOn = false;
            textBox.SetActive(false);
        }
        
    }
    

}
