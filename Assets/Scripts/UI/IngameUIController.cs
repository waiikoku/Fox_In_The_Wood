using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIController : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey = KeyCode.F2;

    private bool isPause = false;
    [SerializeField] private Slider master;
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider sfx;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseMenu;
    [Header("Sub-Pause Menu")]
    [SerializeField] private GameObject[] subMenu;

    private void Start()
    {
#if UNITY_STANDALONE
        pauseKey = KeyCode.Escape;
#endif
        StartCoroutine(FindSoundManager());
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TriggerPause();
        }
    }

    public void Unpause()
    {
        isPause = false;
        pausePanel.SetActive(false);
        pauseMenu.SetActive(true);
        for (int i = 0; i < subMenu.Length; i++)
        {
            subMenu[i].SetActive(false);
        }
        Time.timeScale = 1f;
    }

    private void TriggerPause()
    {
        isPause = !isPause;
        pausePanel.SetActive(isPause);
        if (!isPause)
        {
            pauseMenu.SetActive(true);
            for (int i = 0; i < subMenu.Length; i++)
            {
                subMenu[i].SetActive(false);
            }
        }
        Time.timeScale = isPause ? 0f : 1f;
    }

    private IEnumerator FindSoundManager()
    {
        SoundManager smi = null;
        while (smi == null)
        {
            if (SoundManager.Instance != null)
            {
                smi = SoundManager.Instance;
                smi.OnMasterVolume += UpdateMaster;
                smi.OnBGMVolume += UpdateBGM;
                smi.OnSFXVolume += UpdateSFX;
                master.value = smi.Master;
                bgm.value = smi.Music;
                sfx.value = smi.Effect;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnDestroy()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager smi = SoundManager.Instance;
            smi.OnMasterVolume -= UpdateMaster;
            smi.OnBGMVolume -= UpdateBGM;
            smi.OnSFXVolume -= UpdateSFX;
        }
    }


    private void UpdateMaster(float value)
    {
        master.value = value;
    }

    private void UpdateBGM(float value)
    {
        bgm.value = value;
    }

    private void UpdateSFX(float value)
    {
        sfx.value = value;
    }

    public void LoadScene(string name)
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadAsync(name);
    }

    public void ChangeMasterVolume(float volume)
    {
        SoundManager.Instance.RaiseEventMaster(volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        SoundManager.Instance.RaiseEventMusic(volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        SoundManager.Instance.RaiseEventSFX(volume);
    }

    public void ResetToDefault()
    {
        SoundManager.Instance.ResetToDefault();
    }

    public void SfxButtonTick()
    {
        if (!SoundAvailable()) return;
        SoundManager.Instance.PlaySFX(SoundManager.Sfx_Type.tick);
    }

    public void SfxPickup()
    {
        if (!SoundAvailable()) return;
        SoundManager.Instance.PlaySFX(SoundManager.Sfx_Type.pickUp);
    }

    private bool SoundAvailable()
    {
        return SoundManager.Instance == null ? false : true;
    }
}
