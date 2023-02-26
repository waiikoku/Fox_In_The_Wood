using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public event Action<float> OnLevelLoading;
    public event Action<string> OnLevelBeginLoad;
    public event Action OnLevelLoaded;
    public event Action OnLevelUnloaded;

    private int currentSceneIndex = 0;
    private string currentSceneName;
    private int nextSceneIndex = -1;
    private string nextSceneName = "Null";
    protected override void Awake()
    {
        base.Awake();
        currentSceneName = SceneManager.GetSceneByBuildIndex(currentSceneIndex).name;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        OnLevelLoaded = null;
        OnLevelUnloaded = null;
    }

    public void LoadSingle(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void LoadAdditive(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public void LoadAsync(string name)
    {
        StartCoroutine(AsyncLoader(name));
    }

    /*
    public  void UnloadAsync(string name)
    {
        StartCoroutine(AsyncLoader(name, true));
    }
    */

    private IEnumerator AsyncLoader(string name)
    {
        bool isDone = false;
        float progress = 0;
        nextSceneName = name;
        OnLevelBeginLoad?.Invoke(nextSceneName);
        yield return new WaitForSeconds(0.5f);
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        loadOp.allowSceneActivation = false;
        while (!isDone)
        {
            progress = Mathf.Clamp01(loadOp.progress / 0.9f);
            OnLevelLoading?.Invoke(progress);
            if (progress == 1f)
            {
                isDone = true;
                loadOp.allowSceneActivation = true;
                yield return new WaitForSeconds(0.5f);
                currentSceneName = name;
                nextSceneName = null;
                OnLevelLoaded?.Invoke();
            }
            yield return new WaitForSeconds(0.01f);
        }

    }
}
