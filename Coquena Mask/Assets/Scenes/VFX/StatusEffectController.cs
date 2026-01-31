using UnityEngine;

public class StatusEffectController : MonoBehaviour
{
    private BleedEffect bleedEffect;

    void Awake()
    {
        bleedEffect = GetComponent<BleedEffect>();
    }

    public void ApplyBleed(float duration, float damagePerTick, float tickRate)
    {
        bleedEffect?.Apply(duration, damagePerTick, tickRate);
    }
}
