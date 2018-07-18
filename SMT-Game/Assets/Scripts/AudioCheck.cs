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
using TMPro;

public class AudioCheck : MonoBehaviour {

    [SerializeField]
    GameObject AudioCheckObject;

    [SerializeField]
    GameObject DelayedStart;

    [SerializeField]
    GameObject Countdown;

    [SerializeField]
    AudioSource Voice;

    [SerializeField]
    TextMeshProUGUI audioInstruction;

    [SerializeField]
    TextMeshProUGUI beginInstruction;

    public bool buttonPressed = false;

	// Use this for initialization
	void Start () {
        if (MenuScript.state == "trial" || (MenuScript.levelID == 0 && !Log.isFreePlay))
        {
            StartCoroutine(ShowInstruction());
            StartCoroutine(StartDelay());
        }
        else
        {
            AudioCheckObject.gameObject.SetActive(false);
            DelayedStart.gameObject.SetActive(true);
            Countdown.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.X))
        {
            buttonPressed = true;
        }
	}

    IEnumerator ShowInstruction()
    {
        yield return new WaitForSecondsRealtime(12);
        audioInstruction.text = "Attention: If you don't hear anything, please make sure that your audio device is working and activated. If you still don't hear anything, please try refreshing the website";
    }

    IEnumerator StartDelay()
    {
        Time.timeScale = 0;
        beginInstruction.text = "Please listen to the spoken instructions";
        yield return new WaitForSecondsRealtime(2);

        Voice.volume = PlayerPrefs.GetFloat("Volume");
        Voice.Play();
        while (buttonPressed == false)
        {
            yield return 0;
        }
        AudioCheckObject.gameObject.SetActive(false);
        Time.timeScale = 1;
        DelayedStart.gameObject.SetActive(true);
        Countdown.gameObject.SetActive(true);
        audioInstruction.text = null;
        beginInstruction.text = null;
    }
}
