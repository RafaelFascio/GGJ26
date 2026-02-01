using TMPro;
using UnityEngine;


public class StatsManager : MonoBehaviour
{
    PlayerScript playerScript;
    public TextMeshProUGUI textMaxHp;
    public TextMeshProUGUI textcurrentHp;
    public TextMeshProUGUI textMaxMana;
    public TextMeshProUGUI textCurrentMana;
    float hpPercetenge;
    float manaPercetenge;
    private void Awake()
    {
        playerScript = FindFirstObjectByType<PlayerScript>();
    }

    
    private void FixedUpdate()
    {
        textMaxHp.text = playerScript.maxHealth.ToString();
        textMaxMana.text = playerScript.maxMana.ToString();
        textCurrentMana.text = playerScript.currentMana.ToString();
        textcurrentHp.text   = playerScript.currentHealth.ToString();
    }
}
