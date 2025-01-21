using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{

    [SerializeField] GameObject Object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.CompareTag("Scissors")){
            Object.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            PlayerController.instance.DepleteItem(Item.Scissors);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
