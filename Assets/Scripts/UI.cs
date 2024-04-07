using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    private bool uiVisible = true;

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
        ToggleUI();
        ShiftUI();
    }

    private void ToggleUI()
    {
        if (Input.GetKeyDown(KeyCode.H))
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
    }

    private void ShiftUI()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
            Vector3 newRotation = new Vector3(0, cameraRotation.y, 0);
            if (rotateCoroutine != null)
            {
                StopCoroutine(rotateCoroutine);
            }
            rotateCoroutine = StartCoroutine(RotateUI(Quaternion.Euler(newRotation)));
        }
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
                Debug.Log("Here! Muted");
                masterMuteImage.sprite = mutedSprite;
                if (audioSource.clip != null)
                {
                    audioSource.Pause();
                }
            }
            else
            {
                Debug.Log("Here! Unmuted");
                masterMuteImage.sprite = unmutedSprite;
                if (audioSource.clip != null)
                {
                    audioSource.Play();
                }
            }
        }
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
