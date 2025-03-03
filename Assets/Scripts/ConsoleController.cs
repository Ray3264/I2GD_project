using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    public FanController fan;
    
    public void Operate()
    {
        fan.ToggleFan();
    }
    
}
