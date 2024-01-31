using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody rigidbody;
    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Horizontal!=0||vertical!=0)
        {
            rigidbody.velocity = Vector3.right* Horizontal *speed+Vector3.forward * vertical * speed;
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}
