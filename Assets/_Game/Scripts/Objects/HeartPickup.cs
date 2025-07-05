using System.Collections;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private PlayerInfoManager playerInfoManager;

    private void Awake()
    {
        playerInfoManager = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerInfoManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("This is a player, giving heart if possible");

            // Check for full health
            bool hasFullHealth = playerInfoManager.HasFullHealth();

            if (hasFullHealth) return;

            GameEvents.current.HeartPickup();
            AudioManager.instance.PlaySound("sfx_pickup");

            Destroy(gameObject);

        }
    }

}
