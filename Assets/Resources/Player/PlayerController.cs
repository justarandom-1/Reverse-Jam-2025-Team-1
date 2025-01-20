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
    Axolotl
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
    private Transform cameraTransform;
    private GameObject deployedItem;
    private Object BananaPeel;
    private Object Axolotl;


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

        cameraTransform = GameObject.Find("Main Camera").transform;

        BananaPeel = Resources.Load("Toolbar/BananaPeel") as GameObject;
        Axolotl = Resources.Load("Toolbar/Axolotl") as GameObject;
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
            audioSource.Stop();
            return;
        }

        animator.SetBool("isMoving", true);

        if(movementVector.y > 0 && jumpsFromGround < 1){
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
            case Item.Scissors:
            case Item.Key:
                transform.GetChild((int) currentItem - 1).GetComponent<Animator>().Play("" + (int) currentItem);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(movementVector.x == -1 && cameraTransform.position.x - transform.position.x >= 8.4F)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else   
            rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);

        if(movementVector.x != 0 && !audioSource.isPlaying)    
            audioSource.Play();
    }

    private void UnequipCurrentItem(){
        if(currentItem != 0 && transform.childCount >= (int)currentItem)
            transform.GetChild((int) currentItem - 1).GetComponent<Animator>().Play("Unused");
        if(deployedItem != null)
            Destroy(deployedItem);
    }

    public bool Equip(Item newItem){
        for(int i = 0; i < usedItems.Count; i++){
            if(usedItems[i] == newItem){
                return false;
            }
        }

        if((int) currentItem > 0 && (int) currentItem < 4 && !transform.GetChild((int) currentItem - 1).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return false;

        UnequipCurrentItem();

        currentItem = newItem;

        switch(currentItem){
            case Item.Hammer:
            case Item.Scissors:
            case Item.Key:
                transform.GetChild((int) currentItem - 1).GetComponent<Animator>().Play("Idle");
                break;
            case Item.BananaPeel:
                deployedItem = Instantiate(BananaPeel, new Vector3(transform.position.x + 0.5F * GetDirection(), transform.position.y, transform.position.z - 0.1F), Quaternion.identity) as GameObject;
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), deployedItem.GetComponent<BoxCollider2D>());
                break;
            case Item.Axolotl:
                deployedItem = Instantiate(Axolotl, new Vector3(transform.position.x + 0.5F * GetDirection(), transform.position.y, transform.position.z - 0.1F), Quaternion.identity) as GameObject;
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), deployedItem.GetComponent<Collider2D>());
                break;
        }

        return true;
    }

    public void DepleteItem(Item item){
        if(item == Item.None)
            return;

        if(item == currentItem){
            UnequipCurrentItem();
            currentItem = Item.None;
        }

        Toolbar.instance.DepleteItem(item);
        usedItems.Add(item);
    }

    public Item GetCurrentItem(){
        return currentItem;
    }

    public float GetDirection(){
        return transform.localScale.x / Mathf.Abs(transform.localScale.x);
    }

    public Vector2 GetPosition(){
        return transform.position;
    }
}
