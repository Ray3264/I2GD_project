using System.Collections;
using UnityEngine;

public class PlayerAnxiety : MonoBehaviour
{
    public static PlayerAnxiety Instance { get; private set; } // Глобальный доступ к единственному экземпляру

    public float anxietyLevel = 0f;
    public float maxAnxiety = 100f;
    public float anxietyDecayRate = 1f;
    public bool isHallucinating = false;
    
    public float anxietyIncreaseRate = 10f; // Скорость увеличения тревожности
    public float increaseInterval = 10f; // Интервал в секундах между увеличениями

    void Awake()
    {
        // Если уже есть экземпляр, удаляем новый
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Устанавливаем этот объект как единственный экземпляр
        Instance = this;
        DontDestroyOnLoad(gameObject); // Оставляем объект при смене сцен
    }

    void Start()
    {
        IncreaseAnxiety(50f);
    }
    void Update()
    {
        
        if (anxietyLevel > 0)
        {
            anxietyLevel -= anxietyDecayRate * Time.deltaTime;
            anxietyLevel = Mathf.Clamp(anxietyLevel, 0, maxAnxiety);
        }

        if (anxietyLevel >= 70 && !isHallucinating)
        {
            StartHallucinations();
        }
        else if (anxietyLevel < 70 && isHallucinating)
        {
            StopHallucinations();
        }
    }
    
    
    public void IncreaseAnxiety(float amount)
    {
        anxietyLevel += amount;
        anxietyLevel = Mathf.Clamp(anxietyLevel, 0, maxAnxiety);
    }

    public void ReduceAnxiety(float amount)
    {
        anxietyLevel -= amount;
        anxietyLevel = Mathf.Clamp(anxietyLevel, 0, maxAnxiety);
    }

    void StartHallucinations()
    {
        isHallucinating = true;
        Debug.Log("Hallucinations started!");
    }

    void StopHallucinations()
    {
        isHallucinating = false;
        Debug.Log("Hallucinations stopped!");
    }
    
}