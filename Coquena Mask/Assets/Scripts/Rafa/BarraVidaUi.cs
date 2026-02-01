using UnityEngine;
using UnityEngine.UI;

public class BarraVidaUi : MonoBehaviour
{
    [SerializeField] private Slider barraSlider;
    

    public void IniciarBarraVida(int vidaMax)
    {
        barraSlider.maxValue = vidaMax;
        barraSlider.value = vidaMax;

    }
}
