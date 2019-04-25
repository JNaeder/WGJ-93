using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecThenLoadScene : MonoBehaviour
{


    public float secondsToWait = 3f;
    public int nextSceneToLoad = 1;

    LevelManager lM;

    // Start is called before the first frame update
    void Start()
    {
        lM = FindObjectOfType<LevelManager>();

        StartCoroutine(WaitForATime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForATime() {


        yield return new WaitForSeconds(secondsToWait);

        lM.LoadLevel(nextSceneToLoad);

       }
}
