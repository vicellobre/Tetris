using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    public static void GoToMenu(MenuName name)
    {
        switch (name)
        {
            case MenuName.Main:
                SceneManager.LoadScene("MainMenu");
                break;

            case MenuName.Pause:
                Object.Instantiate(Resources.Load("Prefabs/PauseMenu"), Vector3.zero + GameObject.FindGameObjectWithTag("HUD").transform.position,
                    Quaternion.identity,
                    GameObject.FindGameObjectWithTag("HUD").transform);
                break;

            case MenuName.Help:
                SceneManager.LoadScene("HelpMenu");
                break;

            case MenuName.GameOver:
                Object.Instantiate(Resources.Load("Prefabs/GameOver"),
                    Vector3.zero + GameObject.FindGameObjectWithTag("HUD").transform.position,
                    Quaternion.identity,
                    GameObject.FindGameObjectWithTag("HUD").transform);
                break;

            case MenuName.Gameplay:
                ConfigurationUtils.GameOver = false;
                SceneManager.LoadScene("Gameplay");
                break;

            case MenuName.Difficulty:
                SceneManager.LoadScene("DifficultyMenu");
                break;

            case MenuName.Record:
                SceneManager.LoadScene("RecordMenu");
                break;

            case MenuName.Configuration:
                SceneManager.LoadScene("ConfigurationMenu");
                break;
        }
    }
}