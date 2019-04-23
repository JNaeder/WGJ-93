using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CharacterMovement : MonoBehaviour
{

    public float speed = 5f;
    public Transform fishingLine;

    Vector3 fishingLineScale;

    public bool  isMoveable;

    public Animator guyAnim;
    public SpriteRenderer sensorSP;
    Color sensorStartColor;

    Animator anim;
    Fishing gM;

    public UnderWaterItem currentUWItem;
    float distanceFromItem;
    float lastFrameSpeed = 0;

    [FMODUnity.EventRef]
    public string boatMoving, boatChange, dropLine, pullUpLine, sensorLight;

    FMOD.Studio.EventInstance boatMovingInst, sensorLightInst;



    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<Fishing>();
        anim = GetComponent<Animator>();

        fishingLineScale = fishingLine.localScale;
        fishingLineScale.y = 0;
        fishingLine.localScale = fishingLineScale;

        sensorStartColor = sensorSP.color;

        boatMovingInst = FMODUnity.RuntimeManager.CreateInstance(boatMoving);
        boatMovingInst.setParameterValue("h", 0);
        boatMovingInst.start();

        sensorLightInst = FMODUnity.RuntimeManager.CreateInstance(sensorLight);
        sensorLightInst.setParameterValue("dist", 10);
        sensorLightInst.start();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        UpdateAnim();
        SetSensorColor();

        if (!gM.isFishing)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ThrowFishingRod();
            }
        }
    }


    void Movement() {
        if (isMoveable)
        {

            float h = Input.GetAxis("Horizontal");
            transform.position += new Vector3(h, 0, 0) * Time.deltaTime * speed;

            anim.SetFloat("h", h);
            boatMovingInst.setParameterValue("h", Mathf.Abs(h));

            if (lastFrameSpeed == 0) {
                if (Mathf.Abs(h) != 0) {
                    FMODUnity.RuntimeManager.PlayOneShot(boatChange);
                }

            }

            lastFrameSpeed = h;
        }
    }


    void ThrowFishingRod() {

        // Debug.Log("Throw Fishing Rod");
        if (currentUWItem != null)
        {
            //Debug.Log("StartFishing at " + currentUWItem.distanceFromPlayer + " feet");
            gM.StartFishing(distanceFromItem);
        }
        else {
            gM.FakeFishing();
            //Debug.Log("Fake fishing!");

        }

        
        isMoveable = false;
        FMODUnity.RuntimeManager.PlayOneShot(dropLine);

    }

    void UpdateAnim() {
        guyAnim.SetBool("isFishing", gM.isFishing);
    }

    void SetSensorColor() {
        if (currentUWItem != null)
        {
            distanceFromItem = Vector3.Distance(transform.position, currentUWItem.transform.position);
            float widthOfUWItem = (currentUWItem.GetComponent<Collider2D>().bounds.size.x) / 2;
            float distPerc = (widthOfUWItem/ distanceFromItem)/ widthOfUWItem;
            sensorSP.color = new Color(distPerc, sensorStartColor.g, sensorStartColor.b);

            if (!gM.isFishing && isMoveable)
            {
                sensorLightInst.setParameterValue("dist", distanceFromItem);
            }
            else {
                sensorLightInst.setParameterValue("dist", 10);
            }
        }
        else {
            sensorSP.color = sensorStartColor;
            sensorLightInst.setParameterValue("dist", 10);

        }


    }

    public void PlayPullUpSound() {
        FMODUnity.RuntimeManager.PlayOneShot(pullUpLine);

    }


   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UnderwaterItem")
        {
            currentUWItem = collision.gameObject.GetComponent<UnderWaterItem>();
            //Debug.Log("Something Here!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UnderwaterItem")
        {
            currentUWItem = null;
            //Debug.Log("Leaving");
        }
    }


}
