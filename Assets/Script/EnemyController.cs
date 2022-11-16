using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //ïœêîêÈåæ
    private Rigidbody2D rb;
    private Animator enemyAnim;

    [SerializeField]
    private float moveSpeed, waitTime, walkTime;

    private float waitCounter, moveCounter;

    private Vector2 moveDir;

    [SerializeField]
    private BoxCollider2D area;

    [SerializeField,Tooltip("ÉvÉåÉCÉÑÅ[Çí«Ç¢Ç©ÇØÇÈ?")]
    private bool chase;

    private bool isChasing;

    [SerializeField]
    private float chaseSpeed, rangeToChase;

    private Transform target;

    [SerializeField]
    private float waitAfterHitting;

    [SerializeField]
    private int attackDamage;

    [SerializeField]
    private int currentHealth;

    private bool isKnockingBack;

    [SerializeField]
    private float knockBackTime, knockBackForce;

    private float knockBackCounter;

    private Vector2 knockDir;


    //Ç±Ç¡Ç©ÇÁìÆçÏÇ∆Ç©--------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();

        waitCounter = waitTime;

        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        if(isKnockingBack)
        {
            if(knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;
                rb.velocity = knockDir * knockBackForce;
            }
            else
            {
                rb.velocity = Vector2.zero;

                isKnockingBack = false;
            }

            return;
        }

        if(!isChasing)
        {
            if (waitCounter > 0)
            {
                waitCounter -= Time.deltaTime;
                rb.velocity = Vector2.zero;

                if (waitCounter <= 0)
                {
                    moveCounter = walkTime;

                    enemyAnim.SetBool("moving", true);

                    moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    moveDir.Normalize();
                }
            }
            else
            {
                moveCounter -= Time.deltaTime;

                rb.velocity = moveDir * moveSpeed;

                if (moveCounter <= 0)
                {
                    enemyAnim.SetBool("moving", false);

                    waitCounter = waitTime;
                }
            }

            if(chase)
            {
                if(Vector3.Distance(transform.position, target.transform.position) < rangeToChase)
                {
                    isChasing = true;
                }
            }

        }
        else
        {
            if (waitCounter > 0)
            { 
                waitCounter -= Time.deltaTime;
                rb.velocity = Vector2.zero;

                if (waitCounter <= 0)
                {
                    enemyAnim.SetBool("moving", true);
                }
            }
            else
            {
                moveDir = target.transform.position - transform.position;
                moveDir.Normalize();

                rb.velocity = moveDir * moveSpeed;
            }

            if(Vector3.Distance(transform.position, target.transform.position) > rangeToChase)
            {
                isChasing = false;

                waitCounter = waitTime;

                enemyAnim.SetBool("moving", false);
            }
        }



        

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, area.bounds.min.x + 1, area.bounds.max.x - 1),
                                        Mathf.Clamp(transform.position.y, area.bounds.min.y + 1, area.bounds.max.y - 1),
                                        transform.position.z);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(isChasing)
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();

                player.KnockBack(transform.position);
                player.DamagePlayer(attackDamage);

                waitCounter = waitAfterHitting;

                enemyAnim.SetBool("moving",true);
            }
        }
    }

    public void KnockBack(Vector3 position)
    {
        isKnockingBack = true;
        knockBackCounter = knockBackTime;
        
        knockDir = transform.position - position;
        knockDir.Normalize();

        enemyAnim.SetBool("moving", false);

    }

    public void TakeDamage(int damage, Vector3 position)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        KnockBack(position);
    }
}
