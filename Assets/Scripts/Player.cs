
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rig;

    Animator animator;

    SpriteRenderer SR;

    BoxCollider2D box;

    [SerializeField] [Range(1,10)] private int Vida;

    [SerializeField] [Range(1,10)] private int speed;

    [SerializeField] [Range(1,20)] private int JumpForce;

    [SerializeField] private bool isJumping, doubleJump;

    void Start()
    {
        animator = GetComponent<Animator>();

        rig = GetComponent<Rigidbody2D>();

        box = GetComponent<BoxCollider2D>();
        
        SR = GetComponent<SpriteRenderer>();

        transform.position = new Vector2(0f, 0f);
    }

    void Update()
    {
        Move();
        Jump();
        Die();
    }

    void Move()
    {
        rig.velocity= new Vector2(Input.GetAxis("Horizontal") * speed, rig.velocity.y);

        
        if(Input.GetAxis("Horizontal") > 0)
        {
            SR.flipX = false;
            animator.SetBool("Run", true);
        }

        if(Input.GetAxis("Horizontal") < 0)
        {
            SR.flipX = true;
            animator.SetBool("Run", true);
        }

        if(rig.velocity.y < 0)
        {
            animator.SetBool("Fall", true);
        }
        else
        {
             animator.SetBool("Fall", false);
        }

        if(rig.velocity.y > 0)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                rig.AddForce( new Vector2(0f, JumpForce) , ForceMode2D.Impulse);
                doubleJump = true;
                animator.SetBool("jump", true);
            } 
            else
            {
                if(doubleJump)
                {
                    rig.AddForce( new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    animator.SetBool("jump", false);
                    animator.SetBool("dobleJump", true);
                    doubleJump = false;
                }
            }
        }
    }

    void Die()
    {
        if(Vida == 0)
        {
            Debug.Log("Morreu!");
            //StartCoroutine("Trocar");
        }
    }


   /* IEnumerator Trocar()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Morte");
    } */

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Espinho"))
        {
            Debug.Log("Perdeu Vida");
            Vida = Vida - 1;
            animator.Play("hit");
        }

        if (colisao.gameObject.CompareTag("V2"))
        {
           Vida = 0;
        }

        // Layer 8 = Ground

        if (colisao.gameObject.layer == 8)
        {
            isJumping = false;
            animator.SetBool("Idle", true);
            animator.SetBool("Jump", false);

        }
    }
    void OnCollisionExit2D(Collision2D colisao)
   {
        if (colisao.gameObject.layer == 8)
        {
           isJumping = true;
        }
   }

       void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Inimigo")
        {
            Vida = Vida - 1;
            animator.Play("Hit");
        }
    }
}
