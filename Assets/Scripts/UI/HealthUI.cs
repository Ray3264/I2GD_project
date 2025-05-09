using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    public GameObject healthText; // UI-текст тревожности

    void Update()
    {
        int healthLevel = Mathf.RoundToInt(PlayerCharacter.getHealth());
        healthText.GetComponent<TMP_Text>().text = "Health: " + healthLevel;
    }
}