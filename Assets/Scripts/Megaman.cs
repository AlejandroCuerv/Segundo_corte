using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float fireRate;
    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;

    public Transform FirePoint;
    public GameObject Bullet;

    float nextFire = 0;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        Run();
        Jump();
        caer();
        Disparar();
    }

    void Disparar()
    {
        if (Input.GetKeyDown(KeyCode.X)&& Time.time >= nextFire)
        {
            Instantiate(Bullet, FirePoint.transform.position, FirePoint.rotation);
            nextFire = Time.time + fireRate;
            myAnimator.SetLayerWeight(1, 1);
        }
        else
            myAnimator.SetLayerWeight(1, 0);
    }

    

    void Run()
    {
        float movH = Input.GetAxis("Horizontal");

        Vector2 movimiento = new Vector2(movH * Time.deltaTime * speed, 0);

        transform.Translate(movimiento);

        if (movH != 0)
        {
            myAnimator.SetBool("isRunning", true);
            if (movH < 0)
            {
                transform.localScale = new Vector2(-0.5f, 0.5f);
            }
            else
            {
                transform.localScale = new Vector2(0.5f, 0.5f);
            }
                
        }
        else
            myAnimator.SetBool("isRunning", false);
    }
    void Jump()
    {

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            myAnimator.SetBool("isFalling", false);
            myAnimator.SetBool("takeoff", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                myAnimator.SetTrigger("jump");
                myAnimator.SetBool("takeoff", true);
            }
        }

    }
        void caer()
        {
            if (myBody.velocity.y < 0 && myAnimator.GetBool("takeoff"))
            {
                myAnimator.SetBool("isFalling", true);
            }
        }

        void TerminarDeSaltar()
        {
            myAnimator.SetBool("isFalling", true);
            myAnimator.SetBool("takeoff", false);
        }
    
}

