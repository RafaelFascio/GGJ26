using UnityEngine;

public class Sonidos : MonoBehaviour
{
    public AudioSource source;
    public AudioClip disparo;


    public void ReproducirDisparo()
    {
        source.PlayOneShot(disparo);
    }
}
