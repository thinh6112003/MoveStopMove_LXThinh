using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponClass : MonoBehaviour
{
    public static void ChangeWeapon(WeaponType weaponType)
    {
        LevelManager.Instance.playerGamePlay.weaponData =
            DataManager.Instance.weaponDataSO.listWeapon[(int)weaponType];
        Destroy(LevelManager.Instance.playerGamePlay.weapon.gameObject);
        Weapon weapon=
               Instantiate( LevelManager.Instance.playerGamePlay.weaponData.weapon,
               LevelManager.Instance.playerGamePlay.weaponContainer);
        LevelManager.Instance.playerGamePlay.weapon = weapon;
        weapon = LevelManager.Instance.playerGamePlay.weapon;
        weapon.transform.localPosition =LevelManager.Instance.playerGamePlay.weaponData.positionOffsetCharacter;
        weapon.transform.localRotation = Quaternion.Euler(
            LevelManager.Instance.playerGamePlay.weaponData.rotationOffsetCharacter);
    }
}
