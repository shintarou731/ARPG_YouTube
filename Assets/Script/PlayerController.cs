using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�ϐ��錾
    [SerializeField,Tooltip("�ړ��X�s�[�h")]
    private int moveSpeed;

    [SerializeField]
    private Animator playerAnim;

    [SerializeField]
    private Animator weponAnim;

    public Rigidbody2D rb;

    [System.NonSerialized]
    public int currentHealth;
    public int maxHealth;

    private bool isKnockingback;
    private Vector2 knockDir;

    [SerializeField]
    private float knockbackTime, knockbackForce;
    private float knockbackCounter;

    [SerializeField]
    private float invincibilityTime;
    private float invincibilityCounter;

    //�������瓮��Ƃ�--------------------------------------------------
    void Start()
    {
        currentHealth = maxHealth;

        GameManager.instance.UpdateHealthUI();
    }


    void Update()
    {
        //�v���C���[�̃m�b�N�o�b�N
        if (invincibilityCounter > 0) 
        {
            invincibilityCounter -= Time.deltaTime;
        }

        if(isKnockingback)
        {
            knockbackCounter -= Time.deltaTime;
            rb.velocity = knockDir * knockbackForce;

            if(knockbackCounter <= 0)
            {
                isKnockingback = false;
            }
            else
            {
                return;
            }

        }

        //�v���C���[�̏㉺���E����
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;

        //�v���C���[�̌���
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

        //�v���C���[�̍U��
        if(Input.GetMouseButtonDown(0))
        {
            weponAnim.SetTrigger("Attack");
        }



    }
    /// <summary>
    /// ������΂��p�̊֐�
    /// </summary>
    /// <param name="position"></param>
    public void KnockBack(Vector3 position)
    {
        knockbackCounter = knockbackTime;
        isKnockingback = true;
        knockDir = transform.position - position;
        knockDir.Normalize();
    }

    public void DamagePlayer(int damage)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage,0,maxHealth);
            invincibilityCounter = invincibilityTime;

            if (currentHealth == 0)
            {
                gameObject.SetActive(false);
            }
        }

        GameManager.instance.UpdateHealthUI();
    }
}
