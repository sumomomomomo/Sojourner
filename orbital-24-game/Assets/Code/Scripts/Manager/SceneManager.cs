using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;

    private SceneManager() {}

    public static SceneManager Instance => instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadNamedScene(sceneName));
    }

    private IEnumerator LoadNamedScene(string sceneName)
    {
        yield return new WaitForSeconds(0.01f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
