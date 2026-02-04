using System.Collections;
using UnityEngine;
/*Tucan: le salen alas, ataque con viento - ataque en picada
Mecánica: El jugador usa el pico para lanzar proyectiles (plumas afiladas). Y cada 3° ataque tira una rafaga de viento que daña en area
Habilidad Especial: Pico perforador se lanza sobre un enemigo y lo atraviesa con el pico causando mucho daño
Aleteo del Espíritu del viento: lanza un tornado que daña en área y ralentiza al que lo toque
*/
public class MaskTucan : Mask
{

    public AudioClip  clipTucanAttack;
    public AudioClip  clipTucanWinds;
    public GameObject featherPrefab;
    public GameObject windGustPrefab;
    public Transform spawnpoint;
    PicoPerforador picoPerforadorAbility;
    AleteoEspírituViento aleteoAbility;
    public override void Attack()
    {
      if (nextAttackTime + attackRate < Time.time)
      {
            attackCount++;
            //Vector3 direction = player.move.MousePosition();
            //la direccion del proyectil es donde impacto el mouse en el mundo 3D
            player.move.TurnToMouse();
            Vector3 direction = player.move.MousePosition() - spawnpoint.position;
            direction.Normalize();       
            if (attackCount >= 3)
            {
                WindGustProyectile script = Instantiate(windGustPrefab, spawnpoint.position, Quaternion.identity).GetComponent<WindGustProyectile>();
                
                script.direction = direction;
                script.damage = attackDamage * 2;
                attackCount = 0;
                player.sounds.source.PlayOneShot(clipTucanWinds);
            }
            else 
            {
               FeatherProyectile script = Instantiate(featherPrefab, spawnpoint.position, Quaternion.identity).GetComponent<FeatherProyectile>();
               script.direction = direction;
               script.damage = attackDamage;
               player.sounds.source.PlayOneShot(clipTucanAttack);
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
        picoPerforadorAbility.Activate(); 
    }

    public override void UseSecondAbility()
    {
        aleteoAbility.Activate();
    }

    private void OnEnable()
    {   
        
        
        attacking = false;
        hitbox.enabled = false;
        player.currentMask = this;       
        nextAttackTime = 0;
        attackCount = 0;
        player.move.flying = true;
        //animator.SetBool("volando", true);
    }
    private void OnDisable()
    {
        player.move.flying = false;
        attacking = false;
        
    }
    private void Awake()
    {
        Debug.Log("Tucan Mask Equipped");
        player = GetComponentInParent<PlayerScript>();
       // player.move = GetComponentInParent<Move>();
        hitbox = GetComponent<Collider>();
        attackRate = 0.5f;
        attacking = false;
        attackDuration = 0.3f;
        attackDamage = 10;
        picoPerforadorAbility = GetComponent<PicoPerforador>();
        aleteoAbility = GetComponent<AleteoEspírituViento>();

    }

    

    public override bool isAtacking()
    {
        // la idea seria que devuelva true mientras hace una animacion de ataque
        return attacking;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(picoPerforadorAbility.damage);
            picoPerforadorAbility.hitObject = true;
        }
        else if (other.CompareTag("ground")) 
        {
            picoPerforadorAbility.hitObject = true;

        }
    }
}
