using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{

    public bool isFishing, isDrivingBoat;
    public float fishingBarSpeed;
    public float greenBarAreaSize = 150;

    public GameObject fishigUIBar, dialogBarUI;
    public Text dialogText;
    public RectTransform fishingBarMoving, fishingBarBar, fishingBarGreen;

    bool isBarMovingRight = true;
    bool movingPieceIsMoving;


    float greenAreaSize, movingBarSize, UIBarSize;
    float timePassed, pressedTime;

    public float uiBarDelay = 5f;

    public Dialog[] catchNothingDialog;
    public Dialog[] missedCatchDialog;


    CharacterMovement mainGuy;
    Fishing gM;
    Collectables collGM;
    DialogManager DM;


    // Start is called before the first frame update
    void Start()
    {


        SetUpFishingBar(fishingBarSpeed, greenBarAreaSize);
        fishigUIBar.SetActive(false);

        mainGuy = FindObjectOfType<CharacterMovement>();
        gM = FindObjectOfType<Fishing>();
        collGM = FindObjectOfType<Collectables>();
        DM = FindObjectOfType<DialogManager>();

        dialogBarUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFishing)
        {

            if (Time.time > pressedTime + uiBarDelay)
            {
                fishigUIBar.SetActive(true);
                //Debug.Log("Green size: " + greenAreaSize + " UIbarsize: " + UIBarSize);

                MoveMovingPiece();
                if (Input.GetButtonDown("Jump") && Time.time > (pressedTime + 0.5f))
                {
                    //Debug.Log(fishingBarMoving.anchoredPosition);
                    // work on this line for mini game
                    if (fishingBarMoving.anchoredPosition.x > (-greenAreaSize - movingBarSize) && fishingBarMoving.anchoredPosition.x < (greenAreaSize + movingBarSize))
                    {
                        dialogBarUI.SetActive(true);
                        mainGuy.currentUWItem.GetItem();
                        isFishing = false;
                        // StartCoroutine(EndFishing());

                    }
                    else
                    {
                        dialogBarUI.SetActive(true);
                        //StartCoroutine(EndFishing());
                        isFishing = false;
                        DM.StartDialog(missedCatchDialog, missedCatchDialog.Length);
                    }

                }


            }
        }



    }

    public void StartFishing(float distAway) {
        isFishing = true;
        pressedTime = Time.time;
        uiBarDelay = Random.Range(1.0f, 7.0f);
        Debug.Log(distAway);
        if (distAway < 0.5f)
        {
            //Slow
            float randomSpeed = Random.Range(550, 650);
            float randomGreenSize = Random.Range(75, 200);
            SetUpFishingBar(randomSpeed, randomGreenSize);
        }
        else if (distAway > 0.5f && distAway < 1.5f)
        {
            // med
            float randomSpeed = Random.Range(600, 800);
            float randomGreenSize = Random.Range(100, 150);
            SetUpFishingBar(randomSpeed, randomGreenSize);
        }
        else if (distAway > 1.5f && distAway < 3f) {
            //med fast
            float randomSpeed = Random.Range(800, 1000);
            float randomGreenSize = Random.Range(75, 200);
            SetUpFishingBar(randomSpeed, randomGreenSize);
        }
        else if (distAway > 3f)
        {
            //fast
            float randomSpeed = Random.Range(900, 1200);
            float randomGreenSize = Random.Range(100, 150);
            SetUpFishingBar(randomSpeed, randomGreenSize);
        }

    }

    public void FakeFishing() {
        StartCoroutine(FakeFishNum());
        


    }

    IEnumerator FakeFishNum() {
        uiBarDelay = 300;
        isFishing = true;
        yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
        isFishing = false;
        Debug.Log("Fake fishing!");
        dialogBarUI.SetActive(true);
        DM.StartDialog(catchNothingDialog, catchNothingDialog.Length);
        //EndFishing();
    }

   public void EndFishing() {
        isFishing = false;
       // yield return new WaitForSeconds(3);
        fishigUIBar.SetActive(false);
        mainGuy.isMoveable = true;
        dialogBarUI.SetActive(false);
        if (mainGuy.currentUWItem != null) {
            collGM.TurnOffItem();
            mainGuy.currentUWItem.TurnOffTrigger();
            mainGuy.currentUWItem = null;
        }

        if (collGM.collectedItems.Count == 5) {
            dialogBarUI.SetActive(true);
            dialogText.text = "You Win!";
        }
        

    }


    void MoveMovingPiece() {
            if (isBarMovingRight)
            {
                Vector3 movPos = fishingBarMoving.anchoredPosition;
                movPos.x += Time.deltaTime * fishingBarSpeed;
                fishingBarMoving.anchoredPosition = movPos;
            }
            else
            {
                Vector3 movPos = fishingBarMoving.anchoredPosition;
                movPos.x -= Time.deltaTime * fishingBarSpeed;
                fishingBarMoving.anchoredPosition = movPos;

            }


            if (fishingBarMoving.anchoredPosition.x > ((UIBarSize) - (movingBarSize)))
            {
                isBarMovingRight = false;


            }
            else if (fishingBarMoving.anchoredPosition.x < (-(UIBarSize) + (movingBarSize)))
            {

                isBarMovingRight = true;
            }
        

    }

    void SetUpFishingBar(float speed, float greenSize) {
        fishingBarGreen.sizeDelta = new Vector2(greenSize, fishingBarGreen.rect.height);
        fishingBarSpeed = speed;
        greenAreaSize = fishingBarGreen.rect.width / 2;
        movingBarSize = fishingBarMoving.rect.width / 2;
        UIBarSize = fishingBarBar.rect.width / 2;


    }
}
