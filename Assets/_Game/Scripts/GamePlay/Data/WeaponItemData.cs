using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum WeaponType{ 
    HAMMER,
    KNIFE,
}
[Serializable]
public class WeaponItemData
{
    public WeaponType weaponType;
    public Bullet bullet;
    public Weapon weapon;
    public float range;
}
