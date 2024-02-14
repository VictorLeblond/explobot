using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plug : Interactable
{
    public GameObject endScreen;


    public override void Interact(){

        player.finished = true;
        endScreen.SetActive(true);
        player.PlaySound(player.sm.winSound, .9f);
        endScreen.transform.Find("TotalTime").GetComponent<Text>().text = Mathf.FloorToInt(player.totalTime / 60) + " : " + Mathf.FloorToInt(player.totalTime % 60);

    }
}