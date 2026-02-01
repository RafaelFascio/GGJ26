using UnityEngine;

public class GeneradorBalas : MonoBehaviour
{
    public Sonidos sonidos;
    public GameObject balaPrefab;

    public void Fire()
    {
        Instantiate(balaPrefab, transform.position, transform.rotation);
        sonidos.ReproducirDisparo();

    }
}
