using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
     private Queue<string> sentences;
    Dialog[] currentDialog;

    Fishing gM;
    CharacterMovement mainGuy;

    public Text dialogTextBox;
    public Image dialogImage;
    public GameObject arrowKeysControlUI, spacebarControlsUI;

    public int howManyTurns = 0;
    int indexNum = 0;


    public bool isCutscene, isFirstDialog, isSecondDialog;
    public int nextSceneToLoad;
    LevelManager lM;


    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<Fishing>();
        mainGuy = FindObjectOfType<CharacterMovement>();
        lM = FindObjectOfType<LevelManager>();

        sentences = new Queue<string>();


        if (arrowKeysControlUI != null)
        {
            arrowKeysControlUI.SetActive(false);
            spacebarControlsUI.SetActive(false);
        }
    }


    private void Update()
    {
        if (!mainGuy.isMoveable || isCutscene)
        {
            if (Input.GetButtonDown("Submit"))
            {
                DisplayNextSentence();

            }
        }
    }



    public void StartDialog(Dialog[] dialog, int turnNum) {
        //Debug.Log("Starting Convo with " + dialog.name);
        
            howManyTurns = turnNum;
            indexNum = 0;
            currentDialog = dialog;
            sentences.Clear();
            dialogImage.sprite = dialog[indexNum].characterSprite;
        dialog[indexNum].PlayDialogSound();

            foreach (string sentence in dialog[indexNum].sentences)
            {
                sentences.Enqueue(sentence);

            }

            DisplayNextSentence();
            
    }


    public void StartNextTurn(Dialog[] dialog, int i) {
        sentences.Clear();
      dialogImage.sprite = dialog[indexNum].characterSprite;
        dialog[indexNum].PlayDialogSound();
        foreach (string sentence in dialog[i].sentences)
        {
            sentences.Enqueue(sentence);

        }

        DisplayNextSentence();

    }


    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            howManyTurns--;
            indexNum++;
            if (howManyTurns == 0)
            {
                EndDialog();
                return;
            }
            else {
                StartNextTurn(currentDialog, indexNum);
                return;
            }

        }
        if (sentences.Count != 0)
        {
            string sentence = sentences.Dequeue();
            dialogTextBox.text = sentence;
        }


    }


    public void EndDialog() {
        // Debug.Log("EndConvo");
        if (gM != null && !isFirstDialog && !isSecondDialog)
        {
            gM.EndFishing();
        }
        indexNum = 0;
        if (isCutscene) {
            lM.LoadLevel(nextSceneToLoad);
          } else if (isSecondDialog) {
            mainGuy.isMoveable = true;
            gM.dialogBarUI.SetActive(false);
            arrowKeysControlUI.SetActive(false);
            isFirstDialog = false;
        }
        else if (isFirstDialog) {
            mainGuy.isMoveable = true;
            gM.dialogBarUI.SetActive(false);
            arrowKeysControlUI.SetActive(true);
            isFirstDialog = false;
            isSecondDialog = true;
            }
    }

    
}
