using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Backpack : MonoBehaviour
{
    Dictionary<ItemType, int> inventory;

    private void Start()
    {
        inventory = new Dictionary<ItemType, int>();
    }

    public void Insert(ItemType item)
    {
        if (!inventory.ContainsKey(item))
        {
            inventory.Add(item, 1);
        } else
        {
            inventory[item]++;
        }

        Debug.LogFormat("Picked up {0}", item.ToString());
        PrintContents();
    }

    public void Remove(ItemType item)
    {
        if (inventory.ContainsKey(item))
        {
            if (inventory[item] > 0)
            {
                inventory[item]--;
            }
        } else
        {
            // User feedback maybe?
        }   
    }

    public bool Check(ItemType item)
    {
        if (inventory.ContainsKey(item))
        {
            if (inventory[item] > 0)
            {
                return true;
            }
        }

        return false;
    }

    public void PrintContents()
    {
        ItemType[] keys = inventory.Keys.ToArray();
        int[] values = inventory.Values.ToArray();

        string outputString = "Inventory: ";

        for (int i = 0; i < keys.Length && i < values.Length; i++)
        {
            outputString += keys[i] + ": " + values[i] + (i < keys.Length - 1 ? ", " : ".");
        }

        Debug.Log(outputString);
    }
}
