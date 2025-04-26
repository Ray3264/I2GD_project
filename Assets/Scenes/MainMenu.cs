using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    // public void GotoSettingsMenu()
    // {
    //     SceneManager.LoadScene("Scenes/OptionsMenu");
    // }
}
