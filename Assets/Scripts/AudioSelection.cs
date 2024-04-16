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

    private Button initialButtonToDisable = null;

    void Awake()
    {
        ui = gameObject.GetComponentInParent<UI>();
        audioSource = audioSource.GetComponent<AudioSource>();
        foreach (AudioClip audioClip in audioClips)
        {
            Button newButton = Instantiate(audioSelectionButton, gameObject.transform);
            TMP_Text text = newButton.GetComponentInChildren<TMP_Text>();
            text.text = audioClip.name.Replace("-", " ");
            newButton.transform.name = audioClip.name;
            newButton.GetComponent<ButtonSong>().SetSong(audioClip);
            newButton.onClick.AddListener(() => PlayAudio(audioClip, newButton));
            buttons.Add(newButton);
        }
    }

    void Start()
    {
        if(StaticData.wasOnMenu && transform.name.Equals("Music List"))
        {
            PlayInitialMusic();
        }
        else if(StaticData.wasOnMenu && transform.name.Equals("Meditation List"))
        {
            PlayInitialMeditation();
        }
        if(initialButtonToDisable != null)
            StartCoroutine(DisableInitialButton());
    }

    IEnumerator DisableInitialButton()
    {
        yield return new WaitForSeconds(.1f);
        initialButtonToDisable.interactable = false;
    }

    private void PlayInitialMusic()
    {
        string songSelection = StaticData.songSelection;
        if(songSelection.Equals("None"))
            return;

        InvokeButton(songSelection);
        StaticData.songSelection = "None";
    }

    private  void PlayInitialMeditation()
    {
        string meditationSelection = StaticData.meditationSelection;
        if(meditationSelection.Equals("None"))
            return;

        InvokeButton(meditationSelection);
        StaticData.meditationSelection = "None";
    }

    private void InvokeButton(string find)
    {
        foreach(Button button in buttons)
        {
            if(button.transform.name.Equals(find))
            {
                initialButtonToDisable = button;
                button.onClick.Invoke();
            }
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
}
