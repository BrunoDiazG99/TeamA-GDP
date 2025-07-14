using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionMenu;
    [SerializeField]
    private GameObject loadingMenu;

    [SerializeField]
    private Slider loadingSlider;

    private void Awake()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        loadingMenu.SetActive(false);

    }

    private void Start()
    {
        AudioManager.instance.StopSound("GameTrack");
        AudioManager.instance.PlaySound("MenuTrack");
    }

    public void ExitGame()
    {
        AudioManager.instance.PlaySound("sf_click");
        Application.Quit();
    }

    public void ReturnToMain()
    {
        AudioManager.instance.PlaySound("sf_click");
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void ShowOptions()
    {
        AudioManager.instance.PlaySound("sf_click");
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    private void ShowLoading()
    {
        AudioManager.instance.PlaySound("sf_click");
        mainMenu.SetActive(false);
        loadingMenu.SetActive(true);
        loadingSlider.interactable = false;
    }

    public void PlayGame()
    {
        AudioManager.instance.PlaySound("sf_click");
        StartCoroutine(PlayGameRoutine());
    }
    private IEnumerator PlayGameRoutine()
    {
        ShowLoading();
        yield return new WaitForSeconds(2f);
        LoadLevel("TestingScene-bruno");
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadLevelAsync(sceneName));
    }

    private IEnumerator LoadLevelAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }

}
