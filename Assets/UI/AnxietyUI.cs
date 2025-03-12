using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyUI : MonoBehaviour
{
    public GameObject anxietyText; // UI-текст тревожности

    void Update()
    {
        int anxietyLevel = Mathf.RoundToInt(PlayerAnxiety.Instance.anxietyLevel);
        anxietyText.GetComponent<TMP_Text>().text = "Anxiety: " + anxietyLevel;
    }
}