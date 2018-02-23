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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Analyser
{
    class LogResult
    {
        public string[][] attempts;
        public string levelName;
        public string mode;
    }
    class LogParser
    {
        public static string RemoveLastLineBackup(string backupPath, string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string r = lines[lines.Length - 1];
            string[] toWrite = new string[lines.Length - 1];
            Array.Copy(lines, 0, toWrite, 0, lines.Length - 1);
            File.WriteAllLines(backupPath, toWrite);
            return r;
        }

        public static LogResult[] ReadAttempts(string[] logFile)
        {
            string levelName = null, mode = null;
            LogResult[] r = new LogResult[3];
            List<List<string>> attemptBuffers = new List<List<string>>();
            int currentAttempt = 0;
            int currentLevel = 0;
            bool readingAttempt = false;
            foreach (string line in logFile)
            {
                if (line.StartsWith("END"))
                    break;
                if (readingAttempt)
                    if (line.StartsWith("#"))
                    {
                        attemptBuffers[currentAttempt].Add(line);
                        continue;
                    }
                    else if (line.StartsWith("Score"))
                    {
                        readingAttempt = false;
                        currentAttempt++;
                        continue;
                    }

                string[] words = line.Split();
                if (words[0] == "level:")
                    levelName = words[1];
                else if (words[0] == "mode:")
                    mode = words[1];
                else if (words[0] == "Attempt")
                {
                    attemptBuffers.Add(new List<string>());
                    readingAttempt = true;
                }
                else if (words[0] == "High")
                {
                    r[currentLevel] = new LogResult();
                    r[currentLevel].levelName = levelName;
                    r[currentLevel].mode = mode;
                    r[currentLevel].attempts = attemptBuffers.Select((list) => list.ToArray()).ToArray();
                    attemptBuffers.Clear();
                    currentLevel++;
                    currentAttempt = 0;
                    if (currentLevel == 3)
                        break;
                }
            }

            return r;
        }
        
    }
}
