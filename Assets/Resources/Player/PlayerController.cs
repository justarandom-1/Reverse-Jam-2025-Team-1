using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public enum Item{
    None,
    Hammer,
    Scissors,
    Key,
    BananaPeel,
    Axolotl,
}
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] Vector2 movementVector;
    private Rigidbody2D rb;
    [SerializeField] int jumpsFromGround;

    private AudioSource audioSource;
    private AudioClip moveSFX;
    private Animator animator;
    [SerializeField] int speed;
    [SerializeField] int jumpForce;
    private Item currentItem;
    private List<Item> usedItems;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        jumpsFromGround = 0;

        moveSFX = Resources.Load <AudioClip> ("Player/shorterwalksound");

        currentItem = Item.None;
        usedItems = new List<Item>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsFromGround = 0;
        }
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();

        if(movementVector.x == 0 && movementVector.y == 0){
            animator.SetBool("isMoving", false);
            return;
        }

        animator.SetBool("isMoving", true);

        if(movementVector.y > 0 && jumpsFromGround < 2){
            jumpsFromGround++;
            rb.AddForce(new Vector2(0, jumpForce));
        }

        if(movementVector.x * transform.localScale.x < 0){
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

    }

    void OnItemUse(){
        switch(currentItem){
            case Item.Hammer:
                transform.GetChild((int) currentItem - 1).GetComponent<Animator>().Play("UseHammer");
                break;
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);

        if(movementVector.x != 0 && !audioSource.isPlaying)    
            audioSource.PlayOneShot(moveSFX);
    }

    public bool Equip(Item newItem){
        for(int i = 0; i < usedItems.Count; i++){
            if(usedItems[i] == newItem){
                return false;
            }
        }

        if(currentItem != 0){
            transform.GetChild((int) currentItem - 1).GetComponent<Animator>().Play("Unused");
        }

        currentItem = newItem;

        if(transform.GetChild((int) currentItem - 1) != null){
            transform.GetChild((int) currentItem - 1).GetComponent<Animator>().Play("Idle");
        }

        return true;
    }

    public Item GetCurrentItem(){
        return currentItem;
    }
}
