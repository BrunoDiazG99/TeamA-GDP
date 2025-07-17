using UnityEngine;
using System.Collections;
public class ShakePoint : MonoBehaviour
{
    private bool alreadyTriggered = false;
    public float intensidad;
    public float frecuencia;
    public float tiempo;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyTriggered)
        {
            cinemachineTemblarCamara.Instance.ShakeCam(intensidad, frecuencia, tiempo);
            alreadyTriggered = true;  // Solo activar una vez por zona
        }

    }
}
