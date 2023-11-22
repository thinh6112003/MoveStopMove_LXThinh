using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class Bullet : MonoBehaviour
{
    public float timer = 0;
    public int characterLayer = 1 << 3;
    public string shooterName;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.6f) LeanPool.Despawn(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            //Debug.Log(other.name); 
            if (other.name != shooterName)
            {
                Destroy(gameObject);
            }
        }
    }
}
