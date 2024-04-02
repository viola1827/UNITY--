using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float rotationMultiplier = 1.0f; // 可调节武器旋转速度的倍数
    //public Vector3 newRotationCenter = new Vector3(1, 0, 0);//设置新的旋转中心
    private float rotationSpeed = 0;

    //attack
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    public Animator camAnim;
    public Animator enemyAnim;




    public void SetRotationSpeed(float newSpeed)
    {
        rotationSpeed = newSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * rotationMultiplier * Time.deltaTime));

        //设置新的旋转中心
        // transform.RotateAround(transform.position + newRotationCenter, Vector3.forward, rotationSpeed * Time.deltaTime);

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                camAnim.SetTrigger("shake");
                Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < playerToDamage.Length; i++)
                {
                    //enemiesToDamage[i].GetComponent<Player>().health -= TakeDamage(damage);
                    playerToDamage[i].GetComponent<Enemy>().TakeDamage(damage);

                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
