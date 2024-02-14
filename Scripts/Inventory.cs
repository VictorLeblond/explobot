using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemGUI> items = new List<ItemGUI>();

    public void Start()
    {
        UpdateInv();
    }

    public bool HasItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                return true;
            }
        }
        return false;
    }

    public void AddItems(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].item == null)
            {
                items[i].item = item;
                break;
            }
        }
        UpdateInv();
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].item == item)
            {
                items[i].item = null;
            }
        }
        UpdateInv();
    }


    void UpdateInv()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].item != null)
            {
                items[i].sprite.color = new Color(1,1,1);
                items[i].sprite.sprite = items[i].item.itemIcon;
            }
            else
            {
                items[i].sprite.color = new Color(0,0,0,0);
            }
        }
    }
}
