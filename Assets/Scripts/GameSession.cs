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
    }

    public void loadWorld()
    {
        // try and convert the text field text to an int
        // if that fails then generate our own random number
        // and apply that
        try
        {
            LookUpData.seed = System.Convert.ToInt32(seedInput.text);
        }
        catch (Exception e)
        {
            System.Random prng = new System.Random(DateTime.Now.Millisecond);
            LookUpData.seed = prng.Next(-100000, 100000);
        }

        SceneManager.LoadScene("Main");
    }
}
