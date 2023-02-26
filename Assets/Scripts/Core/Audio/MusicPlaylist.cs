using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlaylist : Playlist
{
    void Start()
    {
        this.playlistName = string.Format("{0}[{1}]",this.Sounds[0].SoundName, this.Sounds[0].SoundID);
    }
}
