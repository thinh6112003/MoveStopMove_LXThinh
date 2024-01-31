using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class FullSkinItemData 
{
    public FullSkinType fullSkinType;
    public Sprite sprite;
    public Player model;
    public int price;
}
public enum FullSkinType
{
    ANGLE,
    DEADPOOL,
    DEAVIL,
    THOR,
    WITCH,
}
