using UnityEngine;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour
{
    
    void Start()
    {
        Invoke("CambiarEscena", 8.5f);
    }

    void CambiarEscena()
    {
        SceneManager.LoadScene("Nivel 1");
    }
}
