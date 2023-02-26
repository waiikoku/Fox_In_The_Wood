using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextController : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    private Color currentColor;
    [SerializeField] private Color baseColor = Color.white;
    [SerializeField] private Color targetColor = Color.white;
    [SerializeField] private Color pressedColor = Color.white;
    private float percentage = 0f;
    private float timer = 0f;
    [SerializeField] private float duration = 1f;
    private Coroutine fadeCout;
    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void Fader(bool reverse,bool press = false)
    {
        fadeCout = StartCoroutine(Fading(reverse, press));
    }

    public void StopFade()
    {
        if(fadeCout != null)
        {
            StopCoroutine(fadeCout);
        }
    }

    private IEnumerator Fading(bool reverse,bool press)
    {
        timer = 0;
        Color localCurrent = text.color;
        while (true)
        {
            percentage = timer / duration;
            if (press)
            {
                currentColor = Color.Lerp(localCurrent, pressedColor, percentage);
            }
            else
            {
                if (reverse)
                {
                    currentColor = Color.Lerp(targetColor, baseColor, percentage);
                }
                else
                {
                    currentColor = Color.Lerp(baseColor, targetColor, percentage);
                }
            }
            yield return new WaitForSeconds(0.01f);
            timer += Time.deltaTime;
            text.color = currentColor;
        }    
    }
}
