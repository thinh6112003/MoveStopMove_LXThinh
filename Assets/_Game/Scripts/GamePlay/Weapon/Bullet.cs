using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class Bullet : MonoBehaviour
{
    public GameObject shooter;
    public Rigidbody rigidbody;
    public float timer = 0;
    public int characterLayer = 1 << 3;
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.6f)LeanPool.Despawn(gameObject);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            //Debug.Log(other.name); 
            if (other.name != shooter.name)
            {
                LevelManager.Instance.SpawnBot();
                if (other.GetComponent<Character>().isDead== false)
                {
                    GameManager.Instance.SetAlive();
                    //Debug.Log(other.name);
                    other.GetComponent<Character>().isDead = true;
                }
                Character character= shooter.GetComponent<Character>();
                character.scale = new Vector3(character.scale.x + 0.03f, character.scale.y + 0.03f, character.scale.z + 0.03f);
                character.gameObject.transform.localScale = character.scale;
                Destroy(gameObject);
            }
        }
    }
}
