using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string characterName;
    public int health;

    public abstract void Movement();

}
