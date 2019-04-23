using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ItemScript : MonoBehaviour
{
    public Transform itemPickedPos;

    public Dialog[] dialog;

    public bool isCollectable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = itemPickedPos.position;

    }

    public void TriggerDialog() {
        FindObjectOfType<DialogManager>().StartDialog(dialog, dialog.Length);
    }
    
}
