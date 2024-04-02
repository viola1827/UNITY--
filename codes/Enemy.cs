using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 inputVec;
    public GameObject EnemyWeapon;
    private EnemyWeapon enemyweaponScript;

    public float SetRotationSpeed;
    public float rotationSpeed = 100f; //change movement in rotation
    public float speed = 3;
    public float jumpForce;

    public GameObject ground;

    public int health;
    public GameObject bloodEffect;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyweaponScript = EnemyWeapon.GetComponent<EnemyWeapon>();

    }

    //use this when object without inputSystem 
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        //inputVec.y = Input.GetAxisRaw("Vertical");
        rigid.velocity = new Vector2(inputVec.x * speed, rigid.velocity.y);

        if (Input.GetKey(KeyCode.Q))
        {
            EnemyWeapon.transform.Rotate(0, 0, -1);//change the rotate value in z angle(0,0,1)
            rigid.AddForce(new Vector2(0, jumpForce));
            print("left arrow key is held down");
        }
        if (Input.GetKey(KeyCode.E))
        {
            EnemyWeapon.transform.Rotate(0, 0, 1);
            rigid.AddForce(new Vector2(0, jumpForce));
            print("right arrow key is held down");
        }
        if (enemyweaponScript != null)
        {
            enemyweaponScript.SetRotationSpeed(inputVec.x * rotationSpeed);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void MovePlayerForkwoard()
    {
        rigid.velocity = new Vector2(speed, rigid.velocity.y);
    }

    void MovePlayerBackwoard()
    {
        rigid.velocity = new Vector2(-speed, rigid.velocity.y);
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage TANKEN");
    }
}
