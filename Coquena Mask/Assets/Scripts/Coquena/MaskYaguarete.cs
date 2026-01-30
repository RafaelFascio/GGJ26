using System.Collections;
using UnityEngine;

/*Yaguareté: 
Mecánica: Ataques rápidos con las manos. Cada tercer golpe consecutivo desgarra al enemigo, causando daño por sangrado.
Habilidad Especial: Salto del Predador. El jugador se lanza sobre un enemigo a distancia, aturdiendo brevemente. (tipo ulti de warwick)
Caceria del Espiritu del Guerrero: Lanza muchos zarpazos en un área causando mucho daño a los que estén en el rango (tipo q de Yi)
Tecla Espacio habilitado para esquivar (dash)
*/
public class MaskYaguarete : Mask
{
    float bleedDuration = 5f;
    float bleedDamagePerTick = 2f;
    float bleedTickInterval = 1f;
    CaceriaEspiritu caceriaEspiritu;
    public override void Attack()
    {
        if (nextAttackTime + attackRate < Time.time)
        {
            attackCount++;
            player.move.TurnToMouse();
            StartCoroutine(EnableHitCollider(attackDuration));
            nextAttackTime = Time.time;
            
            
        }
        else
        {
            Debug.Log("Already attacking!");
        }
    }

    public override void UseFirstAbility()
    {
        throw new System.NotImplementedException();
    }

    public override void UseSecondAbility()
    {
        caceriaEspiritu.Activate();

    }

    private void OnEnable()
    {
        Debug.Log("yaguarete Mask Equipped");
        player.currentMask = this;
        nextAttackTime = 0f;
        attackCount = 0;
        hitbox.enabled = false;
        player.canDash = true;

    }
    private void OnDisable()
    {
        player.canDash = false;
    }
    private void Awake()
    {
        
        player = GetComponentInParent<PlayerScript>();
        hitbox = GetComponent<Collider>();
        caceriaEspiritu = GetComponent<CaceriaEspiritu>();
        attackRate = 0.5f;
        attacking = false;
        attackDuration = 0.3f;
        attackDamage = 10;
    }

    public override bool isAtacking()
    {
        return attacking;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(attackDamage);
                        
            if (attackCount >=3) 
            {
                enemy.ApplyDamageOverTime(bleedDamagePerTick, bleedDuration, bleedTickInterval);               
            }
        }
    }
}
