using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D MyRb;
    public float Speed;

    public GameObject player;
    private Transform playerTrans;

    public GameObject expl;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            GameObject clonEx = Instantiate(expl, transform.position, transform.rotation) as GameObject;
            Destroy(clonEx, 0.5f);
            Destroy(gameObject);

        }
         
    }

    void Start()
    {
        MyRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTrans.localScale.x > 0)
        {
            MyRb.velocity = transform.right * Speed;
            transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        }
        else
        {
            MyRb.velocity = transform.right * -Speed;
            transform.localScale = new Vector3(-3.5f, 3.5f, 3.5f);
        }
        
    }
}
