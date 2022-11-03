using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private PlayerController player;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthUI() 
    {
        hpSlider.maxValue = player.maxHealth;
        hpSlider.value = player.currentHealth;
    }
}
