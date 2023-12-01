using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopUI : Singleton<ShopUI>
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI nameWeaponText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private int typeNumber;
    private Weapon weapon;
    private WeaponType currentWeaponType;
    private void Awake()
    {
        nextButton.onClick.AddListener(NextWeapon);
        backButton.onClick.AddListener(BackWeapon);
    }
    public void LoadStart()
    {
        WeaponItemData weaponItemData= 
            LevelManager.Instance.playerGamePlay.weaponData;
        currentWeaponType = weaponItemData.weaponType;
        weapon= Instantiate(weaponItemData.weapon, mainCamera);
        weapon.transform.localPosition = new Vector3(0, 0, 5);
        weapon.transform.localRotation = Quaternion.Euler(180, 20, 20);
    }
    public void DestroyWeapon()
    {
        Destroy(weapon.gameObject);
    }
    void NextWeapon()
    {
        int current = (int)currentWeaponType;
        current++;
        if (current == typeNumber) current = 0;
        Destroy(weapon.gameObject);
        LevelManager.Instance.playerGamePlay.weaponData= 
            DataManager.Instance.weaponDataScriptableObject.listWeapon[current];
        currentWeaponType = (WeaponType) current;
        ChangeWeaponClass.ChangeWeapon(currentWeaponType);
        weapon = Instantiate(
            LevelManager.Instance.playerGamePlay.weaponData.weapon, mainCamera);
        weapon.transform.localPosition = new Vector3(0, 0, 5);
        weapon.transform.localRotation = Quaternion.Euler(180, 20, 20);
    }
    void BackWeapon()
    {
        int current = (int)currentWeaponType;
        current--;
        if (current == -1) current = typeNumber-1;
        LevelManager.Instance.playerGamePlay.weaponData =
            DataManager.Instance.weaponDataScriptableObject.listWeapon[current];
        currentWeaponType = (WeaponType)current;
        ChangeWeaponClass.ChangeWeapon(currentWeaponType);
        Weapon weapon = Instantiate(
            LevelManager.Instance.playerGamePlay.weaponData.weapon, mainCamera);
        weapon.transform.localPosition = new Vector3(0, 0, 5);
        weapon.transform.localRotation = Quaternion.Euler(180, 20, 20);
    }

}
