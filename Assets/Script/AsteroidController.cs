using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class AsteroidController : NetworkBehaviour
{
    public float forceAsteroid;
    Rigidbody2D rigid;

    [SyncVar(hook = "ChangeScore1")]
    public int score1;
    [SyncVar(hook = "ChangeScore2")]
    public int score2;

    Text scoreUIP1;
    Text scoreUIP2;

    GameObject panelGameOver;
    Text txPlayerWin;

    AudioSource audio;
    public AudioClip asteroidHit;

   
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(-2, 0).normalized;
        rigid.AddForce(direction  * forceAsteroid);

        score1 = 0;
        score2 = 0;

        scoreUIP1 = GameObject.Find("score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("score2").GetComponent<Text>();

        panelGameOver = GameObject.Find("PanelGameOver");
        audio = GetComponent<AudioSource>();
    }


    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audio.PlayOneShot(asteroidHit);
        if (collision.gameObject.name.Contains("rocket"))
        {
            float sisi = (transform.position.y - collision.transform.position.y) * 5f;
            Vector2 direction = new Vector2(rigid.velocity.x, sisi).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(direction * forceAsteroid * 2);
        }
        else if (collision.gameObject.name == "sisiKiri")
        {
            score2 += 1;
            ShowScore();
            if (score2 == 5)
            {
                RpcPanelGameOver("White");
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(-2, 0).normalized;
            rigid.AddForce(arah * forceAsteroid);
        }
        else if (collision.gameObject.name == "sisiKanan")
        {
            score1 += 1;
            ShowScore();
            if (score1 == 5)
            {
                RpcPanelGameOver("Green");
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * forceAsteroid);
        }
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            ClientDisconnect();
        }
    }
    [ClientRpc]
    void RpcPanelGameOver(string warna)
    {
        panelGameOver.transform.localPosition = Vector3.zero;
        txPlayerWin = GameObject.Find("PlayerWin").GetComponent<Text>();
        txPlayerWin.text = "Rocket " + warna + " Win!";
        gameObject.SetActive(false);
    }
    void ShowScore()
    {
        Debug.Log("Score Player1 : " + score1 + "Score Player2 : " + score2);
        scoreUIP1.text = score1 + "";
        scoreUIP2.text = score2 + "";
    }
    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
    }
    void ChangeScore1(int score)
    {
        if (scoreUIP1 != null)
            scoreUIP1.GetComponent<Text>().text = "" + score;
    }
    void ChangeScore2(int score)
    {
        if (scoreUIP2 != null)
            scoreUIP2.GetComponent<Text>().text = "" + score;
    }
    void ClientDisconnect()
    {
        GameObject.Find("Main Camera").SendMessage("backGamePlay");
    }
}
