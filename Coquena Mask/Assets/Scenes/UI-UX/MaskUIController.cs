using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MaskUIController : MonoBehaviour
{
    public Image maskIcon;
    public Image glow;
    UIAnimationHelper anim;

    void Awake()
    {
        anim = GetComponent<UIAnimationHelper>();
    }

    void OnEnable()
    {
        PlayerScript.OnMaskChanged += HandleMaskChanged;
    }

    void OnDisable()
    {
        PlayerScript.OnMaskChanged -= HandleMaskChanged;
    }

    void HandleMaskChanged(Mask newMask)
    {
        // Cambiar icono
        maskIcon.sprite = newMask.icon;

        // Color según tipo
        Color typeColor = newMask.maskColor;
        glow.color = new Color(typeColor.r, typeColor.g, typeColor.b, 0.4f);

        // 🎬 Animación por código
        anim.AnimateScale(Vector3.one * 0.8f, Vector3.one, 0.15f);
        anim.AnimateColor(glow, Color.clear, glow.color, 0.2f);
    }
}
