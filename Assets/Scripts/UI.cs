using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    private Coroutine rotateCoroutine = null;

    [SerializeField] bool inVR = false;
    [SerializeField] float rotateDuration = 2;
    [SerializeField] GameObject[] UIElements;
    [SerializeField] GameObject[] AudioObjects;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 newRotation = new Vector3(0, cameraRotation.y, 0);
        gameObject.transform.eulerAngles = newRotation;
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
            foreach (GameObject uiElement in UIElements)
            {
                Transform transform = uiElement.transform;
                transform.gameObject.SetActive(!transform.gameObject.activeSelf);
            }
        }
    }

    private void ShiftUI()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
            Vector3 newRotation = new Vector3(0, cameraRotation.y, 0);

            if(rotateCoroutine != null)
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

        while(time < rotateDuration)
        {
            gameObject.transform.rotation = Quaternion.Lerp(startValue, newRotation, time / rotateDuration);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.rotation = newRotation;
        rotateCoroutine = null;
    }

    public void MasterMutePressed(Toggle toggle)
    {
        foreach(GameObject thisObject in AudioObjects)
        {
            AudioSource audioSource = thisObject.GetComponent<AudioSource>();
            if(audioSource.clip == null)
            {
                continue;
            }
            if(toggle.isOn)
            {
                audioSource.Pause();
            }
            else{
                audioSource.Play();
            }
        }
    }

}
