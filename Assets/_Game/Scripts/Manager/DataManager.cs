using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataScriptableObject weaponDataSO;
    public int currentZone=1;
    public int hightScore=50;
    public int typeNumber;
    public DynamicData dynamicData;
    private void Start()
    {
        PlayerPrefs.DeleteKey(constr.PLAYERPREFKEY);
        string stringDD = PlayerPrefs.GetString(constr.PLAYERPREFKEY);
        dynamicData= JsonUtility.FromJson<DynamicData>(stringDD);
        if(dynamicData== null)
        {
            dynamicData = new DynamicData();
            dynamicData.buyingStatus = new bool[typeNumber];
            dynamicData.buyingStatus[0] = true;
            dynamicData.weaponType = WeaponType.HAMMER;
            for (int i = 1; i < typeNumber; i++) dynamicData.buyingStatus[i] = false;
            string dynamicDatastring = JsonUtility.ToJson(dynamicData);
        }
        //if(dynamicData!= null)
        //{
        //    PlayerPrefs.DeleteKey(constr.PLAYERPREFKEY);
        //}
    }
    public void SetPlayerpref()
    {
        string dynamicDatastring = JsonUtility.ToJson(dynamicData);
        PlayerPrefs.SetString(constr.PLAYERPREFKEY, dynamicDatastring);
    }
    public void GetPlayerpref()
    {
        string dynamicDataString = PlayerPrefs.GetString(constr.PLAYERPREFKEY);
        dynamicData = JsonUtility.FromJson<DynamicData>(dynamicDataString);
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
