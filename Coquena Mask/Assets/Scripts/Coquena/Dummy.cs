using System.Collections;
using TMPro;
using UnityEngine;

public class Dummy : Enemy
{
    EnemyHealthBar healthBar;
   
    void Start()
    {
        maxHp = 100f;
        currentHp = maxHp;
        speed = 25f;
        
        healthBar = GetComponentInChildren<EnemyHealthBar>();
       
    }

    override public void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthBar.damageAmount += (int)damage;
        healthBar.damageText.text = healthBar.damageAmount.ToString();
        healthBar.timer = 0;
        if (!healthBar.showDamage)
        {         
            StartCoroutine(healthBar.ShowText());
        }
        else 
        {           
            if (healthBar.damageText != null) healthBar.damageText.enabled = true;           
        }
    }
   

}
