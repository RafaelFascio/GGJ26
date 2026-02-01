using System.Collections;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
//Pico perforador se lanza sobre un enemigo y lo atraviesa con el pico causando mucho daño
public class PicoPerforador : Ability
{
    public float speedBuff;
    public int damage;
    public float detectionRadius;
    public float maxduration;
    public LayerMask layerMask;
    bool charging;
    
    private Collider[] overlapResults = new Collider[16];
    [HideInInspector]public bool hitObject;
    
    CharacterController controller;
    public override void Use()
    {
        player.move.flying = false;
        hitObject = false;
        player.currentState = PlayerScript.State.Casting;

        //girar el jugador 45 grados en el eje x hacia abajo
        player.transform.rotation = Quaternion.Euler(45, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
        Vector3 direction = player.move.MousePosition() - player.transform.position;
        direction.Normalize();
        StartCoroutine(Descend(direction));

    }
    IEnumerator Descend(Vector3 direction) 
    {
        player.canMove = false;
        player.transform.rotation = Quaternion.Euler(45, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
        int hitsCount = 0;
        float elapsed = 1f;
        charging = true;

        //Carga hasta que choca con algo o se acaba el tiempo
        while (!hitObject && elapsed < maxduration) 
        {
         hitsCount = Physics.OverlapSphereNonAlloc(player.transform.position, detectionRadius,overlapResults,layerMask);
            controller.Move(speedBuff * Time.deltaTime * direction);
            
            if (hitsCount >0) 
            {
                hitObject = true;           
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
       
        //Aplica daño a los enemigos alcanzados
        charging = false;
        for (int i = 0; i < hitsCount; i++)
        {
            Collider hit = overlapResults[i];
            if (hit != null && hit.CompareTag("Enemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Debug.Log("Hit enemy with Pico Perforador");
                    enemy.TakeDamage(damage);
                }
            }
            
        }
        //girar el jugador 45 grados en el eje x hacia arriba
         player.canMove = true;
         player.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
         player.currentState = PlayerScript.State.Idle;
         player.move.flying = true;
         
 
    }
    private void Awake()
    {
        
        player = GetComponentInParent<PlayerScript>();
        controller = GetComponentInParent<CharacterController>();
        Abilityname = "Pico Perforador";
        damage = 70;
        description = "Se lanza sobre un enemigo y lo atraviesa con el pico causando "+ damage + " daño";
        cooldown = 10f;
        timer = 0f;
        isUsable = true;
        cost = 10;
        speedBuff =14;
        detectionRadius = 1.5f;
        maxduration = 4f;
        charging = false;
    }
    private void OnDrawGizmos()
    {
        if (charging)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.forward * 1.7f, detectionRadius);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + transform.forward * 1.7f, detectionRadius);
        }
    }

}
