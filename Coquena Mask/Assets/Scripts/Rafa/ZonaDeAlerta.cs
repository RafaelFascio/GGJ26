using UnityEngine;
using System.Collections.Generic;

public class ZonaDeAlerta : MonoBehaviour
{
    private List<CazadorInexperto> enemigosEnZona = new List<CazadorInexperto>();

    public void RegistrarEnemigo(CazadorInexperto enemy)
    {
        if (!enemigosEnZona.Contains(enemy))
            enemigosEnZona.Add(enemy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entro enla zona");
            foreach (var enemy in enemigosEnZona)
            {
                enemy.PlayerEnZona(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player salio");
            foreach (var enemy in enemigosEnZona)
            {
                enemy.PlayerFueraDeZona();
            }
        }
    }
}
