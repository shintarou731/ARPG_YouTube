using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField,Tooltip("移動スピード")]
    private int moveSpeed;

    [SerializeField]
    private Animator playerAnim;

    [SerializeField]
    private Animator weponAnim;

    public Rigidbody2D rb;

    [System.NonSerialized]
    public int currentHealth;
    public int maxHealth;


    void Start()
    {
        currentHealth = maxHealth;

        GameManager.instance.UpdateHealthUI();
    }


    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;

        if(rb.velocity != Vector2.zero)
        {
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                if(Input.GetAxisRaw("Horizontal") > 0)
                {
                    playerAnim.SetFloat("X", 1f);
                    playerAnim.SetFloat("Y", 0);

                    weponAnim.SetFloat("X", 1f);
                    weponAnim.SetFloat("Y", 0);
                }
                else
                {
                    playerAnim.SetFloat("X", -1f);
                    playerAnim.SetFloat("Y", 0);

                    weponAnim.SetFloat("X", -1f);
                    weponAnim.SetFloat("Y", 0);
                }
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", 1);

                weponAnim.SetFloat("X", 0);
                weponAnim.SetFloat("Y", 1);
            }
            else
            {
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", -1);

                weponAnim.SetFloat("X", 0);
                weponAnim.SetFloat("Y", -1);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            weponAnim.SetTrigger("Attack");
        }



    }
}
