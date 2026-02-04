using UnityEngine;

public class Sonidos : MonoBehaviour
{
    public AudioSource source;
    public AudioClip disparo;

    public void ReproducirSonido(AudioClip sound) 
    { 
        source.PlayOneShot(sound); 
    }
    public void ReproducirDisparo()
    {
        source.PlayOneShot(disparo);
    }
}
