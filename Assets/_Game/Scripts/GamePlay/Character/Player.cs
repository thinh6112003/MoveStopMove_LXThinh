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
        if (timer >=timeRun&& isStopMove&& botInRange.Count>0&& GameManager.Instance.gameState!= GameState.UNPLAY)
        {
            if (botInRange[0] != null) OnAttack(botInRange[0].transform);
            else botInRange.RemoveAt(0);
        }
    }
    
    private void OnMove()
    {
        Vector3 moveVector= Vector3.zero;
        moveVector.x = floatingJoystick.Horizontal* moveSpeed* Time.deltaTime;
        moveVector.z = floatingJoystick.Vertical* moveSpeed* Time.deltaTime;
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
                LevelManager.Instance.setEndGame();
                LeanPool.Despawn(gameObject);
                if(DataManager.Instance.hightScore > GameManager.Instance.aliveNumber)
                {
                    DataManager.Instance.hightScore = GameManager.Instance.aliveNumber;
                }
                GameManager.Instance.gameState = GameState.UNPLAY;
                UIManager.Instance.SetKillerName(bullet.shooter.name);
                UIManager.Instance.SetZoneAndHightScore(DataManager.Instance.currentZone, DataManager.Instance.hightScore);
                UIManager.Instance.SetSlider();
                UIManager.Instance.TurnLosePanel();
                UIManager.Instance.SetRank();
                isDead = true;
            }
        }
    }
    public void SetWeapon()
    {
        weaponData =DataManager.Instance.GetWeaponData(DataManager.Instance.userData.currentWeaponType);
        weapon =Instantiate(weaponData.weapon, weaponContainer);
        weapon.transform.localPosition = weaponData.positionOffsetCharacter;
        weapon.transform.localRotation = Quaternion.Euler(weaponData.rotationOffsetCharacter);
    }
    public void ChangeWeapon(WeaponType weaponType)
    {
        weaponData = DataManager.Instance.listWeaponItemData[(int)weaponType];
        Destroy(this.weapon.gameObject);
        weapon =Instantiate(weaponData.weapon,weaponContainer);
        this.weapon.transform.localPosition = weaponData.positionOffsetCharacter;
        this.weapon.transform.localRotation = Quaternion.Euler(weaponData.rotationOffsetCharacter);
    }
    public void ChangeHat(int hatType)
    {
        HatItemData hatItemData = DataManager.Instance.hatDataSO.listHat[hatType];
        if(currentSkin!= null)
        {
            Destroy(currentSkin);
        }
        currentSkin = Instantiate(hatItemData.model, hatContainer);
        currentSkin.transform.localPosition = hatItemData.positionOffset;
        currentSkin.transform.localRotation = Quaternion.Euler( hatItemData.rotationOffset);
        currentSkin.transform.localScale= hatItemData.scaleOffset;
    }
    public void ChangeShort(int shortType)
    {
        ShortItemData shortItemData = DataManager.Instance.shortDataSO.listShort[shortType];
        pant.GetComponent<Renderer>().material = shortItemData.material;
    }
    public void ChangeShield(int accessoryType)
    {
        AccessoryItemData accessoryItemData = DataManager.Instance.accessoryDataSO.listAccessory[accessoryType];
        if (currentSkin != null)
        {
            Destroy(currentSkin);
        }
        currentSkin = Instantiate(accessoryItemData.model, shieldContainer);
        currentSkin.transform.localPosition = accessoryItemData.positionOffset;
        currentSkin.transform.localRotation = Quaternion.Euler(accessoryItemData.rotationOffset);
        currentSkin.transform.localScale = accessoryItemData.scaleOffset;
    }
}
public static class constr
{
    public static string PLAYERPREFKEY = "PlayerprefKey";
    public static string CHARACTER = "Character";
    public static string WEAPON = "Weapon";
    public static string IDLE = "idle";
    public static string RUN = "run";
    public static string DEAD = "dead";
    public static string ATTACK = "attack";
}

