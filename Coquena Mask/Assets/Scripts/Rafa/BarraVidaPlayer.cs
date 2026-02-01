using UnityEngine;
using UnityEngine.UI;

public class BarraVidaPlayer : MonoBehaviour
{
    [SerializeField] private Slider barraSlider;
    [SerializeField] private PlayerScript vidaPlayer;

    private void Start()
    {
        vidaPlayer = FindFirstObjectByType<PlayerScript>();
        vidaPlayer.playerTakeDamage += CambiarBarraVida;
        IniciarBarraVida(vidaPlayer.maxHealth, vidaPlayer.currentHealth);
    }

    void OnDisable()
    {
        vidaPlayer.playerTakeDamage -= CambiarBarraVida;
    }

    private void IniciarBarraVida(int vidaMax, int currentVida)
    {
        barraSlider.maxValue = vidaMax;
        barraSlider.value = currentVida;

    }

    private void CambiarBarraVida(int currentVida)
    {
        barraSlider.value = currentVida;
    }
}
