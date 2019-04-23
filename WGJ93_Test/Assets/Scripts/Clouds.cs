using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{

    public float minSpeed, maxSpeed;
    float speed;

    CloudGenerator cG;

    // Start is called before the first frame update
    void Start()
    {
        cG = FindObjectOfType<CloudGenerator>();


        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;

        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CloudDestroyer")
        {
            cG.GenerateNewCloud();
            Destroy(gameObject);
        }
    }
}
