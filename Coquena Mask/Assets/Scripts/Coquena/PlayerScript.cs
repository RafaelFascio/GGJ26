using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    #region components
    public List<GameObject> masks = new List<GameObject>();
    CharacterController controller;
    [HideInInspector] public Move move;
    [HideInInspector] public Mask currentMask;
    #endregion

    #region InputActions
    InputAction changeMask;
    InputAction attack;
    InputAction useAbility1;
    InputAction useAbility2;
    InputAction dash;
    float rawScroll;
    #endregion

    #region Stats
    int maxHealth;
    int currentHealth;
    float moveSpeed;
    int maskIndex;
    float dashSpeed;
    float dashDuration;
    float dashCooldown;
    float dashTimer;
    [HideInInspector] public float damageresist;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool canbeDamaged;
    [HideInInspector] public bool canDash;
    #endregion

    public enum State 
    {
        Idle,Moving,Stunned,Casting,Dashing
    }
    public State currentState;
    private void Awake()
    {
        move = GetComponent<Move>();
        controller = GetComponent<CharacterController>();       
        changeMask = InputSystem.actions.FindAction("ChangeMask");
        attack = InputSystem.actions.FindAction("Attack");
        useAbility1 = InputSystem.actions.FindAction("UseAbility1");
        useAbility2 = InputSystem.actions.FindAction("UseAbility2");
        dash = InputSystem.actions.FindAction("Dash");
        
    }
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;      
        currentState = State.Idle;
        maskIndex = 0;
        ChangeMask(maskIndex);
        moveSpeed = 7.0f;
        canMove =true;
        canAttack = true;
        canbeDamaged = true;
        damageresist = 0f; 
        canDash = false;
        dashSpeed = 35f;
        dashDuration = .4f; 
    }
    void Update()
    {

        if (currentState != State.Stunned )
        {
            if (dash.triggered && canDash)
            {
                Dash();
            }
            if (canMove) 
            {
                move.MoveCharacter(moveSpeed);
            }
            
            if (attack.triggered && canAttack)
            {
                currentMask.Attack();
            }

            if (useAbility1.triggered)
            {
                currentMask.UseFirstAbility();
            }
            if (useAbility2.triggered)
            {
                currentMask.UseSecondAbility();
            }
            
            rawScroll = changeMask.ReadValue<Vector2>().y;
            if (!Mathf.Approximately(rawScroll, 0f) && masks.Count > 0)
            {
                int direction = rawScroll > 0f ? 1 : -1; // solo 1 o -1    
                maskIndex = (maskIndex + direction + masks.Count) % masks.Count;
                ChangeMask(maskIndex);
            }
        }

    }
    void Dash() 
    {     
        if (dashTimer > 0f) return; 
        StartCoroutine(StartDash());
    }
    IEnumerator StartDash() 
    {
        canMove = false;
        canAttack = false;
        currentState = State.Dashing;
        float elapsed = 0f;
        
        while (elapsed < dashDuration) 
        {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canMove = true;
        canAttack = true;
        currentState = State.Idle;
        dashTimer = dashCooldown;        
        StartCoroutine(ResetDash());
    }
    IEnumerator ResetDash() 
    {
        while (dashTimer > 0) 
        {
            dashTimer -= Time.deltaTime;
            yield return null;
        }
        dashTimer = 0f;

    }
    IEnumerator WaitForStun(float duration) 
    {
        currentState = State.Stunned;
        yield return new WaitForSeconds(duration);
        currentState = State.Idle;
    }
    public void TakeDamage(int dmg) 
    {
        if (canbeDamaged && currentState != State.Dashing)
        {
            currentHealth -= (int)(dmg * (1 - damageresist));
            Debug.Log("Player took " + dmg + " damage. Current health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Debug.Log("Player is dead.");
                // Aquí puedes agregar lógica adicional para manejar la muerte del jugador
            }
        }
    }
    public void PushPlayer() 
    {
        // Empuja al jugador dependendo de la dirección del ataque recibido
        //soonTM

    }

    void ChangeMask(int i) 
    {
        Debug.Log("Changing to mask index: " + i);
        for (int j = 0; j < masks.Count; j++) 
        {
            if (j == i) 
            {
                masks[j].SetActive(true);
            }
            else 
            {
                masks[j].SetActive(false);
            }
        }
    }
}
