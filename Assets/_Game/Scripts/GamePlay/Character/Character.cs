using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Character : MonoBehaviour
{
    [SerializeField]private Transform throwPoint;
    private Vector3 direc;
    private float bulletSpeed=10;
    protected bool isAttack = false;
    protected string currentAnimName= constr.IDLE;
    protected string lastAnimName= constr.RUN;
    public WeaponItemData weaponData;
    public bool isDead = false;
    public Transform weaponContainer;
    public Transform hatContainer;
    public Transform shieldContainer;
    public GameObject pant;
    public GameObject currentSkin;
    public Weapon weapon;
    public bool isStopMove = false;
    public GameObject collider;
    public Vector3 scale = new Vector3(1, 1, 1);
    public float timer = 3;
    public Animator anim;
    public List<GameObject> botInRange = new List<GameObject>();
    public float timeRun = 3;
    protected virtual void Start()
    {
        weapon = LeanPool.Spawn(weaponData.weapon, weaponContainer);
        Transform weaponTransform = weapon.transform;
        weaponTransform.localPosition = weaponData.positionOffsetCharacter;
        weaponTransform.localRotation = Quaternion.Euler(weaponData.rotationOffsetCharacter);
    }
    public void OnAttack(Transform target)
    {
        SetAnimation(AnimationType.ATTACK);
        isAttack = true;
        lastAnimName = constr.ATTACK;
        Invoke(nameof(SpawnBullet), 0.4f);
        direc = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(direc.x, 0f, direc.z));
        timer = 0;
    }
    private void SpawnBullet()
    {
        if (isStopMove)
        {
            Bullet newBullet = LeanPool.Spawn(weaponData.bullet);
            newBullet.shooter = gameObject;
            newBullet.transform.position = throwPoint.position;
            newBullet.transform.rotation= Quaternion.LookRotation(new Vector3(direc.x, 0f, direc.z));
            newBullet.timer = 0;
            newBullet.rigidbody.velocity = transform.forward * bulletSpeed;
            Invoke(nameof(SetFalseAttack), 1f);
        }
    }
    private void SetFalseAttack()
    {
        isAttack = false;
        weapon.gameObject.SetActive(true);
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public void SetAnimation(AnimationType animationType)
    {
        switch(animationType){
            case AnimationType.ATTACK: 
                ChangeAnim(constr.ATTACK);
                break;
            case AnimationType.IDLE:
                ChangeAnim(constr.IDLE);
                break;
            case AnimationType.RUN:
                ChangeAnim(constr.RUN);
                break;
            case AnimationType.DEAD:
                ChangeAnim(constr.DEAD);
                break;
        }
    }
}
public enum AnimationType
{
    RUN,
    IDLE,
    DEAD,
    ATTACK,
}