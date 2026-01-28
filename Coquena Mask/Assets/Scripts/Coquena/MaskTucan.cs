using System.Collections;
using UnityEngine;
/*Tucan: le salen alas, ataque con viento - ataque en picada
Mecánica: El jugador usa el pico para lanzar proyectiles (plumas afiladas). Y cada 3° ataque tira una rafaga de viento que daña en area
Habilidad Especial: Pico perforador se lanza sobre un enemigo y lo atraviesa con el pico causando mucho daño
Aleteo del Espíritu del viento: lanza un tornado que daña en área y ralentiza al que lo toque
*/
public class MaskTucan : Mask
{

    
    public GameObject featherPrefab;
    public GameObject windGustPrefab;
    public Transform spawnpoint;

    public override void Attack()
    {
      if (nextAttackTime + attackRate < Time.time)
      {
        attackCount++;
            Vector3 direction = player.move.GetLookDirection();
            player.move.TurnToMouse();
            if (attackCount >= 3)
            {
                WindGustProyectile script = Instantiate(windGustPrefab, spawnpoint.position, spawnpoint.rotation).GetComponent<WindGustProyectile>();
                
                script.direction = direction;
                script.damage = attackDamage * 2;
                attackCount = 0;
            }
            else 
            {
               FeatherProyectile script = Instantiate(featherPrefab, spawnpoint.position, spawnpoint.rotation).GetComponent<FeatherProyectile>();
               script.direction = direction;
               script.damage = attackDamage;
            }
                
             nextAttackTime = Time.time;      
            //attacking = true;
        }
      else 
      { 
            Debug.Log("Already attacking!");
      }
    }

    public override void UseFirstAbility()
    {
        Debug.Log("Tucan First Ability");
    }

    public override void UseSecondAbility()
    {
        Debug.Log("Tucan Second Ability");
    }

    private void OnEnable()
    {
        Debug.Log("Tucan Mask Equipped");
        attacking = false;
        hitbox.enabled = false;
        player.currentMask = this;
        nextAttackTime = 0;
        attackCount = 0;
    }
    private void Awake()
    {
        player = GetComponentInParent<PlayerScript>();
        hitbox = GetComponent<Collider>();
        attackRate = 0.5f;
        attacking = false;
        attackDuration = 0.3f;
        attackDamage = 10;
        
    }

    

    public override bool isAtacking()
    {
        // la idea seria que devuelva true mientras hace una animacion de ataque
        return attacking;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
