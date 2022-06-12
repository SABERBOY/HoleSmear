using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SceneSwitch : MonoBehaviour
{
    private static SceneSwitch switchInstance;

    public void SwitchScene(int val)
    {
        if (val == SceneManager.GetActiveScene().buildIndex) return; //toggle buttons change twice
        SceneManager.LoadSceneAsync(val);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (switchInstance == null)
            switchInstance = this;
        else
            DestroyObject(gameObject);
    }

    private void Update()
    {
        if (Input.GetKey("escape")) Application.Quit();
    }
}