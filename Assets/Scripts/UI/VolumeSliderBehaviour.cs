using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliderBehaviour : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup audioGroup;

    void Start()
    {
        audioMixer.GetFloat(audioGroup.name, out float volume);
        slider.value = Mathf.Pow(10, volume / 20);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(audioGroup.name, Mathf.Log10(value) * 20);
    }
}
