using UnityEngine;

public class LamaMonoBehaviour : MonoBehaviour
{
    public ManagerScript manager;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.PanelVictoria();
        }
    }
}
