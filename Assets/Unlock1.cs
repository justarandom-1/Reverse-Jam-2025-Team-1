using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.CompareTag("Hammer")){
            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Toolbar/hammersound"));
            GameObject.Find("Platform1").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GameObject.Find("Platform1").GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GameObject.Find("Enemy2").GetComponent<Collider2D>());
            Destroy(GameObject.Find("Door1"));
            Destroy(GameObject.Find("Keyhole1"));
            gameObject.SetActive(false);
            PlayerController.instance.DepleteItem(Item.Hammer);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
