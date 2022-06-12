using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RenderingModeSwitch : MonoBehaviour
{
    public RenderingPath[] renderingOptions;
    private Dropdown RenderingMode;
    public int m = 0;

    // Use this for initialization
    private void Start()
    {
        RenderingMode = gameObject.GetComponent<Dropdown>();
        RenderingMode.ClearOptions();
        var options = new List<string>();
        foreach (var rp in renderingOptions) options.Add(rp.ToString());
        RenderingMode.value = m;
        RenderingMode.AddOptions(options);

        RenderingMode.onValueChanged.AddListener(delegate { SetPath(RenderingMode.value); });
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Camera.main.renderingPath = renderingOptions[m];
    }

    // Update is called once per frame
    private void SetPath(int i)
    {
        m = i;
        Camera.main.renderingPath = renderingOptions[m];
    }
}