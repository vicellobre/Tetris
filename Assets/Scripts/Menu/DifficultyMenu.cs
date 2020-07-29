using UnityEngine;

public class DifficultyMenu : MonoBehaviour
{

    public void Easy()
    {
        ConfigurationUtils.Difficulty = Difficulty.Easy;
        AudioManager.Play(AudioClipName.Accept);
        MenuManager.GoToMenu(MenuName.Gameplay);
    }

    public void Medium()
    {
        ConfigurationUtils.Difficulty = Difficulty.Medium;
        AudioManager.Play(AudioClipName.Accept);
        MenuManager.GoToMenu(MenuName.Gameplay);
    }

    public void Hard()
    {
        ConfigurationUtils.Difficulty = Difficulty.Hard;
        AudioManager.Play(AudioClipName.Accept);
        MenuManager.GoToMenu(MenuName.Gameplay);
    }

    public void Back()
    {
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.Back);
    }
}