using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class Fishing : MonoBehaviour
{



    public bool isFishing, isDrivingBoat;
    public float fishingBarSpeed;
    public float greenBarAreaSize = 150;

    public GameObject fishigUIBar, dialogBarUI;
    public Text dialogText;
    public RectTransform fishingBarMoving, fishingBarBar, fishingBarGreen;

    public float maxBarDist, minBarDist, avgBarSpeed;

    bool isBarMovingRight = true;
    bool movingPieceIsMoving;


    float greenAreaSize, movingBarSize, UIBarSize;
    float timePassed, pressedTime;

    public float uiBarDelay = 5f;

    public Dialog[] catchNothingDialog;
    public Dialog[] missedCatchDialog;


    [FMODUnity.EventRef]
    public string startFishGame, pressBarButton, fishGameMovingSound;

    bool hasPlayedFishGameSound = false;


    CharacterMovement mainGuy;
    Fishing gM;
    Collectables collGM;
    DialogManager DM;
    LevelManager lM;


    // Start is called before the first frame update
    void Start()
    {


        //SetUpFishingBar(fishingBarSpeed, greenBarAreaSize);
        if (fishigUIBar != null)
        {
            fishigUIBar.SetActive(false);
        }

        mainGuy = FindObjectOfType<CharacterMovement>();
        gM = FindObjectOfType<Fishing>();
        collGM = FindObjectOfType<Collectables>();
        DM = FindObjectOfType<DialogManager>();
        lM = FindObjectOfType<LevelManager>();

        if (dialogBarUI != null)
        {
            dialogBarUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFishing)
        {

            if (Time.time > pressedTime + uiBarDelay)
            {
                if (!hasPlayedFishGameSound) {
                    hasPlayedFishGameSound = true;
                    FMODUnity.RuntimeManager.PlayOneShot(startFishGame);

                }
                fishigUIBar.SetActive(true);
                
                //Debug.Log("Green size: " + greenAreaSize + " UIbarsize: " + UIBarSize);



                MoveMovingPiece();
                if (Input.GetButtonDown("Jump") && Time.time > (pressedTime + 0.5f))
                {
                    FMODUnity.RuntimeManager.PlayOneShot(pressBarButton);
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
        //Debug.Log(distAway);
        if (distAway < 0.5f)
        {
            //Slow
            float randomSpeed = Random.Range(avgBarSpeed * 0.8f, avgBarSpeed * 0.9f);
            float randomGreenSize = Random.Range(minBarDist, maxBarDist + (maxBarDist * 0.6f));
            SetUpFishingBar(randomSpeed, randomGreenSize);
        }
        else if (distAway > 0.5f && distAway < 1.5f)
        {
            // med
            float randomSpeed = Random.Range(avgBarSpeed *0.9f, avgBarSpeed);
            float randomGreenSize = Random.Range(maxBarDist + (maxBarDist * 0.6f), maxBarDist - (maxBarDist * 0.4f));
            SetUpFishingBar(randomSpeed, randomGreenSize);
        }
        else if (distAway > 1.5f && distAway < 2f) {
            //med fast
            float randomSpeed = Random.Range(avgBarSpeed, avgBarSpeed * 1.2f);
            float randomGreenSize = Random.Range(maxBarDist - (maxBarDist * 0.6f), maxBarDist - (maxBarDist * 0.4f));
            SetUpFishingBar(randomSpeed, randomGreenSize);
        }
        else if (distAway > 2f)
        {
            //fast
            float randomSpeed = Random.Range(avgBarSpeed * 1.2f, avgBarSpeed * 1.4f);
            float randomGreenSize = Random.Range(maxBarDist - (maxBarDist * 0.4f), maxBarDist);
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
       //Debug.Log("Fake fishing!");
        dialogBarUI.SetActive(true);
        DM.StartDialog(catchNothingDialog, catchNothingDialog.Length);
        //EndFishing();
    }

   public void EndFishing() {
        hasPlayedFishGameSound = false;
        isFishing = false;

        if (fishigUIBar != null)
        {
            fishigUIBar.SetActive(false);
            mainGuy.isMoveable = true;
            dialogBarUI.SetActive(false);
        }
        if (mainGuy.currentUWItem != null) {
            collGM.TurnOffItem();
            mainGuy.currentUWItem.TurnOffTrigger();
            mainGuy.currentUWItem = null;
        }

        //WIN!!!
        if (collGM.collectedItems.Count == collGM.UICollImage.Length) {
            //dialogBarUI.SetActive(true);
            //dialogText.text = "You Win!";
            lM.LoadLevel(4);


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
                FMODUnity.RuntimeManager.PlayOneShot(fishGameMovingSound);


        }
            else if (fishingBarMoving.anchoredPosition.x < (-(UIBarSize) + (movingBarSize)))
            {

                isBarMovingRight = true;
            FMODUnity.RuntimeManager.PlayOneShot(fishGameMovingSound);
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
