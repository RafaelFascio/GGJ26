using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TornadoProyectile : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float tornadoSpeed;
    [HideInInspector] public float damage;
    [HideInInspector] public float slowAmount;
    [HideInInspector] public float slowDuration;
    [HideInInspector] public float tornadoDuration;
    void Start()
    {
       
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * tornadoSpeed * direction;
    }
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(tornadoDuration);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Tornado hit an enemy!");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            enemy.ApplySlow(slowAmount, slowDuration,enemy.GetComponent<NavMeshAgent>());
           
        }
        //    Destroy(gameObject);
    }
}
