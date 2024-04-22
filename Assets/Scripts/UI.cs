using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    private Coroutine rotateCoroutine = null;

    private bool masterMuted = false;
    [SerializeField] Image masterMuteImage;
    [SerializeField] Sprite mutedSprite;
    [SerializeField] Sprite unmutedSprite;

    [SerializeField] float rotateDuration = 2;
    [SerializeField] GameObject[] UIElements;
    [SerializeField] AudioSource[] AudioSources;

    [SerializeField] InputActionReference toggleUI;
    [SerializeField] InputActionReference shiftUI;

    private bool uiVisible = true;

    void Awake()
    {
        toggleUI.action.started += XRControllerToggleUIPressed;
        shiftUI.action.started += XRControllerShiftUIPressed;
    }
    
    void OnDestroy()
    {
        toggleUI.action.started -= XRControllerToggleUIPressed;
        shiftUI.action.started -= XRControllerShiftUIPressed;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 newRotation = new Vector3(0, cameraRotation.y, 0);
        gameObject.transform.eulerAngles = newRotation;
        uiVisible = LayerMask.LayerToName(UIElements[0].layer).Equals("Default");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleUI();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            ShiftUI();
        }
    }

    private void XRControllerToggleUIPressed(InputAction.CallbackContext context)
    {
        ToggleUI();
    }

    private void ToggleUI()
    {
        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 newRotation = new Vector3(0, cameraRotation.y, 0);
        gameObject.transform.eulerAngles = newRotation;
        uiVisible = !uiVisible;
        string newLayer = uiVisible ? "Default" : "UI";
        foreach (GameObject uiElement in UIElements)
        {
            
            uiElement.layer = LayerMask.NameToLayer(newLayer);
        }
    }

    private void XRControllerShiftUIPressed(InputAction.CallbackContext context)
    {
        ShiftUI();
    }

    private void ShiftUI()
    {
        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 newRotation = new Vector3(0, cameraRotation.y, 0);
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        rotateCoroutine = StartCoroutine(RotateUI(Quaternion.Euler(newRotation)));
    }

    IEnumerator RotateUI(Quaternion newRotation)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < rotateDuration)
        {
            gameObject.transform.rotation = Quaternion.Lerp(startValue, newRotation, time / rotateDuration);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.rotation = newRotation;
        rotateCoroutine = null;
    }

    public void MasterMutePressed()
    {
        masterMuted = !masterMuted;
        foreach (AudioSource audioSource in AudioSources)
        {
            if (masterMuted)
            {
                masterMuteImage.sprite = mutedSprite;
                if (audioSource.clip != null)
                {
                    audioSource.Pause();
                }
            }
            else
            {
                masterMuteImage.sprite = unmutedSprite;
                if (audioSource.clip != null)
                {
                    audioSource.Play();
                }
            }
        }
    }

    public void HomeButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AdjustVolume(AudioSource audioSource, Slider slider)
    {
        audioSource.volume = slider.value;
    }

    public bool GetMasterMuted()
    {
        return masterMuted;
    }

}
