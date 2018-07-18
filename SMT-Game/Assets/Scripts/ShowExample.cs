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

public class ShowExample : MonoBehaviour {

    Variation soundMode = Log.CurrentMode;
    public static int channel;
    public AudioSource source;
    public bool exampleActive = false;

    [SerializeField]
    GameObject bombAudio;

    [SerializeField]
    ObjectPool bombs;

    [SerializeField]
    Button exampleButton;

    [SerializeField]
    GameObject panel;

    public void spawnExample() // An example of a bomb that is shown in the questionnaire
    {
        exampleButton.interactable = false;
        panel.SetActive(true);
        if (soundMode == Variation.None || soundMode == Variation.Video)
        {
            channel = 0;
        }
        else if (soundMode == Variation.Slow)
        {
            channel = 1;
        }
        else if (soundMode == Variation.Fast || soundMode == Variation.Both)
        {
            channel = 2;
        }

        source = GetNewSource(channel); 
        var bomb = bombs.Spawn(Level.v2(0, 0.05f), false);

        int explosionDelay = 4;
        int radius = 2;
        int teleDuration = 4;

        float delay = explosionDelay;
        bomb.GetComponent<BombControllerScript>().SetProperties(radius, delay, teleDuration);
        bomb.gameObject.SetActive(true);
        
        StartCoroutine(bombSound(explosionDelay, 0));
    }

    AudioSource GetNewSource(int channelId)
    {
        Transform child = bombAudio.transform;
        GameObject baseSource = bombAudio.transform.GetChild(channelId).gameObject;
        GameObject newSource = Instantiate(baseSource, Vector2.zero, Quaternion.identity) as GameObject;
        newSource.SetActive(true);
        newSource.transform.SetParent(child);
        return newSource.GetComponent<AudioSource>();
    }

    IEnumerator bombSound(int explosionDelay, float stereo)
    {
        source.volume = PlayerPrefs.GetFloat("Volume");
        if (explosionDelay >= 4)
        {
            yield return new WaitForSeconds(explosionDelay - 4);
        }
        else
        {
            source.pitch = (4 * (1 / explosionDelay));
        }
        source.Play();
        StartCoroutine(buttonTimeout(4.5f));
    }

    IEnumerator buttonTimeout(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        exampleButton.interactable = true;
        panel.SetActive(false);
    }

    void Update()
    {
        /*
        if (exampleActive == true)
        {
            exampleButton.interactable = false;
            panel.SetActive(true);
        }
        else if (exampleActive == false)
        {
            exampleButton.interactable = true;
            panel.SetActive(false);
        }
        */
    }
}