using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;

public class EjecutarCinematica : MonoBehaviour
{
    public PlayableDirector playableDirector;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            playableDirector.Play();
        }
    }
}
