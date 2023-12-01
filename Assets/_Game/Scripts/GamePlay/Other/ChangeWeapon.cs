using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponClass : MonoBehaviour
{
    public void ChangeWeapon(WeaponType weaponType)
    {
        WeaponItemData weaponItemData=
            DataManager.Instance.weaponDataScriptableObject.listWeapon[(int)weaponType];
        
    }
}
