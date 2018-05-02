using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField]
    Slider volumeSlider;

    protected AudioSource bgMusic;

    // Use this for initialization
    void Start()
    {
        try
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        catch
        {
            volumeSlider.value = 0.5f;
        }
        bgMusic = GetComponent<AudioSource>();
        bgMusic.Play();
    }

    public void Update()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}