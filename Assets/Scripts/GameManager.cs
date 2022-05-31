using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas menuCanvas;
    [SerializeField] TextMeshProUGUI muteText;
    static public GameManager Instance;
    private void Awake()
    {
        Pause();
        audioSource = GetComponent<AudioSource>();
        Instance = this;
        audioSource.ignoreListenerPause = true;
    }
    public void SoundControl(Scrollbar Sc)
    {
        AudioListener.volume = Sc.value;
    }
    public void HomeScreen()
    {
        Resume();
        SaveSystem.SaveBestScore(PlayerUI.Instance.MaxScore);
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Resume()
    {
        menuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
    InputManager inputManager;
    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
    }
    [SerializeField] Canvas UIend;
    AudioSource audioSource;
    public void DeathUI()
    {
        Time.timeScale = 0.4f;

        Invoke("WaitAnimation", 1.2f);
    }
    void WaitAnimation()
    {
        if (audioSource.clip)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        AudioListener.pause = true;
        UIend.gameObject.SetActive(true);
    }

    public void Restart()
    {

        SceneManager.LoadScene(1);
    }
    private void Update()
    {
        if (inputManager.escPressed && !Player.Instance.isDie)
        {
            Pause();
        }
    }
    void Pause()
    {
        Time.timeScale = 0;
        menuCanvas.gameObject.SetActive(true);
        AudioListener.pause = true;
    }
}
