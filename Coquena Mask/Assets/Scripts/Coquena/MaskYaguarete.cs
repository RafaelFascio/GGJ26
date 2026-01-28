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
        throw new System.NotImplementedException();
    }

    public override void UseSecondAbility()
    {
        throw new System.NotImplementedException();
        
    }

    private void OnEnable()
    {
        Debug.Log("yaguarete Mask Equipped");
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

    public override bool isAtacking()
    {
        return attacking;
    }
    
    
}
