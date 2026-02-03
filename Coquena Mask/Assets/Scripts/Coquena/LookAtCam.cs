using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    [Tooltip("Si está activado, el objeto mantendrá la rotación mundial inicial (independiente de la rotación del padre).")]
    public bool maintainInitialRotation = true;

    [Tooltip("Vector 'up' (no usado cuando se mantiene rotación inicial).")]
    public Vector3 worldUp = Vector3.up;

    // Rotación objetivo en espacio mundial que queremos mantener
    private Quaternion desiredWorldRotation;

    private void Awake()
    {
        Quaternion initialRotation = Camera.main.transform.rotation;
        transform.rotation= initialRotation;
        // Guardamos la rotación mundial inicial como la que queremos mantener
        desiredWorldRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (!maintainInitialRotation) return;

        // Aplicamos la rotación deseada en espacio mundial compensando la rotación del padre
        if (transform.parent != null)
        {
            transform.localRotation = Quaternion.Inverse(transform.parent.rotation) * desiredWorldRotation;
        }
        else
        {
            transform.rotation = desiredWorldRotation;
        }
    }

    // Método público para actualizar la rotación objetivo en tiempo de ejecución si lo necesitas
    public void SetDesiredWorldRotation(Quaternion worldRotation)
    {
        desiredWorldRotation = worldRotation;
    }
}