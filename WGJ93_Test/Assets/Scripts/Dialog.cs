using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[System.Serializable]
public class Dialog 
{
    public string name;
    [TextArea(4,10)]
    public string[] sentences;
    public Sprite characterSprite;
    [FMODUnity.EventRef]
    public string dialogSound;



    public void PlayDialogSound() {
        FMODUnity.RuntimeManager.PlayOneShot(dialogSound);
     
       }

}
