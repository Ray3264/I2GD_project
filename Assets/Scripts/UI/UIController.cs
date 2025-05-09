using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private int score;
    private bool isPaused;
    [SerializeField] bool isMainMenu;
    
    [SerializeField] MouseLookY playerLookY; 
    [SerializeField] MouseLookX playerLookX; 
    [SerializeField] Weapon playerShooting; 
    
    [SerializeField] QTESystem qteSystem;
    public AudioClip clickSound;

    public void PlaySound()
    {
        SoundFXManager.instance.PlaySoundFXClip(clickSound, transform);
    }
    
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    
    public void OnOpenSettings()
    {
        pauseMenu.SetActive(true);
        PauseGame();
    }
    public void OnCloseSettings()
    {
        pauseMenu.SetActive(false);
        ResumeGame();
    }
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // Остановка времени
        Cursor.lockState = CursorLockMode.None; // Разблокировка курсора
        Cursor.visible = true; // Отображение курсора

        if (!isMainMenu)
        {
            playerLookX.enabled = false;
            playerLookY.enabled = false;
            playerShooting.enabled = false;
            qteSystem.enabled = false;
            qteSystem.qteUI.SetActive(false);
        }
    }
    
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Возобновление времени
        
        if (!isMainMenu)
        {
            Cursor.lockState = CursorLockMode.Locked; // Блокировка курсора
            Cursor.visible = false; // Скрытие курсора
            playerLookX.enabled = true;
            playerLookY.enabled = true;
            playerShooting.enabled = true;
            qteSystem.enabled = true;
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                OnCloseSettings();
                //ResumeGame();                
            }
            else
            {
                OnOpenSettings();
                //PauseGame();
            }
        }
    }

}
