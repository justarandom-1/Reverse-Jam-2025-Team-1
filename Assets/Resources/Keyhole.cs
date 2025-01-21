using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    [SerializeField] GameObject Removeable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.CompareTag("Key")){
            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Toolbar/keyunlock"));
            Removeable.SetActive(false);
            gameObject.SetActive(false);
            PlayerController.instance.DepleteItem(Item.Key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
