    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private GameObject deathEffectPrefab;

    public void updateHealth(int delta)
    {
        _health += delta;
        Debug.Log("Enemy received damage for: " + (-delta));
        if (_health < 1)
        {
            Debug.Log("Enemy died!");
            Die();
            //Destroy(gameObject);
        }
    }
    public void Die()
    {
        Vector3 position = transform.position;
        position.y = transform.position.y + 1.35f;
        Instantiate(deathEffectPrefab, position, Quaternion.Euler(-90f, 0f, 0f), null);
        Destroy(gameObject);
    }
}
