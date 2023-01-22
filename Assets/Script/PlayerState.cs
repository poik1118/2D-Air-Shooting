using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    Animator                anime;
    private Rigidbody2D     rigid;

    public bool             isTouchTop;
    public bool             isTouchBottom;
    public bool             isTouchRight;
    public bool             isTouchLeft;
    public bool             isHit;

    public int              score = 0;

    public float            speed = 1f;
    public int              HP = 3;
    public int              power = 1;
    public int              maxPower = 5;
    public float            shotSpeed = 20f;
    public float            maxShotSpeed = 0.2f;
    public float            curShotSpeed;

    public GameObject       BulletObjA;
    public GameObject       BulletObjB;

    public int              boom = 0;
    public int              maxBoom = 2;
    public GameObject       boomEffect;
    public int              boomDamage = 500;
    public float            boomVisibleTime = 2f;
    public bool             isBoomTime;

    public GameManager manager;


    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        anime = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
        Boom();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anime.SetInteger("Input", (int)h);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;

                case "Bottom":
                    isTouchBottom = true;
                    break;

                case "Right":
                    isTouchRight = true;
                    break;

                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }

        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (isHit)
                return;
            isHit = true;
            HP--;
            manager.UpdateHPIcon(HP);
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            if (HP == 0)
            {
                manager.GameOver();
            }
            else
            {
                manager.RespawnPlayer();
            }
        }

        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;

                case "Power":
                    if (power < maxPower)
                    {
                        power++;
                    }
                    break;
                case "Boom":
                    if (boom >= maxBoom)
                        score += 500;
                    else
                    {
                        boomEffect.SetActive(false);
                        boom++;
                        manager.UpdateBoomIcon(boom);
                    }
                    break;
            }

            Destroy(collision.gameObject);
        }

    }

    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;

                case "Bottom":
                    isTouchBottom = false;
                    break;

                case "Right":
                    isTouchRight = false;
                    break;

                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }

    void Fire()
    {
        if (!Input.GetKey(KeyCode.Z))
            return;

        if (curShotSpeed < maxShotSpeed)
            return;

        switch (power)
        {
            case 1:
                GameObject bulletC1 = Instantiate(BulletObjA, transform.position, transform.rotation);

                Rigidbody2D rgC1 = bulletC1.GetComponent<Rigidbody2D>();

                rgC1.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);

                break;

            case 2:
                GameObject bulletR2 = Instantiate(BulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL2 = Instantiate(BulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);

                Rigidbody2D rgR2 = bulletR2.GetComponent<Rigidbody2D>();
                Rigidbody2D rgL2 = bulletL2.GetComponent<Rigidbody2D>();

                rgR2.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);
                rgL2.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);

                break;

            case 3:
                GameObject bulletC3 = Instantiate(BulletObjB, transform.position, transform.rotation);

                Rigidbody2D rgC3 = bulletC3.GetComponent<Rigidbody2D>();

                rgC3.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);

                break;

            case 4:
                GameObject bulletR4 = Instantiate(BulletObjA, transform.position + Vector3.right * 0.2f, transform.rotation);
                GameObject bulletC4 = Instantiate(BulletObjB, transform.position, transform.rotation);
                GameObject bulletL4 = Instantiate(BulletObjA, transform.position + Vector3.left * 0.2f, transform.rotation);

                Rigidbody2D rgR4 = bulletR4.GetComponent<Rigidbody2D>();
                Rigidbody2D rgC4 = bulletC4.GetComponent<Rigidbody2D>();
                Rigidbody2D rgL4 = bulletL4.GetComponent<Rigidbody2D>();

                rgR4.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);
                rgC4.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);
                rgL4.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);

                break;

            case 5:
                GameObject bulletR5 = Instantiate(BulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bulletC5 = Instantiate(BulletObjB, transform.position, transform.rotation);
                GameObject bulletL5 = Instantiate(BulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);

                Rigidbody2D rgR5 = bulletR5.GetComponent<Rigidbody2D>();
                Rigidbody2D rgC5 = bulletC5.GetComponent<Rigidbody2D>();
                Rigidbody2D rgL5 = bulletL5.GetComponent<Rigidbody2D>();

                rgR5.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);
                rgC5.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);
                rgL5.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);

                break;
        }
        curShotSpeed = 0;
    }

    void Reload()
    {
        curShotSpeed += Time.deltaTime;
    }

    void Boom()
    {
        if (!Input.GetKeyDown(KeyCode.X))
            return;

        //if (!isBoomTime)
        //    return;

        if (boom == 0)
            return;

        boom--;
        isBoomTime = true;
        manager.UpdateBoomIcon(boom);
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", boomVisibleTime);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");   //Remove Enemy
        for (int index = 0; index < enemies.Length; index++)
        {
            Enemy enemyLogic = enemies[index].GetComponent<Enemy>();
            enemyLogic.OnHit(500);
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");   //Remove Bullet
        for (int index = 0; index < bullets.Length; index++)
        {
            Destroy(bullets[index]);
        }
    }
}