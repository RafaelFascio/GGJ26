using System.Collections;
using UnityEngine;
//Salto del Predador. El jugador se lanza sobre un enemigo a distancia, aturdiendo brevemente. (tipo ulti de warwick)
//el salto seria como un tiro parabólico, se lanza hacia donde mira el jugador y seguira hasta que toque el suelo o un enemigo;
public class SaltoDepredador : Ability
{
    CharacterController controller;
    public LayerMask layerMask;
    Vector2 velocity; 
    float gravity; 
    float detectionRadius;
    int  hitCount;
    float maxduration;
    Collider[] hitColliders;
    bool jumping;
    public override void Use()
    {

        
        velocity = new Vector2(35f, 7f);
        StartCoroutine(Jump());
    }
    private void Awake()
    {
        Abilityname = "Salto depredador";    
        description = "El jugador se lanza sobre un enemigo a distancia, aturdiendo brevemente.";
        cooldown = 3f;
        timer = 0f;
        isUsable = true;
        cost = 10;
        jumping = false;
        maxduration = 1f;
        hitColliders = new Collider[1];
        gravity = -10f;
        detectionRadius = 2f;
        controller = GetComponentInParent<CharacterController>();
        player = GetComponentInParent<PlayerScript>();
    }
    IEnumerator Jump() 

    {   
        
        jumping = true;
        player.canMove = false;
        player.currentState = PlayerScript.State.Casting;
        float elapsed = 0f;
        hitCount = 0;
        Vector3 direction =transform.forward.normalized;
        while (hitCount <=0 && elapsed < maxduration && !IsGrounded()) 
        {
           
            
            controller.Move((direction *velocity.x + Vector3.up *velocity.y) *Time.deltaTime);            
            elapsed += Time.deltaTime;
            velocity.y += gravity * elapsed; 
            hitCount = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, hitColliders, layerMask);
            
            yield return null;
        }
        velocity = Vector2.zero;
        jumping = false;
        if (hitCount >0) 
        {
            //Aturdir enemigo
            Debug.Log("Enemigo aturdido");
        }
        timer = cooldown;
        player.currentState = PlayerScript.State.Idle;
        player.canMove = true;
    }

    bool IsGrounded()
    {
        return controller.isGrounded && velocity.y <0;
    }
    private void OnDrawGizmos()
    {
        if (!jumping) { return; }
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

    }
}
