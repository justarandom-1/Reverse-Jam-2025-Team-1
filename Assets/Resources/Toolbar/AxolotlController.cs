using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxolotlController : MonoBehaviour
{
    private float direction;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.GetDirection();
        rb = GetComponent<Rigidbody2D>();

        transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);
    }

    public void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Enemy"))
            return;

        direction *= -1;

        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 75)
            rb.velocity = new Vector2(direction * 12, rb.velocity.y);
        else
            rb.velocity = new Vector2(direction * 4, rb.velocity.y);
    }
}
