using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Canvas sceneTransitionCanvas;

    public Canvas pauseMenuCanvas;

    public Canvas optionsMenuCanvas;

    public GameObject taskSystemUI;

    public GameObject inventorySystemUI;

    public Slider musicSlider;
    public Slider ambientSlider;
    public Slider sfxSlider;

    private string musicPref = "music";
    private string ambientPref = "ambient";
    private string sfxPref = "sfx";

    private PlayerInput playerInput;
    
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

        playerInput = GetComponent<PlayerInput>();

        AudioManager.CreateAudio("MainMenu");
        LoadAudioData();

        CloseOptionsMenu();
        ClosePauseMenu();
        CloseTaskSystemUI();
        CloseInventorySystemUI();
    }

    public void OnEnable()
    {
        playerInput.actions["toggle-pause"].performed += OnPauseGame;
    }

    public void OnDisable()
    {
        playerInput.actions["toggle-pause"].performed -= OnPauseGame;
    }

    private void OnPauseGame(InputAction.CallbackContext value)
    {
        if (pauseMenuCanvas.gameObject.activeSelf)
            ClosePauseMenu();
        else
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
        AudioManager.CreateAudio("GameBGM");
        AudioManager.CreateAudio("Birds");
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
        CloseTaskSystemUI();
        CloseInventorySystemUI();

    }
    
    public void ClosePauseMenu()
    {
        pauseMenuCanvas.gameObject.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        optionsMenuCanvas.gameObject.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenuCanvas.gameObject.SetActive(false);
    }

    public void OpenTaskSystemUI()
    {
        taskSystemUI.SetActive(true);
        ClosePauseMenu();
    }

    public void CloseTaskSystemUI()
    {
        taskSystemUI.SetActive(false);
    }

    public void OpenInventorySystemUI()
    {
        inventorySystemUI.SetActive(true);
        ClosePauseMenu();
    }

    public void CloseInventorySystemUI()
    {
        inventorySystemUI.SetActive(false);
    }

    public void ReturnToMainScene()
    {
        Destroy(GameObject.Find("MainObjects"));

        SceneManager.LoadScene("MainScene");
    }

    public void SaveMusicData()
    {
        PlayerPrefs.SetFloat(musicPref, musicSlider.value);
        AudioManager.UpdateVolume();
    }

    public void SaveAmbientData()
    {
        PlayerPrefs.SetFloat(ambientPref, ambientSlider.value);
        AudioManager.UpdateVolume();
    }

    public void SaveSfxData()
    {
        PlayerPrefs.SetFloat(sfxPref, sfxSlider.value);
        AudioManager.UpdateVolume();
    }

    public void LoadAudioData()
    {
        if (PlayerPrefs.HasKey(musicPref))
        {
            musicSlider.value = PlayerPrefs.GetFloat(musicPref);
        }

        if (PlayerPrefs.HasKey(ambientPref))
        {
            ambientSlider.value = PlayerPrefs.GetFloat(ambientPref);
        }

        if (PlayerPrefs.HasKey(sfxPref))
        {
            sfxSlider.value = PlayerPrefs.GetFloat(sfxPref);
        }
    }

}

