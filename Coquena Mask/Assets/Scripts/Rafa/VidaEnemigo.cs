using UnityEngine;
using UnityEngine.UIElements;

public class VidaEnemigo : MonoBehaviour
{
    public int vidaMax;
    private int currentVida;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentVida = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restarVida(int danio)
    {
        currentVida = currentVida - danio;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bala"))
        {
            restarVida(other.GetComponent<BalaScript>().danio);
            Debug.Log("RRRRRRRRRRR");
        }
    }
}
