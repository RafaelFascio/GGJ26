using UnityEngine;

public class GeneradorBalas : MonoBehaviour
{
    public GameObject balaPrefab;
    //public float balaSpeed = 20f;

    public void Fire()
    {
        Instantiate(balaPrefab, transform.position, transform.rotation);
        //GameObject bullet = Instantiate(balaPrefab, transform.position, transform.rotation);
        //bullet.GetComponent<Rigidbody>().linearVelocity = transform.forward * balaSpeed;
    }
}
