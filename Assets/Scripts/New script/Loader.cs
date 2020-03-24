using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Store prefab of game manager
    public GameObject gameManager;

    void Awake()
    {
        if (World.instance == null)
        {
            Instantiate(gameManager);
        }
    }

}
