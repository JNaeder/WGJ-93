using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog 
{
    public string name;
    [TextArea(4,10)]
    public string[] sentences;
    public Sprite characterSprite;
    public Color characterColor;
    
}
