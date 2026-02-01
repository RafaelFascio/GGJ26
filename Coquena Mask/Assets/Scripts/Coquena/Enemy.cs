using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    public float speed;
    public int yaguareteHitCount;
    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }
    public virtual void ApplySlow(float amount, float duration) 
    {
        StartCoroutine(ChangeSpeed(amount, duration));
    }
    public virtual void ApplyDamageOverTime(float damagePerTick, float duration, float tick) 
    {
        StartCoroutine(DoDamageOverTime(damagePerTick, duration, tick));
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator ChangeSpeed(float amount,float duration) 
    {
        speed -= amount;
        yield return new WaitForSeconds(duration);
        speed += amount;
    }
    IEnumerator DoDamageOverTime(float damagePerTick,float duration,float tick)
    {
        float elapsed = 0f;
        do
        {
            TakeDamage(damagePerTick);
            yield return new WaitForSeconds(tick);
            elapsed += 1f;
        } while (elapsed < duration);     
    }
}
