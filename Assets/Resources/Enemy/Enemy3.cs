using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{

    new void Start()
    {
        base.Start();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && isAlive)
        {
            GetComponent<Collider2D>().enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            audioSource.Play();
            isAlive = false;
            isDead = true;
            GameObject.Find("Wall3").SetActive(false);
        }
    }
    new void Update()
    {
        base.Update();

        if(transform.position.y < -4 && isAlive){
            audioSource.Play();
            isAlive = false;
            isDead = true;
            GameObject.Find("Wall3").SetActive(false);
            PlayerController.instance.DepleteItem(Item.Axolotl);
        }
        else if(!isDead && animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead"))
        {
            audioSource.Play();
            isDead = true;
            GameObject.Find("Wall3").SetActive(false);
            if(murderWeapon != Item.None)
                PlayerController.instance.DepleteItem(murderWeapon);
        }
    }
}
