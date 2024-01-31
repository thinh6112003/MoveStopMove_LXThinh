using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public int myType;
    public SkinType mySkinKype;
    public GameObject lockItem;
    public int price;
    public void OnPointerClick(PointerEventData eventData)
    {
        ShopSkinUI.Instance.SetSelectItem((int)mySkinKype,myType);
    }
    public void SetLock(bool isBuy)
    {
        if (isBuy) lockItem.SetActive(false);
        else lockItem.SetActive(true);
    }
}
