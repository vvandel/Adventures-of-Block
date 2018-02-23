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
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;

public class MenuScript : MonoBehaviour {

    [SerializeField]
    GameObject explain;

    [SerializeField]
    Dropdown levelSelect;
    public static string state = "";
    public static int QuestionsAnswered = 0;
    public static int levelID = 0;
    public static string[] levelNames = new string[] { "Level1", "Cloudtopia" , "Bombmania", "Level_1.lds", "TextLevel" };

    string format(string s)
    {
        return s.Replace(".lds", "").Replace("_", " ");
    }
    void Start()
    {
       // var res = Tokenizer.Segmentize(new string[] { "some (1 + 1) + test \"is so absolutely fantasic\" amiright?" });

        if (state == "" || state == "menu")
        {
            levelSelect.options.Clear();
            levelSelect.options.AddRange(levelNames.Select((s) => { s = format(s); return new Dropdown.OptionData(s); }));
        }
    }

    public void OnButton()
    {
        int levelId = levelSelect.value;
        State.SoundMode = SoundMode.Generated;
        State.CurrentLevel = levelNames[levelId];
        SceneManager.LoadScene("main");
        state = "game";
    }

    public void OnReturnToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("main");
    }
    
    public void OnClearLogs()
    {
      //  Log.ClearLogs();
    }
    
}
