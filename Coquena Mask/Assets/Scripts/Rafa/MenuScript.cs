using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void CargarEscena(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
