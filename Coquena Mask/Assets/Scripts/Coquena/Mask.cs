using System.Collections;
using UnityEngine;

public abstract class Mask :MonoBehaviour
{
    public Animator animator;
   [HideInInspector] public PlayerScript player;
   [HideInInspector] public Collider hitbox;
    /// <summary>
    ///   rapidez de ataque
    /// </summary>
    public float attackRate;
    /// <summary>
    /// tiempo en el que el hitbox esta activo
    /// </summary>
    public float attackDuration;
    /// <summary>
    /// tiempo hasta el proximo ataque
    /// </summary>
    public float nextAttackTime;
    /// <summary>
    /// Si esta atacando o no.
    /// </summary>
    public bool attacking;
    public float attackDamage;
    public int attackCount;
    public abstract void Attack();
    public abstract void UseFirstAbility();
    public abstract void UseSecondAbility();
    public abstract bool isAtacking();
    public IEnumerator EnableHitCollider(float duration)
    {

        attacking = true;
        hitbox.enabled = true;
        yield return new WaitForSeconds(duration);
        hitbox.enabled = false;
        attacking = false;
        if (attackCount >= 3)
        {
            attackCount = 0;
        }
    }
}
