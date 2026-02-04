using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationHelper : MonoBehaviour
{
    public Coroutine currentAnim;

    public void AnimateScale(Vector3 from, Vector3 to, float duration)
    {
        if (currentAnim != null) StopCoroutine(currentAnim);
        currentAnim = StartCoroutine(ScaleRoutine(from, to, duration));
    }

    IEnumerator ScaleRoutine(Vector3 from, Vector3 to, float duration)
    {
        float t = 0;
        transform.localScale = from;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(from, to, t);
            yield return null;
        }

        transform.localScale = to;
    }

    public void AnimateColor(Image img, Color from, Color to, float duration)
    {
        StartCoroutine(ColorRoutine(img, from, to, duration));
    }

    IEnumerator ColorRoutine(Image img, Color from, Color to, float duration)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            img.color = Color.Lerp(from, to, t);
            yield return null;
        }
        img.color = to;
    }
}
