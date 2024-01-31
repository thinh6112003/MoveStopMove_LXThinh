using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class HatItemData 
{
    public HatType hatType;
    public Sprite sprite;
    public GameObject model;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public Vector3 scaleOffset;
    public int price;
}
public enum HatType
{
    ARROW,
    CAP,
    COWBOY,
    CROWN,
    EAR,
    HEADPHONE,
    HORN,
    POLICE,
    STRAW,
}