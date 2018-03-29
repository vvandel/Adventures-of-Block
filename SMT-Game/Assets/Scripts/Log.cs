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
using System.IO;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

public enum UserGroup { A, B, C, D, E, F }

public static class Log {

    static StreamWriter file;
    public static bool IsInitialized { get; private set; }
    static bool sessionInProgress;
    public static bool IsSessionInProgress { get { return sessionInProgress; } }
    public static string CurrentLevel { get; private set; }
    public static SoundMode CurrentMode { get; private set; }
    public static UserGroup UserGroup { get; private set; }
    static int currentTick = 0;
    static float levelStart;
    static int score;
    public static int HighScore = -10000;
    public static int Attempt { get; private set; }

    public static int Tick { get { return currentTick; } }

    static string logFile;
    public static string LogPath { get { return Path.Combine(Directory.GetParent(Application.dataPath).FullName, Path.Combine("Logs", logFile)); } }

    public static void Initialize(string outFile)
    {
        logFile = outFile;
        string root = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Logs");
        if (!Directory.Exists(root))
            Directory.CreateDirectory(root);
        string path = LogPath;
        File.Delete(path);
        file = new StreamWriter(File.OpenWrite(path));
        IsInitialized = true;
    }

    public static void Close()
    {
        file.Close();
    }

    public static void StartSession(UserGroup userGroup, int id)
    {
        UserGroup = userGroup;
        if (sessionInProgress)
            throw new System.InvalidOperationException("A session is already in progress");
        sessionInProgress = true;
        file.WriteLine(userGroup.ToString() + id + " " + System.DateTime.Now.ToString());
        file.WriteLine();
    }

    public static void EndAttempt()
    {
        file.WriteLine("Score: " + score);
        if (score > HighScore)
            HighScore = score;
        file.WriteLine();
    }

    public static void NextTick()
    {
        currentTick++;
    }

    public static void EndSession()
    {
        file.WriteLine("High Score: " + HighScore);
        file.WriteLine("END " + System.DateTime.Now.ToString());
        file.Close();

        // CREATE HASH OF ALL ABOVE TEXT
        byte[] text;
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(@LogPath))
            {
                text = md5.ComputeHash(stream);
            }
        }
        string hash = ToHex(text, false);

        /* ADD HASH TO END OF FILE (MASKED AS "SEED")
        // CAN BE CHECKED BY REMOVING LAST LINE AND UPLOADING HERE
        // http://onlinemd5.com/
        */
        using (StreamWriter sw = File.AppendText(LogPath))
        {
            sw.WriteLine("SEED " + hash);
        }

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
        file.WriteLine("#" + currentTick + " " + str + " " + (time - levelStart));
        score = newScore;
    }

    public static void WriteSurveyAnswer(int id, string answer)
    {
        file.WriteLine("Q" + id + " " + answer);
    }
    public static void SetLevel(SoundMode mode, string levelName)
    {
        if (CurrentLevel != levelName || CurrentMode != mode)
        {
            file.WriteLine("level: " + levelName);
            file.WriteLine("mode: " + mode);
            file.WriteLine();

            CurrentLevel = levelName;
            CurrentMode = mode;
            Attempt = 0;
        }
    }
    public static void ClearLogs()
    {
        var files = System.IO.Directory.GetFiles(LogPath);
        foreach (string file in files)
            System.IO.File.Delete(file);
    }

    public static void StartLevel( float time)
    {
        currentTick = 0;
        levelStart = time;
        file.WriteLine("Attempt " + Attempt++);
    }

    public static void EndLevel()
    {
        file.WriteLine("High Score: " + HighScore);
        file.WriteLine();
    }
}
