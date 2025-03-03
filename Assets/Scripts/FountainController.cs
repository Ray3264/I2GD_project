using UnityEngine;

public class FountainController : MonoBehaviour
{
    public ParticleSystem fountainParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fountainParticles.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fountainParticles.Stop();
        }
    }
}
