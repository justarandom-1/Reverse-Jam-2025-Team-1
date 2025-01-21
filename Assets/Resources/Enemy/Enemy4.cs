using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
    new void Start()
    {
        base.Start();
        animator.SetBool("isMoving", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isAlive)
            return;
        if (collision.gameObject.CompareTag("Cage"))
        {
            GetComponent<Collider2D>().enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            audioSource.Play();
            isAlive = false;
            isDead = true;
            GameObject.Find("Wall4").SetActive(false);
        }

        if((collision.gameObject.CompareTag("Platform") && direction == -1) || (collision.gameObject.CompareTag("Wall") && direction == 1))
            direction *= -1;

        if(collision.gameObject.CompareTag("Dog")){
            animator.Play("EnemyFall");
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
            isAlive = false;
            murderWeapon = Item.Key;
        }

    }

    new void Update()
    {
        base.Update();

        if(isAlive){
            rb.velocity = new Vector2(direction * 1F, rb.velocity.y);

        }else if(!isDead && animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead")){
            audioSource.Play();
            isDead = true;
            GameObject.Find("Wall4").SetActive(false);
            if(murderWeapon != Item.None)
                PlayerController.instance.DepleteItem(murderWeapon);
        }
    }
}
