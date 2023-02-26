using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PointerFade : MonoBehaviour , IPointerEnterHandler , IPointerDownHandler , IPointerExitHandler
{
    [SerializeField] private UITextController uic;

    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Sfx_Type.tick);
        uic.StopFade();
        uic.Fader(false, true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Sfx_Type.click);
        uic.StopFade();
        uic.Fader(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uic.StopFade();
        uic.Fader(true);
    }
}
