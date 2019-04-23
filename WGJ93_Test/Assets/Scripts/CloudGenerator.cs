using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{

    public GameObject[] cloudPrefabs;
    public Transform cloudTrans;
    public Transform[] cloudGenPoints;

   

    public void GenerateNewCloud() {

        int randomNum = Random.Range(0, cloudPrefabs.Length);
        float yPos = Random.Range(cloudGenPoints[0].position.y, cloudGenPoints[1].position.y);

        GameObject newCloud = Instantiate(cloudPrefabs[randomNum], new Vector3(cloudGenPoints[0].position.x, yPos, 0), Quaternion.identity) as GameObject;
        newCloud.transform.parent = cloudTrans;
      
        }
}
