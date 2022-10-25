using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Order",menuName = "Order Type")]
public class Orders: ScriptableObject
{
    public Sprite order1Image;
    public string order1Name;
    public int order1Count=1;
    public string order2Name;
    public Sprite order2Image;
    public int order2Count=1;
    public Sprite order3Image;
    public string order3Name;
    public int order3Count=1;
    
    public String orderName = "=Soup";
    public Sprite orderImage;
}
