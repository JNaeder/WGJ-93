using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<Fishing>();
        anim = GetComponent<Animator>();

        fishingLineScale = fishingLine.localScale;
        fishingLineScale.y = 0;
        fishingLine.localScale = fishingLineScale;

        sensorStartColor = sensorSP.color;
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
            Debug.Log("Fake fishing!");

        }

        
        isMoveable = false;
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
        }
        else {
            sensorSP.color = sensorStartColor;

        }


    }


   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentUWItem = collision.gameObject.GetComponent<UnderWaterItem>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentUWItem = null;
    }


}
