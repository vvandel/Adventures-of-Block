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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// keeps global game state information
/// </summary>
public static class State
{
    public static SoundMode SoundMode { get; set; }
    public static string CurrentLevel { get; set; }
    public static LevelState CurrentLevelState { get; set; }
}

/// <summary>
/// Stores state relevant to a given level
/// </summary>
public class LevelState
{
    /// <summary>
    /// Health of the player
    /// </summary>
    public int Health = 3;
    public float StartTime;
    /// <summary>
    /// The player's score
    /// </summary>
    public int Score;
    /// <summary>
    /// The current game tick
    /// </summary>
    public int Tick;
    /// <summary>
    /// The length of the level in #ticks.
    /// </summary>
    public int Length;
    /// <summary>
    /// The health of the boss, if present (-1 if no boss present)
    /// </summary>
    public int BossHealth = -1;
    public int BossMaxHealth = -1;
}