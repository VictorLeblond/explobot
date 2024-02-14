using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour
{
    public Item item;
    public Image sprite;

    public void OnValidate()
    {
        sprite = GetComponent<Image>();
    }

    public ItemGUI(Item item)
    {
        this.item = item;
    }

}
