using System.Collections;
using UnityEngine;

public class FeatherProyectile : MonoBehaviour
{
    public Vector3 direction;
    float proyectileSpeed;
    public float damage;
    float duration;
    private void Start()
    {
        proyectileSpeed = 15f;
        damage = 10f;
        duration = 2f;
        StartCoroutine(DestroyAfterTime());
    }
    private void Update()
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
            
            //Hago daño
           
        }
        //    Destroy(gameObject);
    }
}
