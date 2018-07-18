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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour {

    public static UserGroup assignedGroup;

    public static WWW wwwNone = null;
    public static WWW wwwVideo = null;
    public static WWW wwwSlow = null;
    public static WWW wwwFast = null;

    public static string noneString = null;
    public static string videoString = null;
    public static string slowString = null;
    public static string fastString = null;

    public static int noneInt;
    public static int videoInt;
    public static int slowInt;
    public static int fastInt;

    private void Start()
    {
        requestNumbers();
    }

    private void Update() // Workaround for coroutines
    {
        if (wwwNone.isDone && noneString == null)
        {
            noneString = wwwNone.text;
            noneInt = int.Parse(noneString);
        }

        if (wwwVideo.isDone && videoString == null)
        {
            videoString = wwwVideo.text;
            videoInt = int.Parse(videoString);
        }

        if (wwwSlow.isDone && slowString == null)
        {
            slowString = wwwSlow.text;
            slowInt = int.Parse(slowString);
        }

        if (wwwFast.isDone && fastString == null)
        {
            fastString = wwwFast.text;
            fastInt = int.Parse(fastString);
        }

        if (noneString != null && videoString != null && slowString != null && fastString != null)
        {
            assignedGroup = assignGroup();
        }
    }

    public UserGroup assignGroup() // Assign the new participant to a group
    {
        UserGroup group = UserGroup.E;

        int[] keys = { noneInt, videoInt, slowInt, fastInt };
        UserGroup[] values = { UserGroup.A, UserGroup.B, UserGroup.C, UserGroup.D };

        Array.Sort(keys, values);
        group = values[0];

        return group;
    }

    public void requestNumbers() // Get the current number of participants in each group
    {
        wwwNone = new WWW("http://www.students.science.uu.nl/~5718171/noneparticipants.php");
        wwwVideo = new WWW("http://www.students.science.uu.nl/~5718171/videoparticipants.php");
        wwwSlow = new WWW("http://www.students.science.uu.nl/~5718171/slowparticipants.php");
        wwwFast = new WWW("http://www.students.science.uu.nl/~5718171/fastparticipants.php");
    }

    public void OnButton()
    {
        SceneManager.LoadScene("menu");
    }
}