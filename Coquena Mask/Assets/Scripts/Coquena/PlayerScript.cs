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
    #endregion

    #region Stats
    int maxHealth;
    int currentHealth;
    float moveSpeed;
    int maskIndex;
    float stunresist;

    #endregion

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        changeMask = InputSystem.actions.FindAction("ChangeMask");
        attack = InputSystem.actions.FindAction("Attack");
        useAbility1 = InputSystem.actions.FindAction("UseAbility1");
        useAbility2 = InputSystem.actions.FindAction("UseAbility2");
    }
    void Start()
    {

        maskIndex = 0;
        ChangeMask(maskIndex);
        move = GetComponent<Move>();
        moveSpeed = 5.0f;
    }

    
    void Update()
    {
       
        if (!currentMask.isAtacking()) 
        {
            move.MoveCharacter(moveSpeed);
            if (attack.triggered)
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

            //if (changeMask.ReadValue<Vector2>().y != 0)
            //{
            //    maskIndex += (int)changeMask.ReadValue<Vector2>().y;
            //    maskIndex =Mathf.Abs( ((int)changeMask.ReadValue<Vector2>().y +maskIndex) % masks.Count);
            //    ChangeMask(Mathf.Abs(maskIndex % masks.Count));
            //}
            
            float rawScroll = changeMask.ReadValue<Vector2>().y;
            if (!Mathf.Approximately(rawScroll, 0f) && masks.Count > 0)
            {
                int direction = rawScroll > 0f ? 1 : -1; // solo 1 o -1    
                maskIndex = (maskIndex + direction + masks.Count) % masks.Count;
                ChangeMask(maskIndex);
            }
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
}
