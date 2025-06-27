using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class AtaquePico : MonoBehaviour
{

    public GameObject hitboxPico;
    public float tiempoGolpe = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Golpear());
        }
    }

    IEnumerator Golpear()
    {
        Debug.Log("GOLPE ACTIVADO");
        hitboxPico.SetActive(true); //Activamos la hitbox del pico;
        yield return new WaitForSeconds(tiempoGolpe);
        hitboxPico.SetActive(false); //Desactivamos la hitbox del pico;

    }
}
