using UnityEngine;

public class WhisperEffect : MonoBehaviour
{
    public AudioClip[] whispers;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlayWhisper", 10f, Random.Range(5f, 15f)); // Воспроизводить случайный шёпот
    }

    void PlayWhisper()
    {
        if (PlayerAnxiety.Instance.anxietyLevel > 50) // Запуск при высоком уровне тревоги
        {
            audioSource.clip = whispers[Random.Range(0, whispers.Length)];
            audioSource.Play();
        }
    }
}