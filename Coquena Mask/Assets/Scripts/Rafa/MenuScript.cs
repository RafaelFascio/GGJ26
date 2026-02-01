using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    

    public void CargarEscena(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
