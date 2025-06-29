using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AtaquePico : MonoBehaviour
{

    public GameObject hitboxPico;
    public float tiempoGolpe = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        hitboxPico.SetActive(false);//Deactivamos la hit box
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
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
