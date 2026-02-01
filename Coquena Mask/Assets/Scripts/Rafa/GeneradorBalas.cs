using UnityEngine;

public class GeneradorBalas : MonoBehaviour
{
    public GameObject balaPrefab;

    public void Fire()
    {
        Instantiate(balaPrefab, transform.position, transform.rotation);
    }
}
