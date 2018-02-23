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

public class QuestionController : MonoBehaviour {

    [SerializeField]
    QuestionScript slot1, slot2, slot3;

    [SerializeField]
    MenuScript menu;

    [SerializeField]
    UnityEngine.UI.Text highScoreText;

    Question[] questions = new Question[16];

    int Qpages = 1, currentPage = 0;
    bool skip1 = false;

    int[] optionSelections = new int[] { 0, 0, 0, 0, 0, 0,
        1, 2, 1,
        2, 0, 0,
        0, 2, 3,
        2 };
    int[] options = new int[] { 5, 5, 5, 5, 5, 5,
        4, 0, 4,
        0, 5, 5,
        5, 0, 3,
        0 };

    string[][] optionTexts =
    {
        new string[] { "1. Strongly agree", "2. Agree", "3. Undecided", "4. Disagree", "5. Strongly disagree" },
        new string[] { "1. Without sound","2. With the beat","3. With the music","4. No preference", "" },
        new string[] { "", "", "", "", "" },
        new string[] { "Male", "Female", "Other", "", "" }
    };

    string[] qText = new string[]
    {
        "The level was enjoyable",
        "The level was hard",
        "I performed well in the level",

        "The sound fitted the level",
        "The sound was enjoyable",
        "The sound can be classified as music",

        "Which version of the game did you like better?",
        "Why did you like that version better?",
        "Which version do you feel you performed better in?",

        "Why do you think you did better in said version?",
        "I believe the music helped me get higher scores in the game.",
        "The music distracted me in playing the game.",

        "I consider myself an experienced gamer.",
        "What is your age?",
        "What is your gender?",

        "Is there anything else you would like to share?"
    };

    public void OnOK()
    {
        //check if all questions are filled in:
        string s1Answer = slot1.GetAnswer();
        string s2Answer = slot2.GetAnswer();
        string s3Answer = slot3.GetAnswer();
        if (s1Answer == null || s2Answer == null || s3Answer == null)
            return;

        //log answers
        if(slot1.gameObject.activeSelf) Log.WriteSurveyAnswer(currentPage * 3, s1Answer);
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
	void Start () {
        for(int i = 0; i < questions.Length; i++)
        {
            questions[i] = new Question();
            questions[i].text = qText[i];
            questions[i].options = options[i];
            questions[i].optionText = optionTexts[optionSelections[i]];
        }
        questions[13].contentType = UnityEngine.UI.InputField.ContentType.IntegerNumber;
        Qpages = MenuScript.state == "endQ" ? 6 : (Log.CurrentMode == SoundMode.None ? 1 : 2);
        skip1 = Log.CurrentMode == SoundMode.None;
        currentPage = 0;

        highScoreText.text = "Highscore for previous level: " + Log.HighScore;
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

        if (start + 2< questions.Length)
        {
            slot3.gameObject.SetActive(true);
            slot3.SetQuestion(questions[start + 2]);
        }
        else slot3.gameObject.SetActive(false);

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
