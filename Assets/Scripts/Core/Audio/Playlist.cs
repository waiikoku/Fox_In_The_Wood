using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Playlist : MonoBehaviour
{
    [SerializeField] protected string playlistName;
    public string PlaylistName => playlistName;
    [SerializeField] protected Sound[] sounds;
    public Sound[] Sounds => sounds;
}
