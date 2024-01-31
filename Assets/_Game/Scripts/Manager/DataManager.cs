using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataSO weaponDataSO;
    public List<WeaponItemData> listWeaponItemData;
    public HatDataSO hatDataSO;
    public ShortDataSO shortDataSO;
    public AccessoryDataSO accessoryDataSO;
    public FullSkinDataSO fullSkinDataSO;
    public int currentZone=1;
    public int hightScore= 50;
    public UserData userData;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        //PlayerPrefs.DeleteKey(constr.PLAYERPREFKEY);
        listWeaponItemData = weaponDataSO.listWeapon;
        GetPlayerpref();
        if (userData == null)
        {
            SetUserData();
        }
    }
    public void SetUserData()
    {
        userData = new UserData();
        userData.currentCoint = 1000;
        userData.weaponBuyingStatus = new bool[weaponDataSO.listWeapon.Count];
        userData.weaponBuyingStatus[0] = true;
        userData.currentWeaponType = WeaponType.HAMMER;
        for (int i = 1; i < weaponDataSO.listWeapon.Count; i++) { 
            userData.weaponBuyingStatus[i] = false;
        }
        userData.hatBuyingStatus = new bool[hatDataSO.listHat.Count];
        for(int i=0;i< hatDataSO.listHat.Count; i++)
        {
            userData.hatBuyingStatus[i] = false;
        }
        userData.shortBuyingStatus = new bool[shortDataSO.listShort.Count];
        for (int i = 0; i < shortDataSO.listShort.Count; i++)
        {
            userData.shortBuyingStatus[i] = false;
        }
        userData.accessoryBuyingStatus = new bool[accessoryDataSO.listAccessory.Count];
        for (int i = 0; i < accessoryDataSO.listAccessory.Count; i++)
        {
            userData.accessoryBuyingStatus[i] = false;
        }
        userData.fullSkinBuyingStatus = new bool[fullSkinDataSO.listFullSkin.Count];
        for (int i = 0; i < fullSkinDataSO.listFullSkin.Count; i++)
        {
            userData.fullSkinBuyingStatus[i] = false;
        }
        SetPlayerpref();
    }
    public void SetPlayerpref()
    {
        string dynamicDataString = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString(constr.PLAYERPREFKEY, dynamicDataString);
    }
    public void GetPlayerpref()
    {
        string dynamicDataString = PlayerPrefs.GetString(constr.PLAYERPREFKEY);
        userData = JsonUtility.FromJson<UserData>(dynamicDataString);
    }
    public WeaponItemData GetWeaponData(WeaponType weapontype)
    {
        for(int i=0;i< weaponDataSO.listWeapon.Count; i++)
        {
            if(weaponDataSO.listWeapon[i].weaponType==weapontype)
            {
                return weaponDataSO.listWeapon[i];
            }
        }
        return null;
    }

}
