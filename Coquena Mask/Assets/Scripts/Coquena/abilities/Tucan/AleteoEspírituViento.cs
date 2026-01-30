using UnityEngine;
//Aleteo del Espíritu del viento: lanza un tornado que daña en área y ralentiza al que lo toque
public class AleteoEspírituViento : Ability
{
    #region Tornado Stats
    public int damage;
    public float slowAmount;      
    public float slowDuration;
    public float tornadoSpeed;
    public float tornadoDuration;
    #endregion
    public GameObject tornadoPrefab;
    public Transform spawnPoint;
    Vector3 direction;
    public override void Use()
    {
        player.currentState = PlayerScript.State.Casting;
        direction = player.move.MousePosition()-spawnPoint.position;
        direction.Normalize();
        TornadoProyectile script = Instantiate(tornadoPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<TornadoProyectile>();
        script.direction = direction;      
        script.damage = damage;
        script.slowAmount = slowAmount;
        script.slowDuration = slowDuration;
        script.tornadoSpeed = tornadoSpeed;
        script.tornadoDuration = tornadoDuration;
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
        slowDuration = 4f;
        tornadoSpeed = 13f;
        tornadoDuration = 3f;
        
    }

    
}
