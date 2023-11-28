using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button onOffMusicButton;
    [SerializeField] private Button onOffVibrationButton;
    [SerializeField] private GameObject coin;      
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject playPanel;      
    [SerializeField] private GameObject onMusic;
    [SerializeField] private GameObject offMusic;
    [SerializeField] private GameObject offVibration;
    [SerializeField] private GameObject onVibration;
    private bool boolMusic= true;
    private bool boolVibration= true;
    public TextMeshProUGUI textAlive;
    public TextMeshProUGUI killerName;
    public TextMeshProUGUI rank;
    private void Awake()
    {
        playButton.onClick.AddListener(TurnOffMainMenu);
        continueButton.onClick.AddListener(LoseToMainMenu);
        onOffMusicButton.onClick.AddListener(OnOffMusic);
        onOffVibrationButton.onClick.AddListener(OnOffVibration);
    }
    public void SetKillerName(string killerName)
    {
        this.killerName.text = killerName;
    }
    public void SetRank()
    {
        rank.text = "#"+ GameManager.Instance.aliveNumber;
    }
    public void TurnLosePanel()
    { 
        losePanel.SetActive(true);
        playPanel.SetActive(false);
    }
    public void LoseToMainMenu() {
        losePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        LevelManager.Instance.Start();
        
        coin.SetActive(true);
    }
    public void TurnOffMainMenu()
    {
        mainMenuPanel.SetActive(false);
        playPanel.SetActive(true);
        coin.SetActive(false);
    }
    public void SetAlive(int aliveNumber)
    {
        textAlive.text = "Alive: " + aliveNumber;
    }
    void OnOffMusic()
    {
        boolMusic = !boolMusic;
        onMusic.SetActive(boolMusic);
        offMusic.SetActive(!boolMusic);
    }
    void OnOffVibration()
    {
        boolVibration = !boolVibration;
        onVibration.SetActive(boolVibration);
        offVibration.SetActive(!boolVibration);
    }
}
