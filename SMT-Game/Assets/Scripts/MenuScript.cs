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

public class MenuScript : MonoBehaviour {

    [SerializeField]
    GameObject explain;
    public static string state = "";
    public static int QuestionsAnswered = 0;
    public static int levelID = 0;
    public static string[] levelNames = new string[] { "Level1", "Cloudtopia" , "Bombmania" };
    public static SoundMode[][] soundOrders = new SoundMode[][]
    {
        new SoundMode[] { SoundMode.None, SoundMode.Beat, SoundMode.Generated },
        new SoundMode[] { SoundMode.Generated, SoundMode.Beat, SoundMode.None },
        new SoundMode[] { SoundMode.Beat, SoundMode.None, SoundMode.Generated },
        new SoundMode[] { SoundMode.Beat, SoundMode.Generated, SoundMode.None },
        new SoundMode[] { SoundMode.Generated, SoundMode.None, SoundMode.Beat },
        new SoundMode[] { SoundMode.None, SoundMode.Generated, SoundMode.Beat }
    };
    

	public void OnButton()
    {
        InputField codeField = FindObjectOfType<InputField>();
        int code = int.Parse(codeField.text);
        UserGroup group;
        try
        {
            group = codeDict[code];
        }
        catch
        {
            return;
        }

        Log.Initialize(System.DateTime.Now.ToString("yyyyMMdd HHmmss", System.Globalization.CultureInfo.InvariantCulture) + ".txt");
        Log.StartSession(group, code);
        Log.SetLevel(soundOrders[(int)group][0], levelNames[0]);
        SceneManager.LoadScene("main");
        state = "game";
    }

    public void OnNext()
    {
        if (!Log.IsSessionInProgress)
            throw new Exception("only valid when session in progress");

        if(state == "game")
        {
            Log.EndAttempt();
            if (Log.Attempt < 5)
                SceneManager.LoadScene("main");
            else
            {
                if (Log.IsSessionInProgress)
                {
                    Log.EndLevel();
                }
                state = levelID < 2 ? "midQ" : "endQ";
                SceneManager.LoadScene("question");
            }
            return;
        }
        else if (state == "midQ")
        {
            //go to the next level
            state = "game";
            levelID++;
            Log.SetLevel(soundOrders[(int)Log.UserGroup][levelID], levelNames[levelID]);
            SceneManager.LoadScene("main");
        }
       
        else if (state == "finished")
        {
            state = "";

            Log.EndSession();
            Log.Close();
            SceneManager.LoadScene("exit");
        }
    }

    public void OnClearLogs()
    {
        Log.ClearLogs();
    }

    Dictionary<int, UserGroup> codeDict = new Dictionary<int, UserGroup>()
    {
        { 64464, UserGroup.A },
        { 51627, UserGroup.A },
        { 39174, UserGroup.A },
        { 85733, UserGroup.A },
        { 71187, UserGroup.A },
        { 28826, UserGroup.A },
        { 37984, UserGroup.A },
        { 32452, UserGroup.A },
        { 42984, UserGroup.A },
        { 93534, UserGroup.A },

        { 20242, UserGroup.B },
        { 35534, UserGroup.B },
        { 38378, UserGroup.B },
        { 61380, UserGroup.B },
        { 51091, UserGroup.B },
        { 29718, UserGroup.B },
        { 65944, UserGroup.B },
        { 64584, UserGroup.B },
        { 93775, UserGroup.B },
        { 27332, UserGroup.B },

        { 12250, UserGroup.C },
        { 79355, UserGroup.C },
        { 92895, UserGroup.C },
        { 34799, UserGroup.C },
        { 50417, UserGroup.C },
        { 22021, UserGroup.C },
        { 74962, UserGroup.C },
        { 18991, UserGroup.C },
        { 49336, UserGroup.C },
        { 23053, UserGroup.C },

        { 35830, UserGroup.D },
        { 10269, UserGroup.D },
        { 40456, UserGroup.D },
        { 68204, UserGroup.D },
        { 57627, UserGroup.D },
        { 10921, UserGroup.D },
        { 34796, UserGroup.D },
        { 26309, UserGroup.D },
        { 80434, UserGroup.D },
        { 23714, UserGroup.D },

        { 24138, UserGroup.E },
        { 29906, UserGroup.E },
        { 84030, UserGroup.E },
        { 18072, UserGroup.E },
        { 68787, UserGroup.E },
        { 31545, UserGroup.E },
        { 41777, UserGroup.E },
        { 74393, UserGroup.E },
        { 62115, UserGroup.E },
        { 66949, UserGroup.E },

        { 49432, UserGroup.F },
        { 76517, UserGroup.F },
        { 72592, UserGroup.F },
        { 48791, UserGroup.F },
        { 73834, UserGroup.F },
        { 54326, UserGroup.F },
        { 25069, UserGroup.F },
        { 82653, UserGroup.F },
        { 80251, UserGroup.F },
        { 13232, UserGroup.F }
    };
}
