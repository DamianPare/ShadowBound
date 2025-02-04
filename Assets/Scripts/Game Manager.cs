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

    [SerializeField] private GameObject completedScreen;
    [SerializeField] private Image Bar;

    [SerializeField] private GameObject lostScreen;

    private float timeElapsed;
    private float Edibility;
    private int startEdibility;

    public bool LevelCompleted;
    public bool LevelFailed;

    private Color32 resultColor;
    private Color32 milkColor;
    private SpriteRenderer milkRenderer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetUp();
        LevelCompleted = false;
        LevelFailed = false;
    }

    private void Update()
    {
        if (LevelCompleted)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {

        if (SceneManager.GetActiveScene().name == Level1Name)
        {
            SceneManager.LoadScene(Level2Name);
        }
        if (SceneManager.GetActiveScene().name == Level2Name)
        {
            SceneManager.LoadScene(Level3Name);
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

    public void LevelFinished()
    {
        LevelCompleted = true;
        completedScreen.SetActive(true);
    }

    private void EndGame()
    {
        lostScreen.SetActive(true);

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
}
