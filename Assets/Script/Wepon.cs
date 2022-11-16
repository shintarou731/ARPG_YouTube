using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    //•Ï”éŒ¾
    [SerializeField]
    private int attackDamage;




    //‚±‚Á‚©‚ç“®ì‚Æ‚©--------------------------------------------------
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
            //UŒ‚—p‚ÌŠÖ”‚ğŒÄ‚Ño‚·
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(attackDamage,transform.position);
        }
    }

}
