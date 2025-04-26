using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    public void updateHealth(int delta)
    {
        _health += delta;
        Debug.Log("Enemy received damage for: " + (-delta));
        if (_health < 1)
        {
            Debug.Log("Enemy died!");
            Destroy(gameObject);
        }
    }
}
