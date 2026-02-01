using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class LlamaScript : MonoBehaviour
{
    public Transform pistola;
    InputAction shot;
    InputAction move;

    [Header("Velocidades")]
    public float forwardSpeed = 5f;
    public float sideSpeed = 3f;
    public float manualForwardSpeed = 3f;

    [Header("Rotación")]
    public float tiltAngle = 10f;
    public float tiltSpeed = 5f;

    private NavMeshAgent agent;
    private Quaternion originalRotation;
    private float currentTilt = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shot = InputSystem.actions.FindAction("Dash");
        move = InputSystem.actions.FindAction("Move");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (shot.triggered)
        {
            Disparar();
        }

        Vector3 forwardMove = transform.forward * forwardSpeed;

        
        float sideInput = 0f;
        sideInput = (move.ReadValue<Vector2>().y) * -1;

        //if (move.x)
        //    sideInput = -1f;
        //else if (Input.GetKey(KeyCode.S))
        //    sideInput = 1f;

        //Vector3 sideMove = transform.right * sideInput * sideSpeed;
        forwardMove += transform.right * sideInput * sideSpeed;

        float forwardInput = 0f;
        forwardInput = (move.ReadValue<Vector2>().x);
        forwardMove += transform.forward * forwardInput * manualForwardSpeed;

        //agent.velocity = forwardMove + sideMove;
        agent.velocity = forwardMove;


        float targetTilt = sideInput * tiltAngle;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

        Quaternion tiltRotation = Quaternion.Euler(0, originalRotation.eulerAngles.y + currentTilt, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, tiltRotation, Time.deltaTime * tiltSpeed);
    }

    void Disparar()
    {
        pistola.GetComponent<GeneradorBalas>().Fire();
    }
}
