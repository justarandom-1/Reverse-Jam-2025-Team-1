using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = PlayerController.instance.GetPosition().x;
        if(playerX - transform.position.x > 8.4F)
            transform.position = new Vector3(playerX - 8.4F, transform.position.y, transform.position.z);


        if(transform.position.x > 3 && transform.position.x < 19.9F){
            transform.position = new Vector3(transform.position.x + 10 * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}
