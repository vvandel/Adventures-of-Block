/*
The MIT License (MIT)

Copyright (c) 2018 Victor van Andel, Chun He

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

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
    void Start() // Get the value from the slider and play the background music in the menu as an indication of the current volume
    {
        try
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        catch
        {
            Debug.LogWarning("Volume not assigned, 0.5f used instead");
            volumeSlider.value = 0.5f;
        }
        bgMusic = GetComponent<AudioSource>();
        bgMusic.Play();
    }

    public void Update()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value); // The player's preferred volume is saved via PlayerPrefs which is used throughout the game
    }
}