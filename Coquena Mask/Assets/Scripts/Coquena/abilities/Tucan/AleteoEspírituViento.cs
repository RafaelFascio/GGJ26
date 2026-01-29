using UnityEngine;
//Aleteo del Espíritu del viento: lanza un tornado que daña en área y ralentiza al que lo toque
public class AleteoEspírituViento : Ability
{

    public int damage;
    public float slowAmount;
    public GameObject tornadoPrefab;
    public float tornadoSpeed;
    public Transform spawnPoint;
    Vector3 direction;
    public override void Use()
    {
        player.currentState = PlayerScript.State.Casting;
        direction = player.move.GetLookDirection();
        TornadoProyectile script = Instantiate(tornadoPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<TornadoProyectile>();
        script.direction = direction;
        script.proyectileSpeed = tornadoSpeed;
        player.currentState = PlayerScript.State.Idle;
        
    }

    private void Awake()
    {
        player = GetComponentInParent<PlayerScript>();
        Abilityname = "Aleteo del Espíritu del viento";
        damage = 25;
        description = "lanza un tornado que daña en área y ralentiza al que lo toque";
        cooldown = 8f;
        timer = 0f;
        isUsable = true;
        cost = 10;
        slowAmount =5f;
        tornadoSpeed = 13f;
    }

    
}
