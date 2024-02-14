using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public string itemName;
    public Sprite itemIcon;
    public DialogueCaller owner;

    public Inventory inv;

    public override void Interact()
    {
        owner.questState = DialogueCaller.QuestState.GotItem;
        inv.AddItems(this);
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().sprite = null;
    }
}
