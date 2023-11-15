using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    private float rotationSpeed = -1000f;
    private float timer=0;
    private int botLayer = 1 << 3;
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed );
        timer += Time.deltaTime;
        if (timer > 1) Destroy(gameObject);
    }
}
