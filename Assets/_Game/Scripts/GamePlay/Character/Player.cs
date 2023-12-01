using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class Player : Character
{
    [SerializeField] private float moveSpeed;
    [SerializeField] Rigidbody _rigidbody;
    private bool isSetIndicator=false;
    private FloatingJoystick floatingJoystick;
    protected override void Start()
    {
        floatingJoystick = LevelManager.Instance.floatingJoystick;
    }
    private void Update()
    {
        if (isDead) return;
        OnMove();
        timer += Time.deltaTime;
        if (timer >=timeRun&& isStopMove&& botInRange.Count>0&& botInRange[0]!=null&& GameManager.Instance.gameState!= GameState.UNPLAY)
        {
            OnAttack(botInRange[0].transform);
        }
    }
    
    private void OnMove()
    {
        Vector3 moveVector= Vector3.zero;
        moveVector.x = floatingJoystick.Horizontal* moveSpeed;
        moveVector.z = floatingJoystick.Vertical* moveSpeed;
        if ((moveVector.x != 0 || moveVector.z != 0 )&& GameManager.Instance.gameState== GameState.PLAY)
        {
            if(currentAnimName!= constr.RUN) SetAnimation(AnimationType.RUN);
            isStopMove = false;
            lastAnimName = constr.RUN;
            transform.rotation = Quaternion.LookRotation(new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical));
        }
        else
        {
            isStopMove = true;
            moveVector = Vector3.zero;
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
                if (botInRange[i]!= null&&botInRange[i].name == other.name)
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
        if (other.CompareTag(constr.WEAPON) )
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if(bullet.shooter.name != gameObject.name)
            {
                SetAnimation(AnimationType.DEAD);
                LeanPool.Despawn(gameObject);
                if(DataManager.Instance.hightScore > GameManager.Instance.aliveNumber)
                {
                    DataManager.Instance.hightScore = GameManager.Instance.aliveNumber;
                }
                GameManager.Instance.gameState = GameState.UNPLAY;
                UIManager.Instance.SetKillerName(bullet.shooter.name);
                UIManager.Instance.SetZoneAndHightScore(DataManager.Instance.currentZone,GameManager.Instance.aliveNumber+1);
                UIManager.Instance.SetSlider();
                UIManager.Instance.TurnLosePanel();
                UIManager.Instance.SetRank();
                isDead = true;
            }
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

