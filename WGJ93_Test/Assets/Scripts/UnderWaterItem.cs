using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterItem : MonoBehaviour
{

    
    public float distanceFromPlayer;
    public Transform itemPickerPos;

    CharacterMovement player;
    GameObject pickedItem;
    Collectables collGM;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
        collGM = FindObjectOfType<Collectables>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        // Debug.Log(distanceFromPlayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            //Debug.Log("Something Here!");
        }
    }

    public void GetItem() {
        collGM.GetRandomItem();
       

    }

    public void TurnOffTrigger()
    {
        gameObject.SetActive(false);

    }

    public void DestroyItem() {
        //pickedItem.SetActive(false);

    }

}
