using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    [SerializeField] private Button playButton;
    [SerializeField] public GameState gameState=GameState.UNPLAY;
    public int aliveNumber=50;

    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
        playButton.onClick.AddListener(ChangeGameState);
    }
    private void Update()
    {
        if(aliveNumber==1&& LevelManager.Instance.playerGamePlay.isDead != true&& !UIManager.Instance.CheckWinPanel())
        {
            gameState = GameState.UNPLAY;
            UIManager.Instance.TurnWinPanel();
        }
    }
    public void SetAlive()
    {
        aliveNumber--;
        UIManager.Instance.SetAlive(aliveNumber);
    }
    void ChangeGameState()
    {
        gameState = GameState.PLAY;
        UIManager.Instance.SetAlive(aliveNumber);
        //UIManager.Instance.
    }
}

public enum GameState
{
    PLAY,
    UNPLAY
}

