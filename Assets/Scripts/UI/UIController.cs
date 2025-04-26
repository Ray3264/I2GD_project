using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] TMP_Text pauseText;
    [SerializeField] SettingsPopup settingsPopup;
    private int score;
    private bool isPaused = false;
    
    [SerializeField] MouseLookY playerLookY; 
    [SerializeField] MouseLookX playerLookX; 
    [SerializeField] RayShooter playerShooting; 
    
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
        score = 0;
        scoreLabel.text = score.ToString();
        settingsPopup.Close();
        pauseText.gameObject.SetActive(false);
    }


    private void OnEnemyHit()
    {
        score += 1;
        scoreLabel.text = score.ToString();
    }
    public void OnOpenSettings()
    {
        settingsPopup.Open();
        PauseGame();
    }
    public void OnCloseSettings()
    {
        settingsPopup.Close();
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
        
        pauseText.gameObject.SetActive(true);
        
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
        
        pauseText.gameObject.SetActive(false);
        
        //AudioListener.pause = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            OnCloseSettings();
        }
    }

}
