using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public int              enemyScore;
    public float            speed;
    public float            HP;

    public Sprite[]         sprites;
    SpriteRenderer          spriteRenderer;

    public float            shotSpeed;
    public float            maxShotSpeed = 0.2f;
    public float            curShotSpeed;
    public float            power = 1f;

    public GameObject       BulletObjA;
    public GameObject       BulletObjB;

    public GameObject       itemCoin;
    public GameObject       itemBoom;
    public GameObject       itemPower;

    public GameObject       player;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Fire();
        Reload();
    }

    public void OnHit(float Damege)
    {
        if(HP <= 0)
            return;

        HP -= Damege;

        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.2f);
        if (HP <= 0)
        {
            PlayerState playerLogic = player.GetComponent<PlayerState>();
            playerLogic.score += enemyScore;

            int ran = Random.Range(0, 10);
            if(ran < 5)
            {
                Debug.Log("Not Item");
            }
            else if(ran < 8)
            {
                Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
            }
            else if (ran < 9)
            {
                Instantiate(itemPower, transform.position, itemPower.transform.rotation);
            }
            else if (ran < 10)
            {
                Instantiate(itemBoom, transform.position, itemBoom.transform.rotation);
            }

            Destroy(gameObject);
        }
    }
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.Damege);
            Destroy(collision.gameObject);
        }
    }
    void Fire()
    {
        if (curShotSpeed < maxShotSpeed)
            return;

        switch (power)
        {
            case 1:
                GameObject bulletC1 = Instantiate(BulletObjA, transform.position, transform.rotation);
                Rigidbody2D rgC1 = bulletC1.GetComponent<Rigidbody2D>();
                Vector3 dirVecC1 = player.transform.position - transform.position;
                rgC1.AddForce(dirVecC1.normalized * shotSpeed, ForceMode2D.Impulse);
                break;

            case 2:
                GameObject bulletR2 = Instantiate(BulletObjA, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bulletL2 = Instantiate(BulletObjA, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rgR2 = bulletR2.GetComponent<Rigidbody2D>();
                Rigidbody2D rgL2 = bulletL2.GetComponent<Rigidbody2D>();
                Vector3 dirVecR2 = player.transform.position - transform.position;
                Vector3 dirVecL2 = player.transform.position - transform.position;
                rgR2.AddForce(dirVecR2.normalized * shotSpeed, ForceMode2D.Impulse);
                rgL2.AddForce(dirVecL2.normalized * shotSpeed, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletC3 = Instantiate(BulletObjB, transform.position, transform.rotation);
                Rigidbody2D rgC3 = bulletC3.GetComponent<Rigidbody2D>();
                Vector3 dirVecC3 = player.transform.position - transform.position;
                rgC3.AddForce(dirVecC3.normalized * shotSpeed, ForceMode2D.Impulse);
                break;

            case 4:
                GameObject bulletR4 = Instantiate(BulletObjA, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bulletC4 = Instantiate(BulletObjB, transform.position, transform.rotation);
                GameObject bulletL4 = Instantiate(BulletObjA, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rgR4 = bulletR4.GetComponent<Rigidbody2D>();
                Rigidbody2D rgC4 = bulletC4.GetComponent<Rigidbody2D>();
                Rigidbody2D rgL4 = bulletL4.GetComponent<Rigidbody2D>();
                Vector3 dirVecR4 = player.transform.position - transform.position;
                Vector3 dirVecC4 = player.transform.position - transform.position;
                Vector3 dirVecL4 = player.transform.position - transform.position;
                rgR4.AddForce(dirVecR4.normalized * shotSpeed, ForceMode2D.Impulse);
                rgC4.AddForce(dirVecC4.normalized * shotSpeed, ForceMode2D.Impulse);
                rgL4.AddForce(dirVecL4.normalized * shotSpeed, ForceMode2D.Impulse);
                break;

            case 5:
                GameObject bulletR5 = Instantiate(BulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bulletC5 = Instantiate(BulletObjB, transform.position, transform.rotation);
                GameObject bulletL5 = Instantiate(BulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rgR5 = bulletR5.GetComponent<Rigidbody2D>();
                Rigidbody2D rgC5 = bulletC5.GetComponent<Rigidbody2D>();
                Rigidbody2D rgL5 = bulletL5.GetComponent<Rigidbody2D>();
                Vector3 dirVecR5 = player.transform.position - transform.position;
                Vector3 dirVecC5 = player.transform.position - transform.position;
                Vector3 dirVecL5 = player.transform.position - transform.position;
                rgR5.AddForce(dirVecR5.normalized * shotSpeed, ForceMode2D.Impulse);
                rgC5.AddForce(dirVecC5.normalized * shotSpeed, ForceMode2D.Impulse);
                rgL5.AddForce(dirVecL5.normalized * shotSpeed, ForceMode2D.Impulse);
                break;
        }
        curShotSpeed = 0;
    }
    void Reload()
    {
        curShotSpeed += Time.deltaTime;
    }
}