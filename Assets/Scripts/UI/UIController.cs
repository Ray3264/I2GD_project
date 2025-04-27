using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //[SerializeField] TMP_Text scoreLabel;
    //[SerializeField] TMP_Text pauseText;
    //[SerializeField] SettingsPopup settingsPopup;
    [SerializeField] GameObject pauseMenu;
    private int score;
    private bool isPaused;
    
    [SerializeField] MouseLookY playerLookY; 
    [SerializeField] MouseLookX playerLookX; 
    [SerializeField] Weapon playerShooting; 
    
    public AudioClip clickSound;

    public void PlaySound()
    {
        SoundFXManager.instance.PlaySoundFXClip(clickSound, transform);
    }
    
    // void OnEnable()
    // {
    //     Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    // }
    // void OnDisable()
    // {
    //     Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    // }
    void Start()
    {
        //scoreLabel.text = score.ToString();
        pauseMenu.SetActive(false);
        //settingsPopup.Close();
        //pauseText.gameObject.SetActive(false);
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
        
        playerLookX.enabled = false;
        playerLookY.enabled = false;
        playerShooting.enabled = false;
        
        //pauseText.gameObject.SetActive(true);
        
        //AudioListener.pause = true;
    }
    
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Возобновление времени
        Cursor.lockState = CursorLockMode.Locked; // Блокировка курсора
        Cursor.visible = false; // Скрытие курсора
        
        playerLookX.enabled = true;
        playerLookY.enabled = true;
        playerShooting.enabled = true;
        
        //pauseText.gameObject.SetActive(false);
        
        //AudioListener.pause = false;
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
