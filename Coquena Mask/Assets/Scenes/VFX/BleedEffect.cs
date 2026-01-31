using UnityEngine;

public class BleedEffect : MonoBehaviour
{
    [Header("Bleed Settings")]
    public float damagePerTick = 2f;
    public float tickRate = 1f;

    private float timer;
    private float tickTimer;
    private bool isBleeding;

    private Health health;

    [Header("VFX")]
    public ParticleSystem bleedLoopVFX;
    public ParticleSystem bleedTickVFX;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (!isBleeding) return;

        timer -= Time.deltaTime;
        tickTimer -= Time.deltaTime;

        if (tickTimer <= 0f)
        {
            ApplyTick();
            tickTimer = tickRate;
        }

        if (timer <= 0f)
        {
            StopBleed();
        }
    }

    public void Apply(float duration, float damage, float rate)
    {
        damagePerTick = damage;
        tickRate = rate;

        timer = duration;
        tickTimer = 0f;

        if (!isBleeding)
        {
            isBleeding = true;
            bleedLoopVFX.Play();
        }
    }

    void ApplyTick()
    {
        health.TakeDamage(damagePerTick);
        bleedTickVFX.Play();
    }

    void StopBleed()
    {
        isBleeding = false;
        bleedLoopVFX.Stop();
    }
}
