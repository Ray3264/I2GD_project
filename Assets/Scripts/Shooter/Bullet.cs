using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().updateHealth(-100);
            // print("hit " + other.gameObject.name + " !");
            Destroy(gameObject);
        }
        // Destroy(gameObject);
    }
}
