using System.Collections;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour
{
    public Action<float> EnemyTakeDamage;
    public float maxHp;
    public float currentHp;
    public float speed;
    public int yaguareteHitCount;
    //public GameObject hitVFXPrefab;
    //public GameObject bledPrefab;
    private bool isFlashing;
    private Color color;
    GameObject vfx;
    //private Vector3 hitPoint;
    public virtual void TakeDamage(float damage)
    {
        //color = Color.white;
        //StartCoroutine(HitFlash(GetComponent<Renderer>()));
        float vidaTemporal = currentHp - damage;
        vidaTemporal = Mathf.Clamp(vidaTemporal, 0, maxHp);
        currentHp = vidaTemporal;
        EnemyTakeDamage?.Invoke(currentHp);
        if (currentHp <= 0)
        {
            Destroy(vfx);
            Die();

        }
    }
    public virtual void ApplySlow(float amount, float duration)
    {
        StartCoroutine(ChangeSpeed(amount, duration));
    }
    public virtual void ApplyDamageOverTime(float damagePerTick, float duration, float tick)
    {
        float dur = duration;
        StartCoroutine(DoDamageOverTime(damagePerTick, duration, tick));
        SpawnBoodVFX(transform.position, transform.forward, duration); // Removed extra closing parenthesis
    }


    public virtual void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator ChangeSpeed(float amount, float duration)
    {
        speed -= amount;
        yield return new WaitForSeconds(duration);
        speed += amount;
    }
    IEnumerator DoDamageOverTime(float damagePerTick, float duration, float tick)
    {
        color = Color.red;

        float elapsed = 0f;
        do
        {
            TakeDamage(damagePerTick);
            StartCoroutine(HitFlash(GetComponent<Renderer>()));
            yield return new WaitForSeconds(tick);
            elapsed += 1f;
        } while (elapsed < duration);
    }
    public void SpawnHitVFX(Vector3 hitPoint, Vector3 hitDirection)
    {
        //GameObject vfx = Instantiate(hitVFXPrefab, hitPoint, Quaternion.LookRotation(hitDirection));
        Destroy(vfx, 0.5f);
    }
    public void SpawnBoodVFX(Vector3 hitPoint, Vector3 hitDirection,float duration)
    {
        //vfx = Instantiate(bledPrefab, hitPoint, Quaternion.LookRotation(hitDirection));
        Destroy(vfx, duration);
    }
    IEnumerator HitFlash(Renderer rend)
    {
        if (isFlashing) yield break;

        isFlashing = true;

        Color original = rend.material.color;
        rend.material.color = color;

        yield return new WaitForSeconds(0.08f);

        rend.material.color = original;
        isFlashing = false;
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamageObject"))
        {

            color = Color.white;
            StartCoroutine(HitFlash(GetComponent<Renderer>()));
            SpawnHitVFX(other.ClosestPoint(transform.position), other.transform.forward);
        }
    }
}