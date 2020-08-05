using System.Collections;
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
        LoadedData loadedData = new Load().deserialize();



        Instantiate(refrenceManager);

        World worldScript = Instantiate(world).GetComponent<World>();
        worldScript.chunks = loadedData.chunks;
        worldScript.seed = loadedData.seed;

        Player playerScript = Instantiate(player).GetComponent<Player>();
        //playerScript.transform.position = loadedData.transform.position;

        Instantiate(mainCamera);
        Instantiate(UI);
    }
}
