using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private Text scoreChild;
    private Text score;

    public Text Score => scoreChild;

    private void Start()
    {
        Time.timeScale = 0;
        if (GameObject.FindGameObjectWithTag("Score") && GameObject.FindGameObjectWithTag("ButtonPause"))
        {
            score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
            scoreChild.text = score.text;
            score.transform.parent.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("ButtonPause").SetActive(false);
        }
        SetPlayerPrefs();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        AudioManager.Play(AudioClipName.Accept);
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Gameplay);
    }

    public void QuitMenu()
    {
        Time.timeScale = 1;
        AudioManager.Play(AudioClipName.Back);
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }

    private void SetPlayerPrefs()
    {
        string highScore;
        switch (ConfigurationUtils.Difficulty)
        {
            case Difficulty.Easy:
                highScore = PlayerPrefs.GetString("EasyHighScore");
                if (string.IsNullOrEmpty(highScore) || int.Parse(highScore) < int.Parse(score.text))
                {
                    PlayerPrefs.SetString("EasyHighScore", score.text);
                }
                break;

            case Difficulty.Medium:
                highScore = PlayerPrefs.GetString("MediumHighScore");
                if (string.IsNullOrEmpty(highScore) || int.Parse(highScore) < int.Parse(score.text))
                {
                    PlayerPrefs.SetString("MediumHighScore", score.text);
                }
                break;

            case Difficulty.Hard:
                highScore = PlayerPrefs.GetString("HardHighScore");
                if (string.IsNullOrEmpty(highScore) || int.Parse(highScore) < int.Parse(score.text))
                {
                    PlayerPrefs.SetString("HardHighScore", score.text);
                }
                break;
        }
    }
}