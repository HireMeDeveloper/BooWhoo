using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Canvas sceneTransitionCanvas;

    public Canvas pauseMenuCanvas;

    PlayerControls playerControls;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        playerControls = new PlayerControls();
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void Disable()
    {
        playerControls.Disable();
    }

    public void Update()
    {
        if (playerControls.player.togglepause.ReadValue<float>() > 0.1f)
            OpenPauseMenu();
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene("GameScene"));
    }

    public IEnumerator LoadScene(string sceneName)
    {
        sceneTransitionCanvas.GetComponent<FadeInOut>().FadeIn();

        yield return new WaitForSeconds(1);
        
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1);

        sceneTransitionCanvas.GetComponent<FadeInOut>().FadeOut();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    public void OpenPauseMenu()
    {
        pauseMenuCanvas.gameObject.SetActive(true);
    }
    
    public void ClosePauseMenu()
    {
        pauseMenuCanvas.gameObject.SetActive(false);
    }
    public void ReturnToMainScene()
    {
        Destroy(GameObject.Find("MainObjects"));

        SceneManager.LoadScene("MainScene");
    }

}

