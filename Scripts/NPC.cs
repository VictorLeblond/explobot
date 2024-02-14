using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public DialogueCaller caller;

    public Sprite recoveredSprite;
    SpriteRenderer sprite;

    public void Start()
    {
        if (caller == null) caller = GetComponent<DialogueCaller>();
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
    }
    public override void Interact()
    {
        caller.startDialogue();
    }
    public void ChangeSprite()
    {
        sprite.sprite = recoveredSprite;
    }
}
