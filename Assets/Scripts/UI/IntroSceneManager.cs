using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Добавляем пространство имен для TextMeshPro
using UnityEngine.UI;
using System.Collections;

public class IntroSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI storyText; // Заменяем Text на TextMeshProUGUI
    public Button continueButton;
    public float textDisplaySpeed = 0.05f;

    [Header("Story Content")]
    [TextArea(3, 10)] public string[] storyPages;
    
    private int currentPage = 0;
    private bool textRevealing = false;
    private Coroutine revealCoroutine;

    void Start()
    {
        continueButton.onClick.AddListener(OnContinueClick);
        ShowPage(0);
    }

    void ShowPage(int pageIndex)
    {
        if (pageIndex >= storyPages.Length)
        {
            LoadNextScene();
            return;
        }

        currentPage = pageIndex;
        continueButton.interactable = false;
        
        if (revealCoroutine != null)
            StopCoroutine(revealCoroutine);
            
        revealCoroutine = StartCoroutine(RevealText(storyPages[pageIndex]));
    }

    IEnumerator RevealText(string fullText)
    {
        textRevealing = true;
        storyText.text = "";
        
        foreach (char c in fullText)
        {
            storyText.text += c;
            yield return new WaitForSeconds(textDisplaySpeed);
        }
        
        textRevealing = false;
        continueButton.interactable = true;
    }

    void OnContinueClick()
    {
        if (textRevealing)
        {
            StopCoroutine(revealCoroutine);
            storyText.text = storyPages[currentPage];
            textRevealing = false;
            continueButton.interactable = true;
        }
        else
        {
            ShowPage(currentPage + 1);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}