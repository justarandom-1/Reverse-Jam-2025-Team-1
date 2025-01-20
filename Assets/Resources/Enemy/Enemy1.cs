using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    protected float direction;
    protected AudioSource audioSource;
    protected Rigidbody2D rb;
    protected Animator animator;

    protected bool isAlive;
    protected bool isDead;
    protected Item murderWeapon;
    // Start is called before the first frame update
    protected void Start()
    {
        direction = transform.localScale.x / Mathf.Abs(transform.localScale.x);

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    protected void Update()
    {
        if(direction * transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    
}
public class Enemy1 : Enemy
{
    new void Start()
    {
        base.Start();
        animator.SetBool("isMoving", true);
    }
    public void OnTriggerEnter2D (Collider2D other)
    {
        if(!isAlive)
            return;
        if(other.gameObject.CompareTag("Hammer") || other.gameObject.CompareTag("Scissors") || other.gameObject.CompareTag("BananaPeel")){
            animator.Play("EnemyFall");
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
            isAlive = false;
            murderWeapon = PlayerController.instance.GetCurrentItem();
        }
    }

    new void Update()
    {
        base.Update();

        if(isAlive){
            rb.velocity = new Vector2(direction * 1F, rb.velocity.y);

            if((transform.position.x < 0 && direction == -1) || (transform.position.x > 7 && direction == 1))
                direction *= -1;
        }else if(!isDead && animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead")){
            isDead = true;
            GameObject.Find("Wall1").SetActive(false);
            PlayerController.instance.DepleteItem(murderWeapon);
        }
    }
}
