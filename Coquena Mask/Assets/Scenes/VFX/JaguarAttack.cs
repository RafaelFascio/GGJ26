using UnityEngine;

public class JaguarAttack : MonoBehaviour
{
    private int hitCounter = 0;

    public float bleedDuration = 5f;
    public float bleedDamage = 2f;
    public float bleedTickRate = 1f;

    void OnHitEnemy(GameObject enemy)
    {
        hitCounter++;

        enemy.GetComponent<Health>()?.TakeDamage(10f);

        if (hitCounter >= 3)
        {
            hitCounter = 0;

            var status = enemy.GetComponent<StatusEffectController>();
            status?.ApplyBleed(bleedDuration, bleedDamage, bleedTickRate);

            // ACA instanciás Bleed_Impact VFX
        }
    }
}
