using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public string itemType;

    public PlayerManager playerManager;
    public abstract void Use();
}
