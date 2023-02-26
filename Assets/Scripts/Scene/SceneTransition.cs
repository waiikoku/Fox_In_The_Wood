using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneTransition : Singleton<SceneTransition>
{
    [SerializeField] private Image loadingBar;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TextMeshProUGUI loadingPercentage;
    [SerializeField] private TextMeshProUGUI loadingSceneName;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(FindSceneLoader());
    }

    private void OnDestroy()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.OnLevelLoading -= UpdateBar;
        }
    }

    private IEnumerator FindSceneLoader()
    {
        SceneLoader sl = null;
        while (sl == null)
        {
            if(SceneLoader.Instance != null)
            {
                sl = SceneLoader.Instance;
                sl.OnLevelLoading += UpdateBar;
                sl.OnLevelBeginLoad += OpenLoadingUI;
                sl.OnLevelLoaded += CloseLoadingUI;
            }
            yield return null;
        }
    }

    private void OpenLoadingUI(string name)
    {
        loadingPanel.SetActive(true);
        UpdateSceneName(name);
        UpdateBar(0f);
    }

    private void CloseLoadingUI()
    {
        loadingPanel.SetActive(false);
    }

    private void UpdateBar(float value)
    {
        loadingBar.fillAmount = value;
        loadingPercentage.text = string.Format("{0}%", ((float)System.Math.Round((double)value,2)) * 100f);
    }

    private void UpdateSceneName(string name)
    {
        loadingSceneName.text = string.Format("Loading {0}", name);
    }
}
