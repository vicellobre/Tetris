using UnityEngine;
using UnityEngine.UI;

public class ConfigurationMenu : MonoBehaviour
{
    #region Fields
    [SerializeField] [HideInInspector]
    private Image       buttonMusic;
    private GameObject  music, speakers;
    [SerializeField]
    private Sprite      imageEnable, imageDisable;
    #endregion


    #region Properties
    public Image ButtonMusic => buttonMusic; 
    #endregion


    #region Public Methods
    public void EneableMusic()
    {
        AudioManager.Play(AudioClipName.Accept);
        ConfigurationUtils.Music = !ConfigurationUtils.Music;
        music.SetActive(ConfigurationUtils.Music);
        buttonMusic.sprite = ConfigurationUtils.Music ? imageEnable : imageDisable;
    }

    public void Back()
    {
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.Back);
    }
    #endregion


    #region Private Methods
    private void Start()
    {
        speakers = GameObject.FindGameObjectWithTag("Speakers");
        music = speakers.transform.GetChild(0).gameObject;

        buttonMusic.sprite = ConfigurationUtils.Music ? imageEnable : imageDisable;
    }
    #endregion
}