using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject weapon;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] GameObject hammer;
    private bool isSetIndicator=false;
    private bool isStopMove=false;
    private bool isAttack=false;
    private string currentAnimName;
    private float timer = 3;
    private List<GameObject> botInRange = new List<GameObject>();
    
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
            ChangeAnim("attack");
            isAttack = true;
            Invoke(nameof(OnAttack), 0.4f);
            Vector3 direc = botInRange[0].transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(direc.x,0f,direc.z));
            timer = 0;
        }
    }

    private void OnAttack()
    {
        if (isStopMove)
        {
            hammer.SetActive(false);
            GameObject newWeapon = Instantiate(weapon);
            newWeapon.transform.position = throwPoint.position;
            Rigidbody rigidbody = newWeapon.AddComponent<Rigidbody>();
            rigidbody.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            rigidbody.useGravity = false;
            rigidbody.velocity = transform.forward * 5;
        }
            Invoke(nameof(SetFalseAttack), 1f);
    }

    private void SetFalseAttack()
    {
        isAttack = false;
        hammer.SetActive(true);
    }

    private void OnMove()
    {
        Vector3 moveVector= Vector3.zero;
        moveVector.x = floatingJoystick.Horizontal* moveSpeed;
        moveVector.z = floatingJoystick.Vertical* moveSpeed;
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            ChangeAnim("run");
            isStopMove = false;
            transform.rotation = Quaternion.LookRotation(new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical));
        }
        else
        {
            isStopMove = true;
            if (!isAttack) ChangeAnim("idle");
        }
        _rigidbody.MovePosition(transform.position+moveVector);
    }
    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName); 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            botInRange.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i = 0; i < botInRange.Count; i++)
            {
                if(botInRange[i].name == other.name)
                {
                    botInRange.RemoveAt(i);
                }
            }
        }        
    }
}
