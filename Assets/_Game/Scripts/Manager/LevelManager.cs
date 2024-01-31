using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.AI;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Bot bot;
    [SerializeField] private List<Bot> listBot;
    private GameObject zone;
    private int ibot;
    public FloatingJoystick floatingJoystick;
    public Player playerGamePlay;
    public void Start()
    {
        if (zone!= null) Destroy(zone);
        //if(playerGamePlay!= null)LeanPool.Despawn(playerGamePlay);
        if (playerGamePlay== null||!playerGamePlay.gameObject.active)
        { 
            playerGamePlay = LeanPool.Spawn(playerPrefab);
        }
        else
        {
            playerGamePlay.transform.position = new Vector3(0, 1, 0);
        }
        playerGamePlay.scale = new Vector3(1, 1, 1);
        playerGamePlay.transform.localScale = playerGamePlay.scale;
        zone = Instantiate((GameObject)Resources.Load($"Zone" + DataManager.Instance.currentZone));
        //NavMeshBuilder.BuildNavMesh();
        if (playerGamePlay.weapon == null)
        {
            playerGamePlay.SetWeapon();
        }
        
        GameManager.Instance.aliveNumber = 50;
        playerGamePlay.isDead = false;
        playerGamePlay.botInRange = new List<GameObject>();
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
    public void ChangeFullSkin(int fullSkinType)
    {
        FullSkinItemData fullSkinItemData = DataManager.Instance.fullSkinDataSO.listFullSkin[fullSkinType];
        Destroy(playerGamePlay.gameObject);
        playerGamePlay = Instantiate(fullSkinItemData.model);
        playerGamePlay.transform.position = new Vector3(0, 1, 0);
        playerGamePlay.SetWeapon();
    }
    public void setEndGame()
    {
        for(int i=0;i< listBot.Count; i++)
        {
            if(listBot[i]!= null)
            {
                listBot[i].myAgent.SetDestination(listBot[i].transform.position);
                listBot[i].rigidbody.velocity = Vector3.zero;
            }
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
