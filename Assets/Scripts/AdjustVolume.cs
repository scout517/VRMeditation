using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustVolume : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;

    private Slider slider;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume()
    {
        audioSource.volume = slider.value;
    }
}
