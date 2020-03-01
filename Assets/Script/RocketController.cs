using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class RocketController : NetworkBehaviour
{
    public float batasAtas;
    public float batasBawah;
    public float speedRocket;
    public string Axis;

    private void Awake()
    {
        if (transform.position.x > 0) transform.GetComponent<SpriteRenderer>().color = Color.white;
        else transform.GetComponent<SpriteRenderer>().color = Color.green;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (!isLocalPlayer) 
            return;
        float rocketMove = GetInputPC();

        float nextposY = transform.position.y + rocketMove;
        if (nextposY > batasAtas)
        {
            rocketMove = 0;
        }
        if (nextposY < batasBawah)
        {
            rocketMove = 0;
        }
        transform.Translate(0, rocketMove, 0);
        
    }
    float GetInputPC()
    {
        return Input.GetAxis(Axis) * speedRocket * Time.deltaTime;

    }
}
