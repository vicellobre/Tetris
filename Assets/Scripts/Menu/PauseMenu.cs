using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject pause;

    private void Start()
    {
        pause = GameObject.FindGameObjectWithTag("ButtonPause");
        pause.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        AudioManager.Play(AudioClipName.Accept);
        pause.SetActive(true);
        Destroy(gameObject);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        AudioManager.Play(AudioClipName.Accept);
        MenuManager.GoToMenu(MenuName.Gameplay);
    }
    public void QuitMenu()
    {
        Time.timeScale = 1;
        AudioManager.Play(AudioClipName.Back);
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }
}