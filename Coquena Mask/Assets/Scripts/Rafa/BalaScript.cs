using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public int danio;
    public float speed = 20f;
    public float lifeTime = 5f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    public void hacerDanio()
    {

    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponent<VidaEnemigo>().restarVida(danio);
            other.GetComponent<Enemy>().TakeDamage(danio);
            //picoPerforadorAbility.hitObject = true;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        

        
    }
}
