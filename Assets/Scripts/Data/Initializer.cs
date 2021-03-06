﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject world;
    public GameObject player;
    public GameObject refrenceManager;
    public GameObject mainCamera;
    public GameObject UI;

    public void Start()
    {
        Instantiate(refrenceManager);

        Player playerScript = Instantiate(player).GetComponent<Player>();
        playerScript.initializeAsNewWorld();

        World worldScript = Instantiate(world).GetComponent<World>();
        worldScript.initializeAsNewWorld();

        Instantiate(mainCamera);
        Instantiate(UI);
    }
}
