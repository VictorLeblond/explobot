using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Collider2D col;
    public BetterDialogueManager bdm;

    public Player player;

    public void Update()
    {
        if (Input.GetKeyDown(player.interact) && col != null && !bdm.inEvent && !player.inMenu)
        {
            Interact();
        }
    }

    public virtual void Interact()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            col = collision;
            player.ToggleExlamation(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            col = null;
            player.ToggleExlamation(false);
        }
    }

}
