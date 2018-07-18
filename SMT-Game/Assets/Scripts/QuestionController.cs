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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionController : MonoBehaviour {

    [SerializeField]
    QuestionScript slot1, slot2, slot3;

    [SerializeField]
    MenuScript menu;

    [SerializeField]
    TextMeshProUGUI highScoreText;

    [SerializeField]
    TextMeshProUGUI clarification;

    [SerializeField]
    GameObject exampleButton;

    protected Variation soundMode = Log.CurrentMode;
    int Qpages = 1;
    int currentPage = 0;
    bool skip1 = false;

    public Question[] questions;

    public void OnOK()
    {
        //check if all questions are filled in:
        string s1Answer = slot1.GetAnswer();
        string s2Answer = slot2.GetAnswer();
        string s3Answer = slot3.GetAnswer();
        if (s1Answer == null || s2Answer == null || s3Answer == null)
            return;

        //log answers
        if (slot1.gameObject.activeSelf) Log.WriteSurveyAnswer(currentPage * 3, s1Answer);
        if (slot2.gameObject.activeSelf) Log.WriteSurveyAnswer(currentPage * 3 + 1, s2Answer);
        if (slot3.gameObject.activeSelf) Log.WriteSurveyAnswer(currentPage * 3 + 2, s3Answer);

        currentPage++;
        if (currentPage == 1 && skip1)
            currentPage++;
        if (currentPage < Qpages)
        {
            ShowPage(currentPage);
        }
        else
        {
            if (MenuScript.state == "endQ")
                MenuScript.state = "finished";
            menu.OnNext();
        }
    }
    // Use this for initialization
    void Start() {
        clarification.text = null;
        questions = Questions.getQuestions(soundMode);

        float numberofQuestions = questions.Length;
        Qpages = Mathf.CeilToInt(numberofQuestions / 3);

        //Qpages = MenuScript.state == "endQ" ? 4 : (Log.CurrentMode == Variation.None ? 1 : 2);
        //skip1 = Log.CurrentMode == Variation.None;
        currentPage = 0;
        
        //show the first page
        ShowPage(0);
    }

    public void ShowPage(int i)
    {
        int start = i * 3;
        if (start < questions.Length)
        {
            slot1.gameObject.SetActive(true);
            slot1.SetQuestion(questions[start]);
        }
        else slot1.gameObject.SetActive(false);

        if (start + 1 < questions.Length)
        {
            slot2.gameObject.SetActive(true);
            slot2.SetQuestion(questions[start + 1]);
        }
        else slot2.gameObject.SetActive(false);

        if (start + 2 < questions.Length)
        {
            slot3.gameObject.SetActive(true);
            slot3.SetQuestion(questions[start + 2]);
        }
        else slot3.gameObject.SetActive(false);

        if (soundMode == Variation.Video && (currentPage == 1 || currentPage == 2))
        {
            clarification.text = "*By animations is meant the visual animations and particles corresponding to the game events";
            exampleButton.SetActive(true);
        }
        else if ((soundMode == Variation.Slow || soundMode == Variation.Fast) && (currentPage == 1 || currentPage == 2))
        {
            clarification.text = "*By sound effects is meant the soundtracks that are played leading up to the game events";
            exampleButton.SetActive(true);
        }
        else
        {
            clarification.text = null;
            exampleButton.SetActive(false);
        }
    }
}
