using UnityEngine;
using UnityEngine.InputSystem;

public class LoreManager : MonoBehaviour
{
    public Canvas loreCanvas;
    private bool loreShown = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loreCanvas.enabled = true;
        Time.timeScale = 0f; // Stop Game
    }

    // Update is called once per frame
    void Update()
    {
        if (loreShown && Input.GetKeyDown(KeyCode.Return))
        {
            loreCanvas.enabled = false;
            Time.timeScale = 1f;// Play Game
            loreShown = false;
        }
    }
}
