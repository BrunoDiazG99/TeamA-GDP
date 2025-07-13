using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UiManager : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas gameOverScreen;
    public Canvas gameFinishedScreen;


    public GameObject options;
    public GameObject menu;


    private bool enabledMenu = false;
    private bool timeStopped = false;
    private Move playerMoveScript;

    InputAction openMenuInput;


    void Awake()
    {
        playerMoveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
        menu.SetActive(true);
        options.SetActive(false);
        gameOverScreen.enabled = false;
        gameFinishedScreen.enabled = false;
        menuCanvas.enabled = false;
    }


    void Start()
    {
        openMenuInput = InputSystem.actions.FindAction("Cancel");

        openMenuInput.started += TriggerMenuFromInput;

        GameEvents.current.onGameOver += GameOver;
        GameEvents.current.onGameFinish += FinishedGame;

        AudioManager.instance.StopSound("MenuTrack");
        AudioManager.instance.PlaySound("GameTrack");

    }

    void StopTime()
    {
        playerMoveScript.enabled = false;
        Time.timeScale = 0f;

    }

    void FinishedGame()
    {
        gameFinishedScreen.enabled = true;
        StopTime();
    }

    void GameOver()
    {
        gameOverScreen.enabled = true;
        StopTime();
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

        timeStopped = !timeStopped;
        Time.timeScale = !timeStopped ? 1f : 0f;

        // 🔧 Activa o desactiva el movimiento del jugador
        if (playerMoveScript != null)
        {
            playerMoveScript.enabled = !timeStopped;
        }
        // 🔧 FIX: soltar el foco de la UI para que el input del jugador vuelva a funcionar
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void RestartLevel()
    {
        playerMoveScript.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
