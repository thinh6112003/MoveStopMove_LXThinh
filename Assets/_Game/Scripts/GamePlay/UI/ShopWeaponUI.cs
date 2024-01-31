using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopWeaponUI : Singleton<ShopWeaponUI>
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI nameWeaponText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button priceButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Image equippedImage;
    private int typeNumber;
    private WeaponDataSO weaponDataSO;
    private WeaponItemData weaponData;
    private int current;
    private Weapon weapon;
    private WeaponType currentWeaponTypeInShopWeapon;
    private void Awake()
    {
        typeNumber = DataManager.Instance.weaponDataSO.listWeapon.Count;
        weaponDataSO = DataManager.Instance.weaponDataSO;
        selectButton.onClick.AddListener(SelectWeapon);
        priceButton.onClick.AddListener(BuyWeapon);
        nextButton.onClick.AddListener(NextWeapon);
        backButton.onClick.AddListener(BackWeapon);
    }
    public void SelectWeapon()
    {
        LevelManager.Instance.playerGamePlay.ChangeWeapon((WeaponType)current);
        DataManager.Instance.userData.currentWeaponType = (WeaponType)current;
        turnEquipped();
        DataManager.Instance.SetPlayerpref();
    }
    public void BuyWeapon()
    {
        if(DataManager.Instance.userData.currentCoint>= weaponDataSO.listWeapon[current].price)
        {
            DataManager.Instance.userData.currentCoint -= weaponDataSO.listWeapon[current].price;
            UIManager.Instance.loadCoin();
            DataManager.Instance.userData.weaponBuyingStatus[current] = true;
            turnSelect();
            DataManager.Instance.SetPlayerpref();
        }
    }
    public void LoadStart()
    {
        WeaponItemData weaponItemData= 
            LevelManager.Instance.playerGamePlay.weaponData;
        currentWeaponTypeInShopWeapon = weaponItemData.weaponType;
        nameWeaponText.text = weaponItemData.name;
        weapon= Instantiate(weaponItemData.weapon, mainCamera);
        current = (int)weaponItemData.weaponType;
        weapon.transform.localPosition = new Vector3(0, 0, 5);
        weapon.transform.localRotation = Quaternion.Euler(
            DataManager.Instance.weaponDataSO.listWeapon[current].rotationOffsetShop);
    }
    public void DestroyWeapon()
    {
        Destroy(weapon.gameObject);
    }
    void NextWeapon()
    {
        current= (int)currentWeaponTypeInShopWeapon;
        current++;
        if (current == typeNumber) current = 0;
        if (current == (int)LevelManager.Instance.playerGamePlay.weaponData.weaponType)
        {
            turnEquipped();
        }
        else
        {
            if (DataManager.Instance.userData.weaponBuyingStatus[current]) turnSelect();
            else turnPrice();
        }
        Destroy(weapon.gameObject);
        weaponData=DataManager.Instance.weaponDataSO.listWeapon[current];
        nameWeaponText.text = weaponData.name;
        priceText.text =""+weaponData.price;
        currentWeaponTypeInShopWeapon = (WeaponType) current;
        weapon = Instantiate(weaponData.weapon, mainCamera);
        weapon.transform.localPosition = new Vector3(0, 0, 5);
        weapon.transform.localRotation = Quaternion.Euler(
            DataManager.Instance.weaponDataSO.listWeapon[current].rotationOffsetShop);
    }
    void BackWeapon()
    {
        current = (int)currentWeaponTypeInShopWeapon;
        current--;
        if (current == -1) current = typeNumber - 1;
        if (current == (int)LevelManager.Instance.playerGamePlay.weaponData.weaponType)
        {
            turnEquipped();
        }
        else
        {
            if (DataManager.Instance.userData.weaponBuyingStatus[current]) turnSelect();
            else turnPrice();
        }
        Destroy(weapon.gameObject);
        weaponData = DataManager.Instance.weaponDataSO.listWeapon[current];
        nameWeaponText.text = weaponData.name;
        priceText.text = "" + weaponData.price;
        currentWeaponTypeInShopWeapon = (WeaponType)current;
        weapon = Instantiate(weaponData.weapon, mainCamera);
        weapon.transform.localPosition = new Vector3(0, 0, 5);
        weapon.transform.localRotation = Quaternion.Euler(
            DataManager.Instance.weaponDataSO.listWeapon[current].rotationOffsetShop);
    }
    void turnPrice()
    {
        priceButton.gameObject.SetActive(true);
        selectButton.gameObject.SetActive(false);
        equippedImage.gameObject.SetActive(false);
    }
    void turnSelect()
    {
        priceButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(true);
        equippedImage.gameObject.SetActive(false);
    }
    void turnEquipped()
    {
        priceButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);
        equippedImage.gameObject.SetActive(true);
    }
}
