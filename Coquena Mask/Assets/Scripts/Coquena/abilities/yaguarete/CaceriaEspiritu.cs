using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Caceria del Espiritu del Guerrero: Lanza muchos zarpazos en un área causando
//mucho daño a los que estén en el rango (tipo q de Yi)
public class CaceriaEspiritu : Ability
{
    
    float damagePerHit;
    float areaRadius;
    float hitInterval;
    bool isAttacking = false;
    float offsetDistance;
    public LayerMask enemylayer;
    
    Vector3 center;
    Stack<Enemy> enemiesInRange = new Stack<Enemy>();
    public override void Use()
    {
        enemiesInRange.Clear();
        player.currentState = PlayerScript.State.Casting;
       //el centro es adelante del player
        center = transform.position +transform.forward *offsetDistance;
        
        Collider[] hits = Physics.OverlapSphere(center, areaRadius, enemylayer);
        if (hits.Length > 0)
        {
            GetEnemiesInRange(hits);
            StartCoroutine(StartAttacks());
        }
        else 
        {
            Debug.Log("No le pegaste a nadie, gil");
        }
        player.currentState = PlayerScript.State.Idle;
    }
    void GetEnemiesInRange(Collider[] col) 
    {
        foreach (Collider hit in col) 
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null) 
            {
                enemiesInRange.Push(enemy);
            }
        }


    }
    IEnumerator StartAttacks() 
    {
        player.canAttack = false;
        isAttacking = true;
        while (enemiesInRange.Count >0) 
        {
           Enemy enemy = enemiesInRange.Pop();
            if (enemy !=null) 
            {
                enemy.TakeDamage(damagePerHit);
                yield return new WaitForSeconds(hitInterval);
            }
          
        }
        isAttacking = false;
        player.canAttack = true;
    }
    private void OnDrawGizmos()
    {
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.forward * offsetDistance, areaRadius);
        
       
    }
    private void Awake()
    {
        cooldown = 17;
        Abilityname = "Caceria del Espiritu";
        description ="Lanza muchos zarpazos en un área causando mucho daño a los que estén en el rango.";
        isUsable = true;
        timer = 0f;
        isUsable = true;
        cost = 10;
        player = GetComponentInParent<PlayerScript>();
        offsetDistance = 24.5f;
        damagePerHit = 20;
        areaRadius = 25;
        hitInterval = 0.4f;
    }
}
