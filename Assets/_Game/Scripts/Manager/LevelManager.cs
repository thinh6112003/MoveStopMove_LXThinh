using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Bot bot;
    [SerializeField] private List<Bot> listBot;
    public Player playerGamePlay;
    public FloatingJoystick floatingJoystick;
    private int ibot;
    public void Start()
    {
        playerGamePlay= LeanPool.Spawn(playerPrefab);
        playerGamePlay.isDead = false;
        ibot = 0;
        for(int i = listBot.Count-1; i > 0; i--)
        {
            if(listBot[i]!= null)Destroy(listBot[i].gameObject);
            listBot.RemoveAt(i);
        }
        for(int i=0;i < 10; i++)
        {
            SpawnBot();
        }
    }
    public void SpawnBot()
    {
        if (ibot == 49) return;
        Bot newbot = Instantiate(bot);
        listBot.Add(newbot);
        newbot.name = "bot " + ibot;
        float x = Random.Range(-19, 19), y = Random.Range(-19, 19);
        newbot.transform.position = new Vector3(x, 1, y);
        newbot.textName.text = "bot " + ibot;
        ibot++;
    }
}
