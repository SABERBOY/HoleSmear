using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyCanvasSetting : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
#if UNITY_ANDROID
        mySetting();
#endif
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
#if UNITY_ANDROID
        mySetting();
#endif
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void mySetting()
    {
        var canv = FindObjectsOfType(typeof(CanvasScaler)) as CanvasScaler[];
        foreach (var cns in canv) cns.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }
}