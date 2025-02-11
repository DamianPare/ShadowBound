using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PoolManager _poolManager;
    [SerializeField] private ShadowManager _shadowManager;
    public static GameManager instance;

    [SerializeField] private string Level1Name;
    [SerializeField] private string Level2Name;
    [SerializeField] private string Level3Name;

    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject ControlScreen;

    public bool LevelCompleted;
    private bool isPaused;

    public Animator transition;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetUp();
        LevelCompleted = false;
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            isPaused = true;
        }   
        
        if (isPaused)
        {
            Pause();
        }

        if (LevelCompleted)
        {
            NextLevel();
        }
    }

    IEnumerator LoadLevel(String levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelName);
    }

    public void NextLevel()
    {

        if (SceneManager.GetActiveScene().name == Level1Name)
        {
            StartCoroutine(LoadLevel(Level2Name));
        }
        if (SceneManager.GetActiveScene().name == Level2Name)
        {
            StartCoroutine(LoadLevel(Level3Name));
        }
        if (SceneManager.GetActiveScene().name == Level3Name)
        {
            GameCompleted();
        }
    }

    private void GameCompleted()
    {
        SceneManager.LoadScene(Level1Name);
    }

    private void EndGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void SetUp()
    {
        _poolManager.InitializePool();
        Shooting.instance.SetUp(_poolManager);
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseScreen.SetActive(false);
    }

    public void ControlsON()
    {
        ControlScreen.SetActive(true);
        PauseScreen.SetActive(false);
    }
    public void ControlsOFF()
    {
        ControlScreen.SetActive(false);
        PauseScreen.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
