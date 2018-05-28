﻿/*
The MIT License (MIT)

Copyright (c) 2018 Twan Veldhuis, Ivar Troost

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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicController : MonoBehaviour {

    //0 = background
    //1 = bomb
    //2 = flood
    //3 = lightning
    //4 = beat
    //5 = bomb2 (test)

    Dictionary<string, int> channelDict = new Dictionary<string, int>()
    {
        { "bombNone", 0 },
        { "bombVideo", 0 },
        { "bombSlow", 1 },
        { "bombFast", 2 },
        { "cloudNone", 3 },
        { "cloudVideo", 3 },
        { "cloudSlow", 4 },
        { "cloudFast", 5 },
        { "starNone", 6 },
        { "starVideo", 6 },
        { "starSlow", 7 },
        { "starFast", 8 },
        { "floodNone", 9 },
        { "floodVideo", 9 },
        { "floodSlow", 10 },
        { "floodFast", 11 },

        // CHANGED FOR TESTING PURPOSES
        { "bombBoth", 2 },
        { "cloudBoth", 5 },
        { "starBoth", 8 },
        { "floodBoth", 11 },
    };

    //protected AudioSource bgMusic;
    //protected AudioSource floodMusic;
    protected Variation soundMode = Log.CurrentMode;

    //bool bgFadeOut = false;
    //bool bgFadeIn = false;
    //bool floodFadeIn = false;

    AudioSource GetNewSource(int channelId)
    {
        //if (channelId == 0 || channelId == 2 || channelId == 4)

        //return transform.GetChild(channelId).GetComponent<AudioSource>();

        /*
        else
        {
            Transform child = transform.GetChild(channelId);
            GameObject baseSource = child.GetChild(0).gameObject;
            GameObject newSource = Instantiate(baseSource, Vector2.zero, Quaternion.identity) as GameObject;
            newSource.SetActive(true);
            newSource.transform.SetParent(child);
            return newSource.GetComponent<AudioSource>();
        }
        */

        Transform child = transform.GetChild(channelId);
        GameObject baseSource = transform.GetChild(channelId).gameObject;
        GameObject newSource = Instantiate(baseSource, Vector2.zero, Quaternion.identity) as GameObject;
        newSource.SetActive(true);
        newSource.transform.SetParent(child);
        return newSource.GetComponent<AudioSource>();
    }

    void StopSource(AudioSource source, int channelId)
    {
        source.Stop();
        Destroy(source.gameObject);
    }

    IEnumerator PlayForDuration(int channel, float duration, float stereo = 0)
    {
        var source = GetNewSource(channel);
        source.volume = PlayerPrefs.GetFloat("Volume");
        source.pitch = 1 / Level.getTickDuration();
        if (duration == -1)
            duration = source.clip.length * Level.getTickDuration();
        source.panStereo = stereo;
        source.Play();
        yield return new WaitForSeconds(duration);
        StopSource(source, channel);
    }

    IEnumerator bombCue(float explosionDelay, float stereo)
    {
        var channel = channelDict["bomb" + soundMode.ToString()];
        var source = GetNewSource(channel);
        source.volume = PlayerPrefs.GetFloat("Volume");
        if (explosionDelay >= 4)
        {
            yield return new WaitForSeconds(explosionDelay - 4);
        }
        else
        {
            source.pitch = (4*(1/explosionDelay));
        }

        source.Play();
    }

    /*
    public void PlayBackground()
    {
        bgMusic = GetNewSource(0);
        bgMusic.volume = PlayerPrefs.GetFloat("Volume");
        bgMusic.pitch = 1/Level.getTickDuration();
        bgMusic.Play();
    }

    public void PlayBeat()
    {
        bgMusic = GetNewSource(4);
        bgMusic.volume = PlayerPrefs.GetFloat("Volume");
        bgMusic.pitch = 1 / Level.getTickDuration();
        bgMusic.Play();
    }
    */

    /*
    void Update()
    {
        if(bgFadeOut)
        {
            if (bgMusic.volume > 0)
            {
                bgMusic.volume -= Level.getTickDuration() * Time.deltaTime;
            }
            else
            {
                bgFadeOut = false;
            }
        }
        if (bgFadeIn)
        {
            if (bgMusic.volume < 1)
            {
                bgMusic.volume += Level.getTickDuration() * Time.deltaTime;
            }
            else
            {
                bgFadeIn = false;
            }
        }
        if (floodFadeIn)
        {
            // SOUNDS BETTER WITHOUT FADE
            //if (floodMusic.volume < 1)
            //{
            //    floodMusic.volume += 0.1f * Level.getTickDuration() * Time.deltaTime;
            //}
            //else
            //{
            //    floodFadeIn = false;
            //}
            floodFadeIn = false;
            floodMusic.volume = 1;
        }
    }
    */

    /*
    public void FadeOutBGMusic()
    {
        bgFadeOut = false;
    }
    public void FadeInBGMusic()
    {
        bgFadeIn = false;
    }
    */

    public void StartBombCue(float explosionDelay, float stereo)
    {
        StartCoroutine(bombCue(explosionDelay, stereo));
    }

    IEnumerator CloudCue(float duration, float stereo)
    {
        var channel = channelDict["cloud" + soundMode.ToString()];
        yield return PlayForDuration(channel, duration, stereo);
    }

    public void StartCloudCue(float duration, float stereo)
    {
        StartCoroutine(CloudCue(duration, stereo));
    }

    public void StartFloodCue()
    {
        var channel = channelDict["flood" + soundMode.ToString()];
        var floodMusic = GetNewSource(channel);
        floodMusic.volume = PlayerPrefs.GetFloat("Volume");
        floodMusic.pitch = 1 / Level.getTickDuration();
        floodMusic.Play();
    }

    public void StartStarCue()
    {
        var channel = channelDict["star" + soundMode.ToString()];
        var starMusic = GetNewSource(channel);
        starMusic.volume = PlayerPrefs.GetFloat("Volume");
        starMusic.pitch = 1 / Level.getTickDuration();
        starMusic.Play();
    } 
}