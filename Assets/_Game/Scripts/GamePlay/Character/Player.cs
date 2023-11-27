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
    protected override void Start()
    {
        weaponData = DataManager.Instance.GetWeaponData(WeaponType.HAMMER);
        base.Start();
    }
    private void Update()
    {
        if (isDead) return;
        OnMove();
        timer += Time.deltaTime;
        if (timer >=timeRun&& isStopMove&& botInRange.Count>0&& botInRange[0]!=null)
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
            if(currentAnimName!= constr.RUN) SetAnimation(AnimationType.RUN);
            isStopMove = false;
            lastAnimName = constr.RUN;
            transform.rotation = Quaternion.LookRotation(new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical));
        }
        else
        {
            isStopMove = true;
            if (!isAttack || lastAnimName==constr.RUN) SetAnimation(AnimationType.IDLE);
        }
        _rigidbody.MovePosition(transform.position+moveVector);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(constr.CHARACTER))
        {
            for (int i = 0; i < botInRange.Count; i++)
            {
                if (botInRange[i].name == other.name)
                {
                    other.GetComponent<Bot>().indicator.SetActive(false);
                    botInRange.RemoveAt(i);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(constr.CHARACTER))
        {
            other.GetComponent<Bot>().indicator.SetActive(true);
            botInRange.Add(other.gameObject);
        }
        if (other.CompareTag(constr.WEAPON) && other.GetComponent<Bullet>().shooter.name != gameObject.name)
        {
            SetAnimation(AnimationType.DEAD);
            isDead = true;
        }
    }

}
public static class constr
{
    public static string CHARACTER = "Character";
    public static string WEAPON = "Weapon";
    public static string IDLE = "idle";
    public static string RUN = "run";
    public static string DEAD = "dead";
    public static string ATTACK = "attack";
}

