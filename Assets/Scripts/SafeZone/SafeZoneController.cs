using UnityEngine;

public class SafeZoneController : MonoBehaviour
{
    public float anxietyDecreaseRate = 5f;
    public float anxietyIncreaseRate = 10f;

    private int playerInZoneCount = 0;

    void Update()
    {
        if (PlayerAnxiety.Instance == null) return;

        if (playerInZoneCount > 0)
        {
            PlayerAnxiety.Instance.ReduceAnxiety(anxietyDecreaseRate * Time.deltaTime);
        }
        else
        {
            PlayerAnxiety.Instance.IncreaseAnxiety(anxietyIncreaseRate * Time.deltaTime);
        }
    }

    public void NotifyPlayerEntered()
    {
        playerInZoneCount++;
    }

    public void NotifyPlayerExited()
    {
        playerInZoneCount = Mathf.Max(0, playerInZoneCount - 1);
    }
}
