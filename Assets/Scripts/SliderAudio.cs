using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    private Slider slider;
    [SerializeField] AudioMixer mixer;
    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            mixer.SetFloat("MasterVolume", v);
        });
    }
}
