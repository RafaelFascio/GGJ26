using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CazadorInexperto : MonoBehaviour
{
    public ZonaDeAlerta alertZone;

    public Animator animator;
    public enum EstadosCazador
    {
        DESCANSANDO,
        DISPARAR,
        HUIDA,
        VOLVER,
        MUERTO
    }

    public EstadosCazador currentState = EstadosCazador.DESCANSANDO;

    public NavMeshAgent agent;
    public Transform player;
    public Transform puntoDeDescanso;
    public Transform pistola;

    public float tiempoEntreDisparos = 0.8f;
    public int minShoots = 2;
    public int maxShoots = 4;

    public float minDistance = 4f;
    public float maxDistance = 10f;
    public float minTime = 3f;
    public float maxTime = 5f;

    private bool playerEnZona = false;
    private bool playerMuerto = false;
    private Coroutine estadoRutina;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alertZone.RegistrarEnemigo(this);
        agent.isStopped = true;
        transform.position = puntoDeDescanso.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EstadosCazador.DISPARAR)
        {
            MirarPlayer();
        }
    }


    public void PlayerEnZona(Transform playerTransform)
    {
        if (currentState == EstadosCazador.MUERTO) return;

        player = playerTransform;
        playerEnZona = true;

        ChangeState(EstadosCazador.DISPARAR);
    }

    public void PlayerFueraDeZona()
    {
        if (currentState == EstadosCazador.MUERTO) return;

        playerEnZona = false;
        ChangeState(EstadosCazador.VOLVER);
    }

    public void PlayerMurio()
    {
        playerMuerto = true;
        ChangeState(EstadosCazador.VOLVER);
    }


    void ChangeState(EstadosCazador newState)
    {
        animator.SetBool("run", false);
        animator.SetBool("walk", false);
        if (estadoRutina != null)
            StopCoroutine(estadoRutina);

        currentState = newState;

        switch (currentState)
        {
            case EstadosCazador.DISPARAR:
                estadoRutina = StartCoroutine(EstadoDisparar());
                break;

            case EstadosCazador.HUIDA:
                estadoRutina = StartCoroutine(EstadoHuida());
                break;

            case EstadosCazador.VOLVER:
                estadoRutina = StartCoroutine(EstadoVolver());
                break;

            case EstadosCazador.MUERTO:
                Muerte();
                break;
        }
    }


    IEnumerator EstadoDisparar()
    {
        MirarPlayer();
        agent.isStopped = true;

        int disparos = Random.Range(minShoots, maxShoots + 1);

        for (int i = 0; i < disparos; i++)
        {
            if (!playerEnZona || playerMuerto) break;
            //animator.SetTrigger("shot");
            Disparar();
            yield return new WaitForSeconds(tiempoEntreDisparos);
        }

        if (playerEnZona)
            ChangeState(EstadosCazador.HUIDA);
        else
            ChangeState(EstadosCazador.VOLVER);
    }

    IEnumerator EstadoHuida()
    {
        agent.isStopped = false;

        Vector3 fleeDir = (transform.position - player.position).normalized;
        fleeDir += Random.insideUnitSphere;
        fleeDir.y = 0;

        float fleeDistance = Random.Range(minDistance, maxDistance);
        Vector3 targetPos = transform.position + fleeDir.normalized * fleeDistance;

        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, fleeDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            ChangeState(EstadosCazador.DISPARAR);
            yield break;
        }

        animator.SetBool("run", true);

        //float fleeTime = Random.Range(minTime, maxTime);
        //yield return new WaitForSeconds(fleeTime);
        //animator.SetBool("run", false);
        Vector3 lastPosition = transform.position;
        float traveledDistance = 0f;

        // Esperar hasta llegar o recorrer la distancia
        while (true)
        {
            if (!agent.pathPending)
            {
                // Llegó al destino
                if (agent.remainingDistance <= agent.stoppingDistance)
                    break;
            }

            // Calcular distancia recorrida
            traveledDistance += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;

            if (traveledDistance >= fleeDistance)
                break;

            yield return null;
        }

        agent.isStopped = true;

        if (playerEnZona)
            ChangeState(EstadosCazador.DISPARAR);
        else
            ChangeState(EstadosCazador.VOLVER);
    }

    IEnumerator EstadoVolver()
    {
        agent.isStopped = false;
        agent.SetDestination(puntoDeDescanso.position);
        animator.SetBool("walk", true);

        while (Vector3.Distance(transform.position, puntoDeDescanso.position) > 1f)
        {
            yield return null;
        }

        agent.isStopped = true;
        //animator.SetBool("walk", false);
        ChangeState(EstadosCazador.DESCANSANDO);
    }

    void Muerte()
    {
        agent.isStopped = true;
        animator.SetTrigger("muerte");
    }


    void Disparar()
    {
        animator.SetTrigger("shot");
        pistola.GetComponent<GeneradorBalas>().Fire();
    }

    void MirarPlayer()
    {
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}
