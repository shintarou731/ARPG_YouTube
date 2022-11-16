using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //•Ï”éŒ¾
    public static GameManager instance;

    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private PlayerController player;


    //‚±‚Á‚©‚ç“®ì‚Æ‚©--------------------------------------------------
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void UpdateHealthUI() 
    {
        hpSlider.maxValue = player.maxHealth;
        hpSlider.value = player.currentHealth;
    }
}
