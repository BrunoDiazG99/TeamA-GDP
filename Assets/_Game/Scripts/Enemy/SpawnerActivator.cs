using UnityEngine;

public class SpawnerActivator : MonoBehaviour
{
    [SerializeField] private GameObject spawnerToActivate_1;
    [SerializeField] private GameObject spawnerToActivate_2;
    [SerializeField] private GameObject spawnerToDeactivate_1;
    [SerializeField] private GameObject spawnerToDeactivate_2;
    private bool alreadyActivated = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !alreadyActivated)
        {
            if (spawnerToActivate_1 != null || spawnerToActivate_2 != null)
            {
                spawnerToActivate_1.SetActive(true);
                spawnerToActivate_2.SetActive(true);
            }


            if (spawnerToDeactivate_1 != null || spawnerToDeactivate_2 != null)
            {
                spawnerToDeactivate_1.SetActive(false);
                spawnerToDeactivate_2.SetActive(false);
            }


            alreadyActivated = true;
        }
    }
}
