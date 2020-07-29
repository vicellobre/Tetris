using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    void Start()
    {
        EventManager.Initialize();
        ConfigurationUtils.Initialize();
        ScreenUtils.Initialize();
    }
}