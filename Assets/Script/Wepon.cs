using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    //変数宣言
    [SerializeField]
    private int attackDamage;




    //こっから動作とか--------------------------------------------------
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
            //攻撃用の関数を呼び出す
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(attackDamage,transform.position);
        }
    }

}
