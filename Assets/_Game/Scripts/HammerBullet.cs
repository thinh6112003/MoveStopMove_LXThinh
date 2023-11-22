using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class HammerBullet : Bullet
{

    private float rotationSpeed = -1000f;
    
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed );
    }
}
