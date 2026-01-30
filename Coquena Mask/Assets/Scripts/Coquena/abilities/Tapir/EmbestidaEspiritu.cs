using System.Collections;
using UnityEngine;
//Embestida del espíritu del Monte: Embiste rápidamente un área seleccionada,
//haciendo mucho daño a quien este en su camino y aturdiendolos.
public class EmbestidaEspiritu : Ability
{
   
   float chargeDuration = 0.5f;
   float chargeTimer = 0f;
   float chargeSpeed = 30f;
    [HideInInspector]  public float chargeDamage = 50f;
    [HideInInspector] public float stunDuration = 1f;
    CharacterController controller;
    Mask mask;
    Vector3 direction;
    public override void Use()
    {
             StartCoroutine(Charge());     
    }

    private void Awake()
    {
        player = GetComponentInParent<PlayerScript>();
        Abilityname = "Embestida del espíritu del Monte";
        description = "Embiste rápidamente un área seleccionada," +
        "haciendo mucho daño a quien este en su camino y aturdiendolos.";
        cooldown = 15f;
        timer = 0f;
        isUsable = true;
        cost = 10;      
        controller = player.GetComponent<CharacterController>();
        mask = GetComponent<Mask>();
    }
    IEnumerator Charge() 
    {
        player.canMove = false;
        player.canAttack =false;
       
        direction = player.transform.forward;
        direction.y = 0;
        StartCoroutine(mask.EnableHitCollider(chargeDuration));
        while (chargeTimer < chargeDuration && player.currentState != PlayerScript.State.Stunned)
        {
            controller.Move(chargeSpeed * Time.deltaTime * direction);
            chargeTimer += Time.deltaTime;
            yield return null;
        }
        player.canMove = true;
        player.canAttack = true;
        chargeTimer = 0f;
    }
}
