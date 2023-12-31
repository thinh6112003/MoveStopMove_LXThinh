using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziBullet : Bullet
{

    private float rotationSpeed = -1000f;
    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
    }
}
