using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    //�ϐ��錾
    [SerializeField]
    private int attackDamage;




    //�������瓮��Ƃ�--------------------------------------------------
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //�U���p�̊֐����Ăяo��
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(attackDamage,transform.position);
        }
    }

}
