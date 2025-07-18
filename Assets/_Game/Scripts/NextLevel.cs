using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    private bool alreadyTriggered = false;
    [SerializeField] private string sceneToLoad; // 👈 Nombre de la escena a cargar
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !alreadyTriggered)
        {
            SceneManager.LoadScene(sceneToLoad);
            alreadyTriggered = true;
        }
    }
}
