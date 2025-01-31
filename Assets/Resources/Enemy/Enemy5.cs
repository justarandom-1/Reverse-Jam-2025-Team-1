using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : Enemy
{
    new void Start()
    {
        base.Start();
        animator.SetBool("isMoving", true);
    }

    new protected void OnTriggerEnter2D (Collider2D other)
    {
        if(!isAlive)
            return;
        if((other.gameObject.CompareTag("BananaPeel") && animator.GetBool("isMoving")))
        {
            animator.Play("EnemyFall");
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
            isAlive = false;
            murderWeapon = PlayerController.instance.GetCurrentItem();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isAlive)
            return;

        if(collision.gameObject.CompareTag("Player")){
            SceneChanger.Home();
        }

    }

    new void Update()
    {
        base.Update();

        if(transform.position.y < -6 && isAlive){
            audioSource.Play();
            isAlive = false;
            isDead = true;
            GameObject.Find("Wall3").SetActive(false);
            PlayerController.instance.DepleteItem(Item.Axolotl);
        }

        if(isAlive){
            rb.velocity = new Vector2(direction * 2F, rb.velocity.y);

        }else if(!isDead && animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead")){
            audioSource.Play();
            isDead = true;
            if(murderWeapon != Item.None)
                PlayerController.instance.DepleteItem(murderWeapon);
        }
    }
}
