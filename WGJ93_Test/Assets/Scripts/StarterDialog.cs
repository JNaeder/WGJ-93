using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterDialog : MonoBehaviour
{
    public Dialog[] dialog;


    DialogManager dM;
    CharacterMovement mainGuy;
    Fishing gM;
    // Start is called before the first frame update
    void Start()
    {
        dM = FindObjectOfType<DialogManager>();
        mainGuy = FindObjectOfType<CharacterMovement>();
        gM = FindObjectOfType<Fishing>();

        dM.StartDialog(dialog, dialog.Length);
        if(mainGuy != null) {
            mainGuy.isMoveable = false;
            if (gM.dialogBarUI != null)
            {
                gM.dialogBarUI.SetActive(true);
            }
          }
    }
}
