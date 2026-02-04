using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public AudioClip AudioClip;
    public AudioSource AudioSource;
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

        if (other.CompareTag("Jefe1"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
            AudioSource.PlayOneShot(AudioClip);
            Destroy(gameObject,1f);
        }
    }
}
