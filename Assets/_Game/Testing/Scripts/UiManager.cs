using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public Canvas menuCanvas;

    public GameObject options;
    public GameObject menu;

    private bool enabledMenu = false;
    private bool timeStopped = false;

    InputAction openMenuInput;


    void Awake()
    {
        menu.SetActive(true);
        options.SetActive(false);
    }


    void Start()
    {
        menuCanvas.enabled = false;
        openMenuInput = InputSystem.actions.FindAction("Cancel");

        openMenuInput.started += TriggerMenuFromInput;

        AudioManager.instance.StopSound("MenuTrack");
        AudioManager.instance.PlaySound("MenuTrack");

    }

    public void OpenOptions()
    {
        menu.SetActive(false);
        options.SetActive(true);
    }

    public void ReturnFromOptions()
    {
        options.SetActive(false);
        menu.SetActive(true);
    }

    public void TriggerMenu()
    {

        menuCanvas.enabled = !enabledMenu;
        enabledMenu = !enabledMenu;

        Time.timeScale = !timeStopped ? 1f : 0f;
        timeStopped = !timeStopped;

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    private void TriggerMenuFromInput(InputAction.CallbackContext context)
    {

        Debug.Log("open menu canvas");
        Debug.Log(enabledMenu);
        Debug.Log(timeStopped);

        TriggerMenu();

    }


}
