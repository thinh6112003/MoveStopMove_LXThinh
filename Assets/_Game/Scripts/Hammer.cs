using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    private float rotationSpeed = -1000f;
    private float timer=0;
    private int characterLayer = 1 << 3;
    public string shooterName;
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed );
        timer += Time.deltaTime;
        if (timer > 0.6f) Destroy(gameObject);
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
