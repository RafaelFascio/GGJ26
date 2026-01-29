using System.Collections;
using UnityEngine;

public class WindGustProyectile : MonoBehaviour
{
    public Vector3 direction;
    float proyectileSpeed;
    float duration;
    public float damage;
    void Start()
    {
     proyectileSpeed = 25f;
     duration = 3;
     damage = 15f;
     StartCoroutine(DestroyAfterTime());
    }

   
    void Update()
    {
       transform.position += proyectileSpeed * Time.deltaTime * direction.normalized;
    }
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Tornado hit an enemy!");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            
            //Hago daño
            //Slow
        }

    }
}
