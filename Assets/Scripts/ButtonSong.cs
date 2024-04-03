using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSong : MonoBehaviour
{

    private AudioClip song = null;

    public void SetSong(AudioClip newSong) {
        song = newSong;
    }

    public AudioClip GetSong() {
        return song;
    }

}
