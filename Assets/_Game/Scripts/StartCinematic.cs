using UnityEngine;
using UnityEngine.Playables;

public class StartCinematic : MonoBehaviour
{
    [SerializeField] private PlayableDirector director; //Tu Director con la Timeline
    private bool alreadyPlayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !alreadyPlayed)
        {
            director.Play(); //Reproduce la cinemática
            alreadyPlayed = true;
        }
    }
}
