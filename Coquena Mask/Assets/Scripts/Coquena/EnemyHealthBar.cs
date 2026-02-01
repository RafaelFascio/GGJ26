using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public TextMeshPro damageText;
    [SerializeField] SpriteRenderer healthBarFill;
    [SerializeField] Vector2 widthRange;
    Vector2 size;
    Enemy enemy;
    public bool showDamage;
    public float timer;
    public float damageTimer;
    public float damageAmount;

    void Start()
    {
        size = healthBarFill.size;
        widthRange = healthBarFill.size;
        enemy = GetComponentInParent<Enemy>();
        showDamage =false;
        damageTimer = 3;
        damageText.enabled = false;
        Debug.Log(enemy == null);
    //    CheckHealth();
    }
    void Update()
    {
        CheckHealth();
    }

    void CheckHealth() 
    {
        float percentage = enemy.currentHp / enemy.maxHp;
        size.x = widthRange.x * percentage;
        
        healthBarFill.size = size;
    }
    public IEnumerator ShowText()
    {
        damageText.enabled = true;
        showDamage = true;
        while (timer < damageTimer)
        {

            timer += Time.deltaTime;
            yield return null;
        }
        showDamage = false;
        damageAmount = 0;
        damageText.enabled = false;

    }
}
