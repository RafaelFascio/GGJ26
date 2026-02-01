using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ability :MonoBehaviour
{
    public PlayerScript player;
    private static WaitForSeconds _waitForSeconds1 = new(1);
    public string Abilityname;
    public string description;
    public Sprite icon;
    public float cooldown;
    public float timer;
    public bool isUsable;
    public int cost;
    public IEnumerator SetCooldown()
    {
        
        isUsable = false;
        while (timer > 0)
        {
            Debug.Log("Cooldown timer: " + timer);
            yield return _waitForSeconds1;
            timer--;
        }
        Debug.Log("ability ready");
        isUsable = true;
    }
    public abstract void Use();
    public void Activate()
    {
        if (isUsable)
        {
            if (player.currentMana > cost)
            {
                Use();
                timer = cooldown;
                player.currentMana -= cost;
                player.StartCoroutine(SetCooldown());
                player.StartCoroutine(player.StartManaRegen());
            }
            else 
            {
                Debug.Log("Not enough mana to use " + Abilityname + ". Current mana: " + player.currentMana + ", required mana: " + cost);
            }
           
            
        }
        else
        {
            Debug.Log("Modifier " + Abilityname + " is on cooldown. Time left: " + timer + " seconds.");
        }
    }
    void OnEnable()
    {
        isUsable = true;
        timer = 0;
        
    }
    private void Awake()
    {
        timer = 0;
    }
}
