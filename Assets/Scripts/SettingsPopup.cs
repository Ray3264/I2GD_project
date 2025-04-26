using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] Slider speedSlider;
    
    public TMP_InputField nameInput;
    [SerializeField] TMP_Text nameText;
    
    [SerializeField] AudioSource music1Source;
    
    private float minSpeed = 1f; // Минимальная скорость
    private float maxSpeed = 10f; // Максимальная скорость
    void Start()
    {
        nameInput.onEndEdit.AddListener(OnSubmitName);
        
        speedSlider.value = PlayerPrefs.GetFloat("speed", 0.5f);
        speedSlider.onValueChanged.AddListener(OnSpeedValue);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void OnSubmitName(string name)
    {
        nameText.text = name;
        Debug.Log(name);
    }
    public void OnSpeedValue(float sliderValue)
    {
        float speed = minSpeed + (maxSpeed - minSpeed) * sliderValue;
        //Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
        PlayerPrefs.SetFloat("speed", speed);
    }
    
    public void PlayMusic1()
    {
        music1Source.clip = Resources.Load("Music/06 - Quad machine") as AudioClip;
        music1Source.Play();
    }
    public void PlayMusic2()
    {
        music1Source.clip = Resources.Load("Music/loop") as AudioClip;
        music1Source.Play();
    }

}
