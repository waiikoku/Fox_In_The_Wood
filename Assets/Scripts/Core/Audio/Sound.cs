using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private int soundID;
    public string SoundID
    {
        get
        {
            string result;
            if (soundID < 10)
            {
                result = "000" + soundID;
            }
            else if(soundID < 100)
            {
                result = "00" + soundID;
            }
            else if(soundID < 1000)
            {
                result = "0" + soundID;
            }
            else
            {
                result = soundID.ToString();
            }
            return result;
        }
    }
    [SerializeField] private string soundName;
    public string SoundName => soundName;
    [SerializeField] private AudioClip sound;
    public AudioClip _Sound => sound;
}
