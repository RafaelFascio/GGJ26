using System.Collections;
using UnityEngine;

public class TornadoProyectile : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float proyectileSpeed;
    [HideInInspector] public float damage;
    float duration;
    void Start()
    {
        proyectileSpeed = 15f;
        damage = 20f;
        duration = 2f;
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * proyectileSpeed * direction;
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
            //Slow
        }
        //    Destroy(gameObject);
    }
}
