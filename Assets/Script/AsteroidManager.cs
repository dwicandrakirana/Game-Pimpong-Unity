using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AsteroidManager : NetworkBehaviour
{
    public GameObject prefabAsteroid;
    bool asteroidSpawn = false;
    GameObject ball;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer || asteroidSpawn)
            return;
        if (NetworkServer.connections.Count == 2)
        {
            ball = (GameObject)Instantiate(prefabAsteroid);
            NetworkServer.Spawn(ball);
            asteroidSpawn = true;
        }
    }
}
