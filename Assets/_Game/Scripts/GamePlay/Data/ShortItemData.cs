using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ShortItemData
{
    public ShortType shortType;
    public Sprite sprite;
    public Material material;
    public int price;
}
public enum ShortType
{
    PANT1,
    PANT2,
    PANT3,
    PANT4,
    PANT5,
    PANT6,
    PANT7,
    PANT8,
    PANT9,
}