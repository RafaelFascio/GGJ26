using UnityEngine;
using UnityEngine.UI;

public class BarraVidaUi : MonoBehaviour
{
    [SerializeField] private Slider barraSlider;
    [SerializeField] private Enemy vidaEnemy;

    private void Start()
    {
        vidaEnemy = FindFirstObjectByType<Enemy>();
        vidaEnemy.EnemyTakeDamage += CambiarBarraVida;
        IniciarBarraVida(vidaEnemy.maxHp, vidaEnemy.currentHp);
    }

    void OnDisable()
    {
        vidaEnemy.EnemyTakeDamage -= CambiarBarraVida;
    }

    private void IniciarBarraVida(float vidaMax, float currentVida)
    {
        barraSlider.maxValue = vidaMax;
        barraSlider.value = currentVida;

    }

    private void CambiarBarraVida(float currentVida)
    {
        barraSlider.value = currentVida;
    }
}
