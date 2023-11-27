using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Bot bot;
    private int ibot;
    void Start()
    {
        for(int i=0;i < 10  ; i++)
        {
            SpawnBot();
        }
    }
    public void SpawnBot()
    {
        if (ibot == 50) return;
        Bot newbot = Instantiate(bot);
        newbot.name = "bot " + ibot;
        float x = Random.Range(-19, 19), y = Random.Range(-19, 19);
        newbot.transform.position = new Vector3(x, 1, y);
        newbot.textName.text = "bot " + ibot;
        ibot++;
    }
}
