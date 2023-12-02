using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button changeWeaponButton;
    [SerializeField] private Button turnOffChangeWeaponButton;
    [SerializeField] private Button loseToMainButton;
    [SerializeField] private Button wintoMainButton;
    [SerializeField] private Button onOffMusicButton;
    [SerializeField] private Button onOffVibrationButton;
    [SerializeField] private GameObject coin;      
    [SerializeField] private GameObject changeWeaponPanel;      
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject playPanel;      
    [SerializeField] private GameObject onMusic;
    [SerializeField] private GameObject offMusic;
    [SerializeField] private GameObject offVibration;
    [SerializeField] private GameObject onVibration;
    [SerializeField] private Slider slider;
    private bool boolMusic= true;
    private bool boolVibration= true;
    public TextMeshProUGUI textAlive;
    public TextMeshProUGUI killerName;
    public TextMeshProUGUI rank;
    public TextMeshProUGUI zoneAndHightScore;
    public TextMeshProUGUI currentZone; 
    public TextMeshProUGUI nextZone; 
    private void Awake()
    {
        turnOffChangeWeaponButton.onClick.AddListener(TurnOffChangeWeapon);
        changeWeaponButton.onClick.AddListener(TurnOnChangeWeapon);
        playButton.onClick.AddListener(TurnOffMainMenu);
        wintoMainButton.onClick.AddListener(WintoMainMenu);
        loseToMainButton.onClick.AddListener(LoseToMainMenu);
        onOffMusicButton.onClick.AddListener(OnOffMusic);
        onOffVibrationButton.onClick.AddListener(OnOffVibration);
    }
    public void TurnOnChangeWeapon()
    {
        mainMenuPanel.SetActive(false);
        changeWeaponPanel.SetActive(true);
        ShopWeaponUI.Instance.LoadStart();
    }
    public void TurnOffChangeWeapon()
    {
        mainMenuPanel.SetActive(true);
        changeWeaponPanel.SetActive(false);
        ShopWeaponUI.Instance.DestroyWeapon();
    }
    public void SetSlider()
    {
        slider.value =4+ (60-GameManager.Instance.aliveNumber)/10;
        /// cong thuc tinh slide
    }
    public void WintoMainMenu()
    {
        DataManager.Instance.currentZone++;
        LevelManager.Instance.Start();
        winPanel.SetActive(false);  
        mainMenuPanel.SetActive(true);
        SetZoneAndHightScore(DataManager.Instance.currentZone, GameManager.Instance.aliveNumber);
        coin.SetActive(true);
    }
    public void SetZoneAndHightScore(int zone, int hightScore)
    {
        zoneAndHightScore.text = "ZONE:" + (zone) + "  -  " + "BEST:#"+  (hightScore);
    }
    public void SetKillerName(string killerName)
    {
        this.killerName.text = killerName;
    }
    public void SetRank()
    {
        rank.text = "#"+ (GameManager.Instance.aliveNumber+1);
    }
    public void TurnLosePanel()
    { 
        losePanel.SetActive(true);
        playPanel.SetActive(false);
    }
    public bool CheckWinPanel()
    {
        if (winPanel.active == true) return true;
        else return false;
    }
    public void TurnWinPanel()
    {
        winPanel.SetActive(true);
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
