using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    public AudioMixer mixer;
    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            mixer.SetFloat("MasterVolume", v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
