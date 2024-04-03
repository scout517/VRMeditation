using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelection : MonoBehaviour
{

    [SerializeField] AudioClip[] music;
    [SerializeField] AudioSource audioSource;

    [SerializeField] Button musicSelectionButton;

    private List<Button> buttons = new List<Button>(5);

    // Start is called before the first frame update
    void Start()
    {
        audioSource = audioSource.GetComponent<AudioSource>();
        foreach(AudioClip audioClip in music) {
            Button newButton = Instantiate(musicSelectionButton, gameObject.transform);
            TMP_Text text = newButton.GetComponentInChildren<TMP_Text>();
            text.text = audioClip.name.Replace("-", " ");
            newButton.GetComponent<ButtonSong>().SetSong(audioClip);
            newButton.onClick.AddListener(() => PlayAudio(audioClip, newButton));
            buttons.Add(newButton);
        }
    }

    void PlayAudio(AudioClip clip, Button thisButton) {
        foreach(Button button in buttons) {
            button.interactable = true;
        }
        audioSource.clip = clip;
        audioSource.Play();
        thisButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
