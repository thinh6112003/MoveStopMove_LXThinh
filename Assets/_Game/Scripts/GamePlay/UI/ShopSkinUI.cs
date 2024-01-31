using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopSkinUI : Singleton<ShopSkinUI>
{
    [SerializeField]private Button buttonHat;
    [SerializeField]private Button buttonShort;
    [SerializeField]private Button buttonAccessory;
    [SerializeField]private Button buttonFullSkin;
    [SerializeField]private Button selectButton;
    [SerializeField] private Button priceButton;
    [SerializeField] private Button buttonUnequipped;
    [SerializeField]private ShopItem template;
    [SerializeField]private Transform shopHatItemParent;
    [SerializeField]private Transform shopShortItemParent;
    [SerializeField]private Transform shopAccessoryItemParent;
    [SerializeField]private Transform shopFullSkinItemParent;
    [SerializeField] private GameObject hatSV;
    [SerializeField] private GameObject shortSV;
    [SerializeField] private GameObject accessorySV;
    [SerializeField] private GameObject fullSkinSV;
    [SerializeField]private List<int> currentType;
    [SerializeField]private TextMeshProUGUI priceText;  
    private SkinType currentSkinType;
    private ShopItem currentShopItem;
    private List<ShopItem> listHat= new List<ShopItem>();
    private List<ShopItem> listShort= new List<ShopItem>();
    private List<ShopItem> listAccessory= new List<ShopItem>();
    private List<ShopItem> listFullSkin= new List<ShopItem>();
    private void Awake()
    {
        buttonHat.onClick.AddListener(LoadHat);
        buttonShort.onClick.AddListener(LoadShort);
        buttonAccessory.onClick.AddListener(LoadAccessory);
        buttonFullSkin.onClick.AddListener(LoadFullSkin);
        selectButton.onClick.AddListener(SelectSkin);
        priceButton.onClick.AddListener(BuySkin);
    }
    public void SelectSkin()
    {
        switch (currentSkinType)
        {
            case SkinType.HAT:
                LevelManager.Instance.playerGamePlay.ChangeHat(currentType[(int)currentSkinType]);
                break;
            case SkinType.SHORT:
                LevelManager.Instance.playerGamePlay.ChangeShort(currentType[(int)currentSkinType]);
                break;
            case SkinType.ACCESSORY:
                LevelManager.Instance.playerGamePlay.ChangeShield(currentType[(int)currentSkinType]);
                break;
            case SkinType.FULLSKIN:
                LevelManager.Instance.ChangeFullSkin(currentType[(int)currentSkinType]);
                break;
        }
    }
    public void BuySkin()
    {
        if (currentShopItem.price <= DataManager.Instance.userData.currentCoint)
        {
        turnSelect();
        setBuyingStatus( (int)currentSkinType,currentType[(int)currentSkinType]);
        currentShopItem.SetLock(true);
        DataManager.Instance.userData.currentCoint -= currentShopItem.price;
        UIManager.Instance.loadCoin();
        DataManager.Instance.SetPlayerpref();
        }
    }
    public void OffShort()
    {
        Color color = buttonShort.GetComponent<Image>().color;
        color.a = 0.5f;
        buttonShort.GetComponent<Image>().color = color;
        shortSV.SetActive(false);
    }
    public void OffHat()
    {
        Color color = buttonHat.GetComponent<Image>().color;
        color.a = 0.5f;
        buttonHat.GetComponent<Image>().color = color;
        hatSV.SetActive(false);
    }
    public void OffAccessory()
    {
        Color color = buttonAccessory.GetComponent<Image>().color;
        color.a = 0.5f;
        buttonAccessory.GetComponent<Image>().color = color;
        accessorySV.SetActive(false);
    }
    public void OffFullSkin()
    {
        Color color = buttonFullSkin.GetComponent<Image>().color;
        color.a = 0.5f;
        buttonFullSkin.GetComponent<Image>().color = color;
        fullSkinSV.SetActive(false);
    }
    public void LoadHat()
    {
        OffShort();
        OffAccessory();
        OffFullSkin();
        hatSV.SetActive(true);
        Color color= buttonHat.GetComponent<Image>().color;
        color.a = 0;
        buttonHat.GetComponent<Image>().color = color;
        if (listHat.Count == 0) { 
            List<HatItemData> listHatData = DataManager.Instance.hatDataSO.listHat;
            for(int i = 0; i < listHatData.Count; i++)
            {
                ShopItem newHatItem = Instantiate(template, shopHatItemParent);
                newHatItem.SetLock(GetBuyingStatus(1, i));
                newHatItem.price = DataManager.Instance.hatDataSO.listHat[i].price;
                listHat.Add(newHatItem);
                newHatItem.name = "Hat" + i;
                GameObject sprite = newHatItem.transform.GetChild(0).gameObject;
                sprite.GetComponent<Image>().sprite = listHatData[i].sprite;
                ShopItem shopItem =newHatItem.GetComponent<ShopItem>();
                shopItem.myType = (int)listHatData[i].hatType;
                shopItem.mySkinKype = SkinType.HAT;
            }        
        }
        SetSelectItem((int)SkinType.HAT,0);
    }
    public void LoadShort()
    {
        OffHat();
        OffFullSkin();
        OffAccessory();
        shortSV.SetActive(true);
        Color color = buttonShort.GetComponent<Image>().color;
        color.a = 0;
        buttonShort.GetComponent<Image>().color = color;

        if (listShort.Count == 0)
        {
            List<ShortItemData> listShortData = DataManager.Instance.shortDataSO.listShort;
            for (int i = 0; i < listShortData.Count; i++)
            {
                ShopItem newShortItem = Instantiate(template, shopShortItemParent);
                newShortItem.SetLock(GetBuyingStatus(2,i));
                newShortItem.price = DataManager.Instance.shortDataSO.listShort[i].price;
                listShort.Add(newShortItem);
                GameObject sprite = newShortItem.transform.GetChild(0).gameObject;
                sprite.GetComponent<Image>().sprite = listShortData[i].sprite;
                ShopItem shopItem= newShortItem.GetComponent<ShopItem>();
                shopItem.myType =(int) listShortData[i].shortType;
                shopItem.mySkinKype = SkinType.SHORT;
            }
        }
        SetSelectItem( (int)SkinType.SHORT,0);
    }
    public void LoadAccessory()
    {
        OffHat();
        OffFullSkin();
        OffShort();
        accessorySV.SetActive(true);
        Color color = buttonAccessory.GetComponent<Image>().color;
        color.a = 0;
        buttonAccessory.GetComponent<Image>().color = color;

        if (listAccessory.Count == 0)
        {
            List<AccessoryItemData> listAccessoryData = DataManager.Instance.accessoryDataSO.listAccessory;
            for (int i = 0; i < listAccessoryData.Count; i++)
            {
                ShopItem newAccessoryItem = Instantiate(template, shopAccessoryItemParent);
                newAccessoryItem.SetLock(GetBuyingStatus(3, i));
                newAccessoryItem.price = DataManager.Instance.accessoryDataSO.listAccessory[i].price;
                listAccessory.Add(newAccessoryItem);
                GameObject sprite = newAccessoryItem.transform.GetChild(0).gameObject;
                sprite.GetComponent<Image>().sprite = listAccessoryData[i].sprite;
                ShopItem shopItem = newAccessoryItem.GetComponent<ShopItem>();
                shopItem.myType = (int)listAccessoryData[i].accessoryType;
                shopItem.mySkinKype = SkinType.ACCESSORY;
            }
        }
        SetSelectItem((int)SkinType.ACCESSORY, 0);
    }
    public void LoadFullSkin()
    {
        OffHat();
        OffAccessory();
        OffShort();
        fullSkinSV.SetActive(true);
        Color color = buttonFullSkin.GetComponent<Image>().color;
        color.a = 0;
        buttonFullSkin.GetComponent<Image>().color = color;

        if (listFullSkin.Count == 0)
        {
            List<FullSkinItemData> listFullSkinData = DataManager.Instance.fullSkinDataSO.listFullSkin;
            for (int i = 0; i < listFullSkinData.Count; i++)
            {
                ShopItem newFullSkinItem = Instantiate(template, shopFullSkinItemParent);
                newFullSkinItem.SetLock(GetBuyingStatus(4, i));
                newFullSkinItem.price = DataManager.Instance.fullSkinDataSO.listFullSkin[i].price;
                listFullSkin.Add(newFullSkinItem);
                GameObject sprite = newFullSkinItem.transform.GetChild(0).gameObject;
                sprite.GetComponent<Image>().sprite = listFullSkinData[i].sprite;
                ShopItem shopItem = newFullSkinItem.GetComponent<ShopItem>();
                shopItem.myType = (int)listFullSkinData[i].fullSkinType;
                shopItem.mySkinKype = SkinType.FULLSKIN;
            }
        }
            SetSelectItem((int)SkinType.FULLSKIN, 0);
    }
    public bool GetBuyingStatus(int i, int j)
    {
        switch (i)
        {
            case 1:
                return DataManager.Instance.userData.hatBuyingStatus[j];
            case 2:
                return DataManager.Instance.userData.shortBuyingStatus[j];
            case 3:
                return DataManager.Instance.userData.accessoryBuyingStatus[j];
            case 4:
                return DataManager.Instance.userData.fullSkinBuyingStatus[j];
        }
        return true;
    }
    public void setBuyingStatus(int i, int j)
    {
        switch (i)
        {
            case 1:
                DataManager.Instance.userData.hatBuyingStatus[j]= true;
                break;
            case 2:
                DataManager.Instance.userData.shortBuyingStatus[j]= true;
                break;
            case 3:
                DataManager.Instance.userData.accessoryBuyingStatus[j] = true;
                break;
            case 4:
                DataManager.Instance.userData.fullSkinBuyingStatus[j] = true;
                break;
        }
    }
    public void SetSelectItem(int skinType, int type)
    {
        currentSkinType = (SkinType)skinType;
        if(GetBuyingStatus(skinType,type) == true)
        {
            turnSelect();
        }
        else
        {
            turnPrice();
        }
        
        switch (skinType)
        {
            case 1:
                {
                    listHat[currentType[skinType]].GetComponent<Image>().color = Color.black;
                    listHat[type].GetComponent<Image>().color = Color.red;
                    currentType[skinType] = type;
                    currentShopItem = listHat[type];
                }
                break;
            case 2:
                {
                    listShort[currentType[skinType]].GetComponent<Image>().color = Color.black;
                    listShort[type].GetComponent<Image>().color = Color.red;
                    currentType[skinType] = type;
                    currentShopItem = listShort[type];
                }
                break;
            case 3:
                {
                    listAccessory[currentType[skinType]].GetComponent<Image>().color = Color.black;
                    listAccessory[type].GetComponent<Image>().color = Color.red;
                    currentType[skinType] = type;
                    currentShopItem = listAccessory[type];
                }
                break;
            case 4:
                {
                    listFullSkin[currentType[skinType]].GetComponent<Image>().color = Color.black;
                    listFullSkin[type].GetComponent<Image>().color = Color.red;
                    currentType[skinType] = type;
                    currentShopItem = listFullSkin[type];
                }
                break;
        }
        priceText.text = ""+currentShopItem.price;
    }
    void turnPrice()
    {
        priceButton.gameObject.SetActive(true);
        selectButton.gameObject.SetActive(false);
        buttonUnequipped.gameObject.SetActive(false);
    }
    void turnSelect()
    {
        priceButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(true);
        buttonUnequipped.gameObject.SetActive(false);
    }
    void turnEquipped()
    {
        priceButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);
        buttonUnequipped.gameObject.SetActive(true);
    }
}
public enum SkinType
{
    NONE,
    HAT,
    SHORT,
    ACCESSORY,
    FULLSKIN,
}
