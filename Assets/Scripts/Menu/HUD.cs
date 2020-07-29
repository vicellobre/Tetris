using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    bool active;
    Text score;

    void Awake()
    {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }

    void Start()
    {
        EventManager.AddListener(EventName.PointsEvent, HandlePointsEvent);
        EventManager.AddListener(EventName.GameOverEvent, HandleGameOverEvent);
        active = false;
    }

    public void Pause()
    {
        AudioManager.Play(AudioClipName.Accept);
        MenuManager.GoToMenu(MenuName.Pause);
    }

    void HandlePointsEvent(string pts)
    {
        int points = int.Parse(pts) + int.Parse(score.text);
        score.text = points.ToString();
    }

    void HandleGameOverEvent(string nothing)
    {
        if (!active)
        {
            AudioManager.Play(AudioClipName.GameOver);
            MenuManager.GoToMenu(MenuName.GameOver);
            active = true;
        }
    }
}