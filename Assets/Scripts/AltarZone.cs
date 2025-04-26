using UnityEngine;

public class AltarZone : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAnxiety.Instance.IncreaseAnxiety(5 * Time.deltaTime);
        }
    }

    
}