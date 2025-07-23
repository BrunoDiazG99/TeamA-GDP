using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private bool alreadyTriggered = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !alreadyTriggered)
        {
            alreadyTriggered = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
