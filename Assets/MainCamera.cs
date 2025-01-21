using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    bool hasReachedFinal;
    void Start()
    {
        transform.position = new Vector3(0, 0, -10);
        hasReachedFinal = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = PlayerController.instance.GetPosition();
        if(playerPosition.y < -4)
            return;
        float playerX = playerPosition.x;
        if(playerX - transform.position.x > 8.4F)
            transform.position = new Vector3(playerX - 8.4F, transform.position.y, transform.position.z);


        if((transform.position.x > 3 && transform.position.x < 19.9F) || (transform.position.x > 22.9 && transform.position.x < 39.8F) || (transform.position.x > 42.8 && transform.position.x < 59.7F) || (transform.position.x > 62.7 && transform.position.x < 79.8F)){
            transform.position = new Vector3(transform.position.x + 10 * Time.deltaTime, transform.position.y, transform.position.z);
        }

        if(!hasReachedFinal && transform.position.x >= 79.8F){
            GameObject.Find("Wall5").GetComponent<Animator>().Play("WallRaise");
        }
    }
}
