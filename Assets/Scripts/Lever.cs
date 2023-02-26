using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Lever : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Sprite leverUp, leverDown;
    private bool isUnlocked = false;
    private bool isOn = false;
    private bool insideZone = false;
    [SerializeField] private GameObject blocker;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!insideZone) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isUnlocked)
            {
                if (GameController.Instance.HaveGem())
                {
                    isUnlocked = true;
                    GameController.Instance.UseGem();
                    TriggerLever();
                }
            }
            else
            {
                TriggerLever();
            }
        }
    }

    private void UpdateLever()
    {
        if (isOn)
        {
            sr.sprite = leverDown;
            SoundManager.Instance.PlaySFX(SoundManager.Sfx_Type.leverDown);
        }
        else
        {
            sr.sprite = leverUp;
            SoundManager.Instance.PlaySFX(SoundManager.Sfx_Type.leverUp);
        }
        blocker.SetActive(!isOn);
    }

    public void TriggerLever()
    {
        isOn = !isOn;
        UpdateLever();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            insideZone = true;
            GameController.Instance.LeverTip(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            insideZone = false;
            GameController.Instance.LeverTip(false);
        }
    }
}
