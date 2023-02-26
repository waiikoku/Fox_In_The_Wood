using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform cherryPosition;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private GameObject leverUI;
    [SerializeField] private GameObject finalGateUI;
    [SerializeField] private GameObject finaleUI;
    private PlayerCharacter pc;
    private int keyGem = 0;

    public event Action<int> OnGemUpdated;

    private void Start()
    {
        pc = FindObjectOfType<PlayerCharacter>();
        OnGemUpdated?.Invoke(keyGem);
    }
    public void PlayVFX(Transform marker)
    {
        explosionVFX.SetActive(false);
        explosionVFX.transform.position = marker.position;
        explosionVFX.GetComponent<SpriteRenderer>().enabled = true;
        explosionVFX.SetActive(true);
    }

    public void AddCherry()
    {
        if (pc == null) return;
        pc.Heal(1);
    }

    public void ClearCherry()
    {
        if (pc == null) return;
        pc.TakeDamage(5);
    }

    public void AddGem()
    {
        keyGem++;
        ClampKey();
        OnGemUpdated?.Invoke(keyGem);
    }

    public bool HaveGem()
    {
        return keyGem > 0 ? true : false;
    }

    public void UseGem()
    {
        keyGem--;
        ClampKey();
        RaiseGemEvent();
    }

    public void RaiseGemEvent()
    {
        OnGemUpdated?.Invoke(keyGem);
    }

    private void ClampKey()
    {
        keyGem = Mathf.Clamp(keyGem, 0, 3);
    }
    public void LeverTip(bool value)
    {
        leverUI.SetActive(value);
    }

    public void FinalGate(bool value)
    {
        finalGateUI.SetActive(value);
    }

    public void TheEnd()
    {
        SoundManager.Instance.StopMusic();
        finaleUI.SetActive(true);
    }
    public void ResetPosition(Transform target)
    {
        switch (target.tag)
        {
            case "Player":
                target.position = spawnPosition.position;
                break;
            case "Mystic":
                target.parent = null;
                target.position = cherryPosition.position;
                break;
            default:
                break;
        }
    }
}
