using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{

    public GameObject[] possibleItems;
    public List<GameObject> collectedItems;
    public List<int> randNumList;
    public Image[] UICollImage;

    public Transform itemPickerPos;

    int indexNum;
    GameObject newItem;

    Fishing gM;
    UnderWaterItemGenerator UWGen;

    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<Fishing>();
        UWGen = FindObjectOfType<UnderWaterItemGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCollectable(GameObject newItem, int randomNumm) {
        collectedItems.Add(newItem);
        SpriteRenderer itemSpriteRend = newItem.GetComponent<SpriteRenderer>();
        UpdateUI(itemSpriteRend.sprite, randomNumm, itemSpriteRend.color);
        indexNum++;


    }


    void UpdateUI(Sprite itemSprite, int newIndexNum, Color newColor) {
        Image newImage = UICollImage[newIndexNum];
        newImage.sprite = itemSprite;
        newImage.color = newColor;
        newImage.gameObject.SetActive(true);

    }

    public GameObject GetRandomItem() {
        int randNum = Random.Range(0, possibleItems.Length);
        for(int i = 0; i < randNumList.Count; i++) {
            if (randNum == randNumList[i] && randNumList.Count != 5) {
               // Debug.Log("repeat!");
                GetRandomItem();
                return null;

            }
        }
        

        newItem = Instantiate(possibleItems[randNum], itemPickerPos.position, Quaternion.identity) as GameObject;
       // Debug.Log(newItem.name + " is collected!");
        ItemScript newItemScript = newItem.GetComponent<ItemScript>();
        newItemScript.itemPickedPos = itemPickerPos;
        if (newItemScript.isCollectable)
        {
            AddCollectable(newItem, randNum);
            randNumList.Add(randNum);
        }

        //gM.dialogText.text = newItemScript.collectedMessage;
        newItemScript.TriggerDialog();

        return newItem;
    }

    public void TurnOffItem() {
        if (newItem != null)
        {
            foreach (GameObject g in UWGen.gens) {
                g.SetActive(true);
            }

            newItem.SetActive(false);
        }

    }
}
