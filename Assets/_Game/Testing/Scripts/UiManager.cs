using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiManager : MonoBehaviour
{
    public Canvas menu;
    private bool enabledMenu = false;
    private bool timeStopped = false;

    InputAction openMenu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu.enabled = false;
        openMenu = InputSystem.actions.FindAction("Cancel");

        openMenu.started += TriggerMenu;

    }

    //void OnEnable()
    //{
        
    //}

    private void TriggerMenu(InputAction.CallbackContext context)
    {

        Debug.Log("open menu");
        Debug.Log(enabledMenu);
        Debug.Log(timeStopped);

        menu.enabled = !enabledMenu;
        enabledMenu = !enabledMenu;

        Time.timeScale = !timeStopped ? 1f : 0f;
        timeStopped = !timeStopped;

    }


}
