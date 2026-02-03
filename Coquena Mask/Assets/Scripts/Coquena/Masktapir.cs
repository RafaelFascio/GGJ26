
using System.Collections;
using UnityEngine;


/*Tapir: Piel de la selva: Resistencia al daño y al empuje.
Mecánica: El jugador ataca cuerpo a cuerpo con cabezazos, cada 3° ataque aturde al objetivo.
Habilidad especial: Pisotón furioso: crea un temblor que ralentiza a todos los enemigos
Embestida del espíritu del Monte: Embiste rápidamente un área seleccionada, haciendo mucho daño a quien este en su camino y aturdiendolos.
*/
public class Masktapir : Mask
{
    
    Pisoton pisoton;
    EmbestidaEspiritu embestida;
    float stunDuration;
    float damageResist = 0.25f; //25% de resistencia al daño
    public override void Attack()
    {
        if (nextAttackTime + attackRate < Time.time)
        {
            attackCount++;
            player.move.TurnToMouse();
            StartCoroutine(EnableHitCollider(attackDuration));
            nextAttackTime = Time.time;
            animator.SetTrigger("punch");
            
        }
        else
        {
            
            Debug.Log("Already attacking!");
        }
    }

    public override void UseFirstAbility()
    {
        pisoton.Activate();
    }

    public override void UseSecondAbility()
    {
        embestida.Activate();
    }

    private void OnEnable()
    {
        Debug.Log("Tapir Mask Equipped");
        player.currentMask = this;
        nextAttackTime = 0f;
        attackCount = 0;
        hitbox.enabled = false;
        player.damageresist += damageResist;

    }
    
    private void OnDisable()
    {
        player.damageresist -= damageResist;
    }
    private void Awake()
    {
        player = GetComponentInParent<PlayerScript>();
        hitbox = GetComponent<Collider>();
        pisoton = GetComponent<Pisoton>();
        embestida = GetComponent<EmbestidaEspiritu>();
        attackRate = 0.5f;
        attacking = false;
        attackDuration = 0.3f;
        attackDamage = 10;
        damageResist = 0.25f;
        stunDuration = 3;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            

            if (player.currentState == PlayerScript.State.Casting) //el trigger se activo durante una habilidad

            {
                Debug.Log("enemigo Embestido");
                enemy.TakeDamage(embestida.chargeDamage);
                enemy.ApplyStun(embestida.stunDuration);
            }else //el trigger se activo durante un ataque normal
            {
                enemy.TakeDamage(attackDamage);
                if (attackCount >= 3)
                {
                    Debug.Log("Atudi por ataque");
                    enemy.ApplyStun(stunDuration);
                }
            }           
        }
    }

    public override bool isAtacking()
    {
        return attacking;
    }
    
}
