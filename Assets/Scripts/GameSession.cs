using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;


public class GameSession : MonoBehaviour
{

    public TMP_InputField seedInput;

    public void Start()
    {
        // get the text input field
        seedInput = GameObject.FindGameObjectWithTag("SeedTextField").GetComponent<TMP_InputField>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void loadLoadingScreen()
    {
        // try and convert the text field text to an int
        // if that fails then generate our own random number
        // and apply that
        try
        {
            // the world seed file in player prefs to the seed - try to
            PlayerPrefs.SetInt("worldSeed", System.Convert.ToInt32(seedInput.text));
        }
        catch (Exception e)
        {
            System.Random prng = new System.Random(DateTime.Now.Millisecond);
            PlayerPrefs.SetInt("worldSeed", prng.Next(-100000, 100000));
            print(e);
        }

        SceneManager.LoadScene("Loading");
        Invoke("loadWorld", 3f);
    }

    public void loadWorld()
    {
        SceneManager.LoadScene("Main");
    }
}
