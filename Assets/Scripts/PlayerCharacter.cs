using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCharacter : MonoBehaviour
{
    private static int health;
    void Start()
    {
        health = 5;
    }
    public void Hurt(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log($"Health: {health}");
        }
    }
    public static int getHealth() { return health; }
}
