
using System.Collections;
using UnityEngine;


/*Tapir: Piel de la selva: Resistencia al daño y al empuje.
Mecánica: El jugador ataca cuerpo a cuerpo con cabezazos, cada 3° ataque aturde al objetivo.
Habilidad especial: Pisotón furioso: crea un temblor que ralentiza a todos los enemigos
Embestida del espíritu del Monte: Embiste rápidamente un área seleccionada, haciendo mucho daño a quien este en su camino y aturdiendolos.
*/
public class Masktapir : Mask
{
    
    public override void Attack()
    {
        if (nextAttackTime + attackRate < Time.time)
        {
            attackCount++;
            player.move.TurnToMouse();
            StartCoroutine(EnableHitCollider());
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
        Debug.Log("Tapir First Ability");
    }

    public override void UseSecondAbility()
    {
        Debug.Log("Tapir Second Ability");
    }

    private void OnEnable()
    {
        Debug.Log("Tapir Mask Equipped");
        player.currentMask = this;
        nextAttackTime = 0f;
        attackCount = 0;
        hitbox.enabled = false;

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
    
    private void OnTriggerEnter(Collider other)
    {
        if (attacking && other.CompareTag("Enemy"))
        {
            //hago daño al enemigo

            if (attackCount >= 3) 
            {
               //aturde al enemigo
               
            }
            
        }
    }

    public override bool isAtacking()
    {
        return attacking;
    }
    
}
