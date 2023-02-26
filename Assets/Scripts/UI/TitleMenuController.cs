using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuController : MonoBehaviour
{
    [SerializeField] private GameObject landingPage;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Slider master;
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider sfx;
    private bool isLanding = true;

    private void Start()
    {
        StartCoroutine(FindSoundManager());
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
    private void Update()
    {
        Landing();
    }

    private void Landing()
    {
        if (!isLanding) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlayMusic(0);
            isLanding = false;
            landingPage.SetActive(false);
            mainPanel.SetActive(true);
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
        SoundManager.Instance.PlaySFX(SoundManager.Sfx_Type.tick);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
