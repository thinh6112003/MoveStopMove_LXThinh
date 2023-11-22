using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] Rigidbody _rigidbody;
    private bool isSetIndicator=false;
    
    private void Update()
    {
        OnMove();
        if (Input.GetKeyDown(KeyCode.A)){
            GameObject newWeapon= Instantiate(weapon);
            newWeapon.transform.position = throwPoint.position;
            Rigidbody rigidbody=  newWeapon.AddComponent<Rigidbody>();
            rigidbody.useGravity= false ;
            rigidbody.velocity = transform.forward*5;
        }
        timer += Time.deltaTime;
        if (timer >=3&& isStopMove&& botInRange.Count>0)
        {
            OnAttack(botInRange[0].transform);
        }
    }
    

    private void OnMove()
    {
        Vector3 moveVector= Vector3.zero;
        moveVector.x = floatingJoystick.Horizontal* moveSpeed;
        moveVector.z = floatingJoystick.Vertical* moveSpeed;
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            ChangeAnim(constr.RUN);
            isStopMove = false;
            transform.rotation = Quaternion.LookRotation(new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical));
        }
        else
        {
            isStopMove = true;
            if (!isAttack) ChangeAnim(constr.IDLE);
        }
        _rigidbody.MovePosition(transform.position+moveVector);
    }

    
}
public static class constr
{
    public static string CHARACTER = "Character";
    public static string WEAPON = "Weapon";
    public static string IDLE = "idle";
    public static string RUN = "run";
    public static string DEAD = "dead";
}

