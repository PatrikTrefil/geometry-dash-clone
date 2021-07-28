using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public AudioMixer mixer;
    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            if (v == slider.minValue)
                mixer.SetFloat("MasterVolume", -80);
            else
                mixer.SetFloat("MasterVolume", v);

            Debug.Log($"Volume: {v}");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
