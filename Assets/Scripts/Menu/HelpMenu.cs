using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public void Back()
    {
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.Back);
    }
}