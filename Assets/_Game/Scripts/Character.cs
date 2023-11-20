using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public bool isStopMove = false;
    public bool isAttack = false;
    public string currentAnimName;
    public Transform throwPoint;
    public float timer = 3;
    public GameObject weapon;
    public GameObject hammer;
    public Animator anim;
    public List<GameObject> botInRange = new List<GameObject>();
    public void OnAttack(Transform target)
    {
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(spawnHammer), 0.4f);
        Vector3 direc = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(direc.x, 0f, direc.z));
        timer = 0;
    }
    private void spawnHammer()
    {
        if (isStopMove)
        {
            hammer.SetActive(false);
            GameObject newWeapon = Instantiate(weapon);
            newWeapon.GetComponent<Hammer>().shooterName = gameObject.name;
            newWeapon.transform.position = throwPoint.position;
            Rigidbody rigidbody = newWeapon.AddComponent<Rigidbody>();
            rigidbody.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            rigidbody.useGravity = false;
            rigidbody.velocity = transform.forward * 10f;
            Invoke(nameof(SetFalseAttack), 1f);
        }
    }

    private void SetFalseAttack()
    {
        isAttack = false;
        hammer.SetActive(true);
    }
    public void ChangeAnim(string animName)
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
        if (other.CompareTag(constr.CHARACTER))
        {
            botInRange.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(constr.CHARACTER))
        {
            for (int i = 0; i < botInRange.Count; i++)
            {
                if (botInRange[i].name == other.name)
                {
                    botInRange.RemoveAt(i);
                }
            }
        }
    }
}
