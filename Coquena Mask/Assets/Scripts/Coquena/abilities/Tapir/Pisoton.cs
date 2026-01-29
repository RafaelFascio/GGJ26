using UnityEngine;

//Pisotón furioso: crea un temblor que ralentiza a todos los enemigos
public class Pisoton : Ability
{

    
    float slowAmount;
    float radius;
    public LayerMask mask;
    public override void Use()
    {
      Collider[] coliders =  Physics.OverlapSphere(transform.position, radius, mask);
        foreach (Collider hit in coliders) 
        {
            hit.gameObject.GetComponent<Enemy>().ApplySlow(slowAmount, 3f);
        }
    }

    private void Awake()
    {
        player = GetComponentInParent<PlayerScript>();
        Abilityname = "Pisotón furioso";
        description = "crea un temblor que ralentiza a todos los enemigos";
        cooldown = 8f;
        timer = 0f;
        isUsable = true;
        cost = 10;
        slowAmount = 5f;
        radius = 15;
        
    }
}
