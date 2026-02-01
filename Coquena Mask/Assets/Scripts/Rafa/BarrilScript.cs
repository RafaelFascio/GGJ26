using UnityEngine;

public class BarrilScript : MonoBehaviour
{
    [SerializeField] private ManagerScript manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindFirstObjectByType<ManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.PanelDerrota();
        }
    }
}
