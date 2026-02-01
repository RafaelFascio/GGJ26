using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ManagerScript : MonoBehaviour
{
    public GameObject panelDerrota;
    public GameObject panelPausa;
    public GameObject panelVictoria;
    public bool enPausa=false;

    InputAction pausa;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausa = InputSystem.actions.FindAction("Pausar");
        Reanudar();
    }

    // Update is called once per frame
    void Update()
    {
        if (pausa.triggered & !enPausa)
        {
            PanelPausa();
        }
    }

    public void PanelPausa()
    {
        panelPausa.SetActive(true);
        Pausa();
    }
    public void CerrarPanelPausa()
    {
        panelPausa.SetActive(false);
        Reanudar();
    }
    public void PanelDerrota()
    {
        panelDerrota.SetActive(true);
        Pausa();
    }
    public void PanelVictoria()
    {
        panelVictoria.SetActive(true);
        Pausa();
    }
    public void CargarEscena(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Reanudar();
    }

    public void Pausa()
    {
        enPausa=true;
        Time.timeScale = 0f;
    }

    public void Reanudar()
    {
        enPausa = false;
        Time.timeScale = 1.0f;
    }
}
