using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public PlayerController[] players;
    public int ganador;
    public int infectados = 0;


    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        int infectedPlayer = Random.Range(0, 3);
        for (int i=0;i<4;i++)
        {
            players[i].setInfected(false);
        }
        players[infectedPlayer].setInfected(true);
		players [infectedPlayer].health = 0;
		players [infectedPlayer].maxSpeed = 25;
        ganador = infectedPlayer;



        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Player:" + i + " Infectado: " + players[i].isSick);
        }
    }

    public void RestartGame () {
        int infectedPlayer = Random.Range(0, 3);
        for (int i = 0; i < 4; i++)
        {
            players[i].setInfected(false);
            players[i].health = 100f;
            players[i].maxSpeed = 20f;
        }
        players[infectedPlayer].setInfected(true);
        players[infectedPlayer].health = 0;
        players[infectedPlayer].maxSpeed = 25;
        ganador = infectedPlayer;
        Timer.time = 90f;
        infectados = 0;


    }
}
