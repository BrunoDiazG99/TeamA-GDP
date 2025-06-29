using System.Collections;
using UnityEngine;

public class BombPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("This is a player, giving bomb");

            GameEvents.current.BombPickup();

            Destroy(gameObject);

        }
    }

}
