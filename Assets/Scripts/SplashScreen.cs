using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    void Update()
    {
        if (Time.time > 1)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
