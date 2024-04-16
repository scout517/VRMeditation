using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject[] scenes;
    [SerializeField] GameObject[] music;
    [SerializeField] GameObject[] guidedMediation;

    [SerializeField] Button startButton;

    // String is GameObject name. Button is button component of GameObject
    private Dictionary<string, Button> allScenes;
    private Dictionary<string, Button> allSongs;
    private Dictionary<string, Button> allMeditation;

    // Start is called before the first frame update
    void Start()
    {
        StaticData.wasOnMenu = true;
        SetupDictionaries();
        ButtonSetup();
    }

    private void SetupDictionaries()
    {
        allScenes = new Dictionary<string, Button>();
        allSongs = new Dictionary<string, Button>();
        allMeditation = new Dictionary<string, Button>();
    }

    private void ButtonSetup()
    {
        foreach (GameObject scene in scenes)
        {
            Button sceneButton = scene.GetComponent<Button>();
            allScenes.Add(scene.transform.name, sceneButton);
            sceneButton.onClick.AddListener(() => SelectScene(scene));
        }
        foreach (GameObject song in music)
        {
            Button songButton = song.GetComponent<Button>();
            allSongs.Add(song.transform.name, songButton);
            songButton.onClick.AddListener(() => SelectSong(song));
        }
        foreach (GameObject meditation in guidedMediation)
        {
            Button meditationButton = meditation.GetComponent<Button>();
            allMeditation.Add(meditation.transform.name, meditationButton);
            meditationButton.onClick.AddListener(() => SelectMeditation(meditation));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeAllButtonsInteractable(Dictionary<string, Button> dictionary)
    {
        foreach(KeyValuePair<string, Button> pair in dictionary)
        {
            pair.Value.interactable = true;
        }
    }

    public void StartButtonPressed()
    {
        StaticData.songSelection = allSongs.FirstOrDefault(x => !x.Value.interactable).Key;
        StaticData.meditationSelection = allMeditation.FirstOrDefault(x => !x.Value.interactable).Key;
        SceneManager.LoadScene(allScenes.FirstOrDefault(x => !x.Value.interactable).Key);
    }


    public void SelectScene(GameObject selectedScene)
    {
        MakeAllButtonsInteractable(allScenes);
        allScenes[selectedScene.transform.name].interactable = false;
        startButton.interactable = true;
    }

    public void SelectSong(GameObject selectedSong)
    {
        MakeAllButtonsInteractable(allSongs);
        allSongs[selectedSong.transform.name].interactable = false;
    }

    public void SelectMeditation(GameObject selectedMeditation)
    {
        MakeAllButtonsInteractable(allMeditation);
        allMeditation[selectedMeditation.transform.name].interactable = false;
    }    

}
