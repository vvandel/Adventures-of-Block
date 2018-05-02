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
    public static string[] levelNames = new string[] { "Level1", "Cloudtopia", "Bombmania" };
    public static Variation[][] soundOrders = new Variation[][]
    {
        new Variation[] { Variation.None, Variation.None, Variation.None },
        new Variation[] { Variation.Video, Variation.Video, Variation.Video },
        new Variation[] { Variation.Slow, Variation.Slow, Variation.Slow },
        new Variation[] { Variation.Fast, Variation.Fast, Variation.Fast }
    };


    public void OnButton()
    {
        InputField codeField = FindObjectOfType<InputField>();

        UserGroup group;
        try
        {
            int code = int.Parse(codeField.text);
            group = codeDict[code];
        }
        catch
        {
            System.Random rnd = new System.Random();
            group = codeDict[rnd.Next(1, 5)];
        }

        Log.Initialize(System.DateTime.Now.ToString("yyyyMMdd HHmmss"));
        Log.StartSession(group);
        Log.SetLevel(soundOrders[(int)group][0], levelNames[0]);
        SceneManager.LoadScene("main");
        state = "game";
    }

    public void OnTrialButton()
    {
        InputField codeField = FindObjectOfType<InputField>();

        UserGroup group;
        try
        {
            int code = int.Parse(codeField.text);
            group = codeDict[code];
        }
        catch
        {
            System.Random rnd = new System.Random();
            group = codeDict[rnd.Next(1, 5)];
        }

        Log.Initialize(System.DateTime.Now.ToString("yyyyMMdd HHmmss"));
        Log.StartSession(group);
        Log.SetLevel(soundOrders[(int)group][0], "TestLevel");
        SceneManager.LoadScene("main");
        state = "trial";
    }

    public void OnNext()
    {
        if (!Log.IsSessionInProgress)
            throw new Exception("only valid when session in progress");

        if(state == "game")
        {
            Log.EndAttempt();
            if (Log.Attempt < 1)
                SceneManager.LoadScene("main");
            else
            {
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
            SceneManager.LoadScene("exit");
        }

        else if (state == "trial")
        {
            Log.EndAttempt();
            if (Log.Attempt < 1)
                SceneManager.LoadScene("main");
            else
            {
                SceneManager.LoadScene("menu");
            }
            Log.EndSession();
            return;
        }
    }

    Dictionary<int, UserGroup> codeDict = new Dictionary<int, UserGroup>()
    {
        { 1, UserGroup.A },

        { 2, UserGroup.B },

        { 3, UserGroup.C },

        { 4, UserGroup.D },
    };
}
