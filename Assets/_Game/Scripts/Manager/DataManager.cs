using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataScriptableObject weaponDataScriptableObject;
    public int currentZone=1;
    public int hightScore=50;
    private void Start()
    {

    }
    public WeaponItemData GetWeaponData(WeaponType weapontype)
    {
        for(int i=0;i< weaponDataScriptableObject.listWeapon.Count; i++)
        {
            if(weaponDataScriptableObject.listWeapon[i].weaponType==weapontype)
            {
                return weaponDataScriptableObject.listWeapon[i];
            }
        }
        return null;
    }

}
