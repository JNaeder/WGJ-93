using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterItemGenerator : MonoBehaviour
{

    public GameObject underWaterItemPrefab;
    BoxCollider2D UWItemColl;

    public Transform sea;
    public int numOfTriggersAtATime = 3;

    public GameObject[] gens;

    float seaWidth;

    // Start is called before the first frame update
    void Start()
    {
        seaWidth = sea.localScale.x / 2;

        //CreateNewUnderWaterItems(numOfTriggersAtATime);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CreateNewUnderWaterItems(int prefabnum) {

        for (int i = 0; i < prefabnum; i++)
        {

            float maxBounds = seaWidth - (underWaterItemPrefab.GetComponent<BoxCollider2D>().bounds.size.x) / 2;
            float randomXNum = Random.Range(-maxBounds, maxBounds);
            Vector3 newPos = new Vector3(randomXNum, -1.0f, 0);
            GameObject newUW = Instantiate(underWaterItemPrefab, newPos, Quaternion.identity) as GameObject;
            newUW.transform.parent = transform;
            UWItemColl = underWaterItemPrefab.GetComponent<BoxCollider2D>();

        }
    }
}
