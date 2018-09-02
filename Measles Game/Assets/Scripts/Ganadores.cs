using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganadores : MonoBehaviour {

    public GameObject[] Coronas;

	// Use this for initialization
	void Start () {

        bool ganador;
        ganador = false;
        for (int i=0; i<4; i++)
        {
            PlayerController player = GameController.instance.players[i];
            if (!player.isSick)
            {
                ganador = true;
                Coronas[i].SetActive(true);
            }
            else
            {
                Coronas[i].SetActive(false);
            }
        }

        if (ganador == false)
        {
            Coronas[GameController.instance.ganador].SetActive(true);
        }
  
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
