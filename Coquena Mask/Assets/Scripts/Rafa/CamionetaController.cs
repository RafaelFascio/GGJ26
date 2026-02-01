using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CamionetaController : Enemy
{
    [Header("Movimiento")]
    public float forwardSpeed = 6f;
    public float sideSpeed = 2f;
    public float sideChangeTime = 2f;

    [Header("Rotación")]
    public float tiltAngle = 8f;
    public float tiltSpeed = 4f;

    [Header("Lanzar Obstáculos")]
    public GameObject obstaclePrefab;
    public Transform spawnPoint;
    public float minThrowTime = 3f;
    public float maxThrowTime = 5f;
    public float throwForce = 5f;

    private NavMeshAgent agent;
    private float sideDirection;
    private Quaternion originalRotation;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        originalRotation = transform.rotation;

        StartCoroutine(ChangeSideMovement());
        StartCoroutine(ThrowObstacleRoutine());
    }

    void Update()
    {
        Vector3 forwardMove = transform.forward * forwardSpeed;
        Vector3 sideMove = transform.right * sideDirection * sideSpeed;

        agent.velocity = forwardMove + sideMove;

        // Rotación suave al moverse lateralmente
        float targetTilt = sideDirection * tiltAngle;
        Quaternion targetRot = Quaternion.Euler(
            0,
            originalRotation.eulerAngles.y + targetTilt,
            0
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRot,
            Time.deltaTime * tiltSpeed
        );
    }

    IEnumerator ChangeSideMovement()
    {
        while (true)
        {
            sideDirection = Random.Range(-1f, 1f);
            yield return new WaitForSeconds(sideChangeTime);
        }
    }

    IEnumerator ThrowObstacleRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minThrowTime, maxThrowTime);
            yield return new WaitForSeconds(waitTime);

            ThrowObstacle();
        }
    }

    void ThrowObstacle()
    {
        if (obstaclePrefab == null || spawnPoint == null) return;

        GameObject obj = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(-transform.forward * throwForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPath"))
        {
            Destroy(gameObject); // o desactivar, animar, etc
        }
    }
}
