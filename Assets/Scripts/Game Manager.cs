using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PoolManager _poolManager;
    [SerializeField] private ShadowManager _shadowManager;
    [SerializeField] private ThirdPersonCamera _cameraManager;

    void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        _poolManager.InitializePool();
        Shooting.instance.SetUp(_poolManager);
    }
}
