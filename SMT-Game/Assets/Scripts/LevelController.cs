/*
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

public class LevelController : MonoBehaviour {

    [SerializeField]
    string levelToLoad;

    [SerializeField]
    Variation soundMode;

    [SerializeField]
    UnityEngine.UI.Text buttonText;

    [SerializeField]
    GameObject button, background;

    [SerializeField]
    Rigidbody2D player;

	// Use this for initialization
	void Start () {
        levelToLoad = Log.CurrentLevel;
        soundMode = Log.CurrentMode;
        if (levelToLoad == "")
            return;

        System.Type type = System.Type.GetType(levelToLoad);
        Log.SetLevel(soundMode, levelToLoad);
        gameObject.AddComponent(type);
        button.SetActive(false);
        background.SetActive(false);
    }
	
    public void SetLevel(string level)
    {
        levelToLoad = level;
    }
	public void LoadLevel()
    {
        Start();
    }

    public void Finish()
    {
        int triesLeft = 1 - Log.Attempt;
        if (triesLeft > 0)
            buttonText.text = "Try Again (" + triesLeft + ")";
        else
            buttonText.text = "Return";
        button.SetActive(true);
        background.SetActive(true);
        player.constraints |= RigidbodyConstraints2D.FreezeAll;
    }
}
