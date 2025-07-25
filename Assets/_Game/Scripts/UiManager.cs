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

    public Sound trackToPlay;

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

        StartAudioTrackOfLevel();

    }

    void StartAudioTrackOfLevel()
    {
        AudioManager.instance.StopAll();
        AudioManager.instance.PlaySound(trackToPlay.name);
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
        AudioManager.instance.PlaySound("sf_click");
        menu.SetActive(false);
        options.SetActive(true);
    }

    public void ReturnFromOptions()
    {
        AudioManager.instance.PlaySound("sf_click");
        options.SetActive(false);
        menu.SetActive(true);
    }

    public void TriggerMenu()
    {

        AudioManager.instance.PlaySound("sf_click");
        menuCanvas.enabled = !enabledMenu;
        enabledMenu = !enabledMenu;

        timeStopped = !timeStopped;
        Time.timeScale = !timeStopped ? 1f : 0f;


    }

    public void RestartLevel()
    {
        //playerMoveScript.enabled = true;
        AudioManager.instance.PlaySound("sf_click");
        Time.timeScale = 1f; // ✅ Asegura que el tiempo vuelva a correr
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        AudioManager.instance.PlaySound("sf_click");
        Time.timeScale = 1f;
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
