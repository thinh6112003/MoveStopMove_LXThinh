using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum WeaponType{ 
    HAMMER,
    KNIFE,
    UZI,
    CANDI0,
    CANDI1,
    CANDI2,
    CANDI3,
    BOOMERANG,
    AXE0,
    AXE1,
    ARROW,
}
[Serializable]
public class WeaponItemData
{
    public WeaponType weaponType;
    public Bullet bullet;
    public Weapon weapon;
    public string name;
    public int price;
    public Vector3 rotationOffsetShop;
    public Vector3 rotationOffsetCharacter;
    public Vector3 positionOffsetCharacter;
}
