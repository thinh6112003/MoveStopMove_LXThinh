using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Weapontype{ 
    hammer,
    knife,
}
[Serializable]
public class WeaponItemData
{
    public Weapontype weaponType;
    public Bullet bullet;
    public Weapon weapon;
    public float range;
}
