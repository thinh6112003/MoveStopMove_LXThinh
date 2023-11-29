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

