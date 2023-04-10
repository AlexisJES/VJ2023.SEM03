using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private int currentAnimation = 1;
    private float jumpForce = 400f;
    private float xVelocity = 10f;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        this.currentAnimation = 1;

        if (isDead)
        {
            if (Input.GetKey(KeyCode.R)) // Restart
            {
                isDead = false;
                Debug.Log("Here we go again");
                currentAnimation = 1;
                rb.position = new Vector3(-16.0f, 5.0f, 0.0f);
            }
            return;
        }


        if (isJumping())
            this.currentAnimation = 5;

        if (isFalling())
            this.currentAnimation = 6;

        var yVelocity = rb.velocity.y;
        rb.velocity = new Vector2(0, yVelocity);

        if (Input.GetKeyUp(KeyCode.UpArrow)) // Jump
        {
            currentAnimation = 5;
            rb.AddForce(transform.up * jumpForce);
        }


        if (Input.GetKey(KeyCode.LeftArrow)) // run left
        {
            currentAnimation = 2;
            rb.velocity = new Vector2(-xVelocity, yVelocity);
            sr.flipX = true;
        }


        if (Input.GetKey(KeyCode.RightArrow)) // run right
        {
            currentAnimation = 2;
            rb.velocity = new Vector2(xVelocity, yVelocity);
            sr.flipX = false;
        }

        

        anim.SetInteger("PJ", currentAnimation);
    }

    private bool isJumping()
    {

        //Debug.Log($"Before: {before}, Now: {rb.position.y}");
        Debug.Log($"YVelocity: {rb.velocity.y}");

        if (rb.velocity.y > 0)
            return true;

        return false;
    }

    private bool isFalling()
    {
        if (rb.velocity.y < 0)
            return true;
        return false;
    }


}