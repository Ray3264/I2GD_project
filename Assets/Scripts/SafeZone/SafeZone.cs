using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private bool playerInside = false;
    public float anxietyDecreaseRate = 5f;
    public float anxietyIncreaseRate = 10f;

    void Update()
    {
        if (PlayerAnxiety.Instance == null) return;

        if (playerInside)
        {
            PlayerAnxiety.Instance.ReduceAnxiety(anxietyDecreaseRate * Time.deltaTime);
        }
        else
        {
            PlayerAnxiety.Instance.IncreaseAnxiety(anxietyIncreaseRate * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}

