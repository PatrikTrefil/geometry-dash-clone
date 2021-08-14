using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] AudioMixer mixer;
    void Start()
    {
        mixer.GetFloat("MasterVolume", out float currVolume);
        slider.value = currVolume;
        slider.onValueChanged.AddListener((v) =>
        {
            if (v == slider.minValue)
                mixer.SetFloat("MasterVolume", -80);
            else
                mixer.SetFloat("MasterVolume", v);

            Debug.Log($"Volume: {v}");
        });
    }
}
