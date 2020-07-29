using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManager.Play(AudioClipName.Accept);
        MenuManager.GoToMenu(MenuName.Difficulty);
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();
        AudioManager.Play(AudioClipName.Back);
        Application.Quit();
    }

    public void Help()
    {
        AudioManager.Play(AudioClipName.Accept);
        MenuManager.GoToMenu(MenuName.Help);
    }

    public void Record()
    {
        AudioManager.Play(AudioClipName.Accept);
        MenuManager.GoToMenu(MenuName.Record);
    }

    public void Configuration()
    {
        AudioManager.Play(AudioClipName.Accept);
        MenuManager.GoToMenu(MenuName.Configuration);
    }
}