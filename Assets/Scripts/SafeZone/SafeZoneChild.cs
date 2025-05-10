using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SafeZoneChild : MonoBehaviour
{
    private SafeZoneController controller;

    private void Start()
    {
        controller = GetComponentInParent<SafeZoneController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.NotifyPlayerEntered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.NotifyPlayerExited();
        }
    }
}
