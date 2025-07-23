using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    private bool alreadyTriggered = false;
    [SerializeField] private Transform teleportDestination; // 👈 posición a la que se moverá el jugador

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !alreadyTriggered)
        {
            alreadyTriggered = true;

            // Teletransportar al jugador al nuevo punto
            collision.transform.position = teleportDestination.position;

        }
    }
}
