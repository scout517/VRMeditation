using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSelection : MonoBehaviour
{
    UI ui;

    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource audioSource;

    [SerializeField] Button audioSelectionButton;

    private List<Button> buttons = new List<Button>(5);

    // Start is called before the first frame update
    void Start()
    {
        ui = gameObject.GetComponentInParent<UI>();
        audioSource = audioSource.GetComponent<AudioSource>();
        foreach (AudioClip audioClip in audioClips)
        {
            Button newButton = Instantiate(audioSelectionButton, gameObject.transform);
            TMP_Text text = newButton.GetComponentInChildren<TMP_Text>();
            text.text = audioClip.name.Replace("-", " ");
            newButton.GetComponent<ButtonSong>().SetSong(audioClip);
            newButton.onClick.AddListener(() => PlayAudio(audioClip, newButton));
            buttons.Add(newButton);
        }
    }

    void PlayAudio(AudioClip clip, Button thisButton)
    {
        ActivateAllButtons();
        audioSource.clip = clip;
        if (!ui.GetMasterMuted())
        {
            audioSource.Play();
        }
        thisButton.interactable = false;
    }

    public void ActivateAllButtons()
    {
        audioSource.clip = null;
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
