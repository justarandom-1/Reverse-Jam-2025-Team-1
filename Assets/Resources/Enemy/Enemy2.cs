using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{

    new void Start()
    {
        base.Start();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isAlive)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>());
            audioSource.Play();
            animator.Play("EnemyDead");
            isAlive = false;
            isDead = true;
            GameObject.Find("Wall2").SetActive(false);
        }
    }

    new void Update()
    {
        base.Update();

        if(!isDead && animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead")){
            audioSource.Play();
            isDead = true;
            GameObject.Find("Wall2").SetActive(false);
            if(murderWeapon != Item.None)
                PlayerController.instance.DepleteItem(murderWeapon);
        }
    }
}
