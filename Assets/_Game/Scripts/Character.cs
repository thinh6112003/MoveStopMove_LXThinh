using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Character : MonoBehaviour
{
    public bool isStopMove = false;
    protected bool isDead = false;
    protected bool isAttack = false;
    protected string currentAnimName;
    [SerializeField]private Transform throwPoint;
    [SerializeField]private Transform weaponContainer;
    private Weapon weapon;
    public float timer = 3;
    public Animator anim;
    public List<GameObject> botInRange = new List<GameObject>();
    public WeaponItemData weaponData;
    protected virtual void Start()
    {
        Debug.Log(gameObject.name);
        weaponData = DataManager.Instance.GetWeaponData(Weapontype.hammer);
        weapon= Instantiate(weaponData.weapon ,weaponContainer);
        weapon.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localRotation= Quaternion.Euler(180,0,0); 
    }
    private void Update()
    {
        if (isDead) return;
    }
    public void OnAttack(Transform target)
    {
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(SpawnBullet), 0.4f);
        Vector3 direc = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(direc.x, 0f, direc.z));
        timer = 0;
    }
    private void SpawnBullet()
    {
        if (isStopMove)
        {
            Bullet newBullet = LeanPool.Spawn(weaponData.bullet);
            newBullet.shooterName = gameObject.name;
            newBullet.transform.position = throwPoint.position;
            Rigidbody rigidbody = newBullet.gameObject.AddComponent<Rigidbody>();
            newBullet.transform.rotation = Quaternion.Euler(-90, 0, 0);
            rigidbody.useGravity = false;
            rigidbody.velocity = transform.forward * 10f;
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
        if (other.CompareTag(constr.WEAPON) && other.GetComponent<Bullet>().shooterName != gameObject.name)
        {
            ChangeAnim("dead");
            isDead = true;
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
