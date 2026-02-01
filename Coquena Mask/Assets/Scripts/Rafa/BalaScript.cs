using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
    public float damage = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<PlayerScript>()?.TakeDamage(damage);

            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
