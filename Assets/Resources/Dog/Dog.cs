using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] float speed;
    protected float direction;
    protected AudioSource audioSource;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Item murderWeapon;

    public int stage;
    protected GameObject cage;
    protected GameObject target;
    // Start is called before the first frame update
    protected void Start()
    {
        direction = transform.localScale.x / Mathf.Abs(transform.localScale.x);

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        cage = GameObject.Find("Cage");
        target = GameObject.Find("Enemy4");

        stage = 0;
    }

    protected void Update()
    {
        if(stage == 0 && !cage.activeInHierarchy){
            stage = 1;
            animator.SetBool("isMoving", true);
        }

        if(stage == 1 || stage == 2){
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }

        if(stage == 2){
            float distance = target.transform.position.x - transform.position.x;
            direction = distance/Mathf.Abs(distance);
        }

        if(direction * transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(stage == 1 && collision.gameObject.CompareTag("Ground"))
            stage = 2;
        if(collision.gameObject.CompareTag("Enemy")){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            stage = 3;
            animator.SetBool("isMoving", false);
            rb.velocity = new Vector2(0, 0);
        }
    }
}
