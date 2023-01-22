using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    public float itemDropSpeed;
    Rigidbody2D rg;


    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = Vector2.down * itemDropSpeed;
    }
    void Start()
    {
        
    }
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
