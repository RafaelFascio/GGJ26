using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Action<int> playerTakeDamage;
    public ManagerScript manager;
    public GameObject ghostPrefab;
    [HideInInspector] public Sonidos sounds;
    TrailRenderer trailRenderer;
    //public Animator animator;
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

    float moveSpeed;
    int maskIndex;
    float dashSpeed;
    float dashDuration;
    float dashCooldown;
    float dashTimer;
    public int maxHealth;
    public int currentHealth;
    [HideInInspector] public float maxMana;
    [HideInInspector] public float currentMana;
    [HideInInspector] public float manaRegen;
    [HideInInspector] public bool isManaRegenerating;
    [HideInInspector] public float damageresist;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool canbeDamaged;
    [HideInInspector] public bool canDash;
    #endregion

    public enum State
    {
        Idle, Moving, Stunned, Casting, Dashing
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
        trailRenderer = GetComponent<TrailRenderer>();
        sounds = FindFirstObjectByType<Sonidos>();


    }
    void Start()
    {
        maxHealth = 100;
        maxMana = 125f;
        currentHealth = maxHealth;
        currentState = State.Idle;
        maskIndex = 0;
        ChangeMask(maskIndex);
        moveSpeed = 12.0f;
        canMove = true;
        canAttack = true;
        canbeDamaged = true;
        damageresist = 0f;
        canDash = false;
        dashSpeed = 35f;
        dashDuration = .4f;
        currentMana = maxMana;
        manaRegen = 0.5f;

    }
    public IEnumerator StartManaRegen()
    {
        if (!isManaRegenerating && manaRegen != currentMana) //evita iniciar multiples corrutinas
        {
            isManaRegenerating = true;
            WaitForSeconds waitTime = new(.7f);
            // Regenera mana cada 0.7 segundos hasta el maximo
            while (currentMana < maxMana)
            {
                currentMana += manaRegen;
                if (currentMana > maxMana) currentMana = maxMana;
                yield return waitTime;
            }
        }

    }
    void Update()
    {

        if (currentState != State.Stunned)
        {
            if (dash.triggered && canDash)
            {
                Debug.Log("dasheando");
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
            SpawnAfterImage();
            elapsed += Time.deltaTime;
            yield return null;
        }
        trailRenderer.emitting = false;
        ghostPrefab.SetActive(false);
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
    public void TakeDamage(float dmg)
    {
        if (canbeDamaged && currentState != State.Dashing)
        {
            currentHealth -= (int)(dmg * (1 - damageresist));
         //   Debug.Log("Player took " + dmg + " damage. Current health: " + currentHealth);
            playerTakeDamage?.Invoke(currentHealth);
            if (currentHealth <= 0)
            {
                manager.PanelDerrota();
                Debug.Log("Player is dead.");
                // Aqu? puedes agregar l?gica adicional para manejar la muerte del jugador
            }
        }
    }
    public void PushPlayer()
    {
        // Empuja al jugador dependendo de la direcci?n del ataque recibido
        //soonTM

        if (attack.triggered && canAttack)
        {
            currentMask.Attack();
            //animator.SetTrigger("punch");
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
    void SpawnAfterImage()
    {
        trailRenderer.emitting = true;
        ghostPrefab.SetActive(true);

        GameObject ghost = Instantiate(ghostPrefab, transform.position, transform.rotation);
        Destroy(ghost, 0.2f);
    }
}
