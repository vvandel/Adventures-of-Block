/*
The MIT License (MIT)

Copyright (c) 2018 Victor van Andel, Chun He
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
using System.IO;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

public enum UserGroup { A, B, C, D, E } // CHANGED FOR TESTING PURPOSES

public static class Log {

    public static string logContent;
    public static string logTitle;
    public static bool IsInitialized { get; private set; }
    static bool sessionInProgress;
    public static bool IsSessionInProgress { get { return sessionInProgress; } }
    public static string CurrentLevel { get; private set; }
    public static Variation CurrentMode { get; private set; }
    public static UserGroup UserGroup { get; private set; }
    static int currentTick = 0;
    static float levelStart;
    static float score;
    public static float totalScore;
    static float averageScore;
    public static float HighScore = -10000;
    public static int Attempt { get; private set; }
    public static int Tick { get { return currentTick; } }
    public static bool isFreePlay = false;

    public static void Initialize(string outFile)
    {
        IsInitialized = true;
        logTitle = outFile;
    }

    public static void StartSession(UserGroup userGroup)
    {
        UserGroup = userGroup;
        if (sessionInProgress)
            throw new System.InvalidOperationException("A session is already in progress");
        sessionInProgress = true;
        logContent = "START " + System.DateTime.Now.ToString() + "\r\nGroup: " + userGroup.ToString();
    }

    public static void EndAttempt()
    {
        logContent += "\r\nScore: " + score + "\r\n";
        if (MenuScript.state != "trial")
        {
            totalScore += score;
        }
    }

    public static void NextTick()
    {
        currentTick++;
    }

    public static void EndSession()
    {
        averageScore = totalScore / 3;
        if (totalScore > HighScore)
        {
            HighScore = totalScore;
        }
        logContent += "\r\n\r\nTotal Score: " + totalScore;
        logContent += "\r\nAverage Score: " + averageScore;
        logContent += "\r\nEND " + System.DateTime.Now.ToString();

        sessionInProgress = false;
    }

    public static string ToHex(byte[] bytes, bool upperCase)
    {
        StringBuilder result = new StringBuilder(bytes.Length * 2);

        for (int i = 0; i < bytes.Length; i++)
            result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

        return result.ToString();
    }

    public static void WriteHit(string str, float time, int newScore)
    {
        logContent += "\r\n#" + currentTick + " " + str + " " + (time - levelStart);
        score = newScore;
    }

    public static void WriteSurveyAnswer(int id, string answer)
    {
        logContent += "\r\nQ" + id + " " + answer;
    }
    public static void SetLevel(Variation mode, string levelName)
    {
        if (CurrentLevel != levelName || CurrentMode != mode)
        {
            logContent += "\r\n\r\nLevel: " + levelName;
            logContent += "\r\nMode: " + mode + "\r\n";

            CurrentLevel = levelName;
            CurrentMode = mode;
            Attempt = 0;
        }
    }

    public static void StartLevel( float time)
    {
        currentTick = 0;
        levelStart = time;
        logContent += "\r\nAttempt " + Attempt++;
    }
}