using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private GameManager() {}

    public static GameManager Instance => instance;

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
        SceneManager.LoadScene(sceneName);
    }
}
