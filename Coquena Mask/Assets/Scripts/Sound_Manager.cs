using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public AudioSource[] listaAudios;

    public void Pausar_Audios()
    {
        foreach (AudioSource audio in listaAudios) { audio.Pause(); }
    }

    public void Play_Audios()
    {
        foreach (AudioSource audio in listaAudios) { audio.UnPause(); }
    }
}
