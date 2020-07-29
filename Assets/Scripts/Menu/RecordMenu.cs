using UnityEngine;
using UnityEngine.UI;

public class RecordMenu : MonoBehaviour
{
    [SerializeField]
    private Text easyText, mediumText, hardText;

    public Text EasyText => easyText;
    public Text MediumText => mediumText;
    public Text HardText => hardText;

    private void Start()
    {
        string easy = PlayerPrefs.GetString("EasyHighScore");
        if (!string.IsNullOrEmpty(easy))
        {
            easyText.text = easy;
        }

        string medium = PlayerPrefs.GetString("MediumHighScore");
        if (!string.IsNullOrEmpty(medium))
        {
            mediumText.text = medium;
        }

        string hard = PlayerPrefs.GetString("HardHighScore");
        if (!string.IsNullOrEmpty(hard))
        {
            hardText.text = hard;
        }
    }

    public void Back()
    {
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.Back);
    }
}