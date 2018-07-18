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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Questions : MonoBehaviour // This class holds the questions and options for the questionnaire for each variant. Questioncontroller retrives them and QuestionScript shows them and handles the input
{
    public static string[][] optionTexts =
    {
        new string[] { "1. Strongly disagree", "2. Disagree", "3. Neither agree nor disagree", "4. Agree", "5. Strongly agree" },
        new string[] { "1. Without sound","2. With the beat","3. With the music","4. No preference", "" },
        new string[] { "", "", "", "", "" },
        new string[] { "Male", "Female", "Other", "", "" }
    };

    static string[] qTextNone = new string[]
    {
        "What is your age?",
        "What is your gender?",
        "I consider myself an experienced gamer",

        "The game was enjoyable",
        "The game was hard",
        "Were there any technical issues while playing the game?",

        "Is there anything else that you think may have impacted your performance in the game?",
    };

    static int[] optionsNone = new int[]
    {
        0, 3, 5,
        5, 5, 0,
        0
    };

    static int[] optionSelectionsNone = new int[]
    {
        2, 3, 0,
        0, 0, 2,
        2
    };

    static string[] qTextVideo = new string[]
    {
        "What is your age?",
        "What is your gender?",
        "I consider myself an experienced gamer",

        "The game was enjoyable",
        "The game was hard",

        "The animations* matched the game events well",
        "The animations* were enjoyable",
        "I believe the animations* helped me in getting higher scores in the game",
        "The animations* distracted me in playing the game",

        "Were there any technical issues while playing the game?",
        "Is there anything else that you think may have impacted your performance in the game?"
    };

    static int[] optionsVideo = new int[]
    {
        0, 3, 5,
        5, 5,
        5, 5, 5, 5,
        0, 0
    };

    static int[] optionSelectionsVideo = new int[]
    {
        2, 3, 0,
        0, 0,
        0, 0, 0, 0,
        2, 2
    };

    static string[] qTextAudio = new string[]
    {
        "What is your age?",
        "What is your gender?",
        "I consider myself an experienced gamer",

        "The game was enjoyable",
        "The game was hard",

        "The sound effects* matched the game events well",
        "The sound effects* were enjoyable",
        "I believe the sound effects* helped me in getting higher scores in the game",
        "The sound effects* distracted me in playing the game",

        "Were there any technical issues while playing the game?",
        "Is there anything else that you think may have impacted your performance in the game?"
    };

    static int[] optionsAudio = new int[]
    {
        0, 3, 5,
        5, 5,
        5, 5, 5, 5,
        0, 0
    };

    static int[] optionSelectionsAudio = new int[]
    {
        2, 3, 0,
        0, 0,
        0, 0, 0, 0,
        2, 2
    };

    public static Question[] getQuestions(Variation soundMode) // Workaround for getting the questions for the right variant
    {
        Question[] questions = null;

        if (soundMode == Variation.None)
        {
            questions = new Question[qTextNone.Length];
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i] = new Question();
                questions[i].text = qTextNone[i];
                questions[i].options = optionsNone[i];
                questions[i].optionText = optionTexts[optionSelectionsNone[i]];
            }
        }

        else if (soundMode == Variation.Video)
        {
            questions = new Question[qTextVideo.Length];
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i] = new Question();
                questions[i].text = qTextVideo[i];
                questions[i].options = optionsVideo[i];
                questions[i].optionText = optionTexts[optionSelectionsVideo[i]];
            }
        }

        else if (soundMode == Variation.Slow || soundMode == Variation.Fast)
        {
            questions = new Question[qTextAudio.Length];
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i] = new Question();
                questions[i].text = qTextAudio[i];
                questions[i].options = optionsAudio[i];
                questions[i].optionText = optionTexts[optionSelectionsAudio[i]];
            }
        }
        
        questions[0].isAge = true;
        return questions;
    }
}