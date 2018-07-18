﻿/*
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

[System.Serializable]
public class Question
{
    public int options;
    public string text;
    public string[] optionText = new string[5];
    public InputField.ContentType contentType = InputField.ContentType.Standard;
    public bool isAge = false;
}

public class QuestionScript : MonoBehaviour {

    [SerializeField]
    Question question;

    [SerializeField]
    GameObject inputField;

    [SerializeField]
    GameObject toggleGroup;

	// Use this for initialization
	void Start () {
        var text = transform.FindChild("Text").GetComponent<TextMeshProUGUI>();
        text.text = question.text;
        inputField.GetComponent<InputField>().text = "";
        if (question.options <= 0)
        {
            if (question.isAge == true)
            {
                inputField.GetComponent<InputField>().GetComponent<RectTransform>().sizeDelta = new Vector2(100, 30);
                inputField.GetComponent<InputField>().GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, 10);
                inputField.GetComponent<InputField>().contentType = UnityEngine.UI.InputField.ContentType.IntegerNumber;
                inputField.GetComponent<InputField>().characterLimit = 2;
            }
            else
            {
                inputField.GetComponent<InputField>().GetComponent<RectTransform>().sizeDelta = new Vector2(700, 75);
                inputField.GetComponent<InputField>().GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -10);
                inputField.GetComponent<InputField>().contentType = UnityEngine.UI.InputField.ContentType.Standard;
                inputField.GetComponent<InputField>().characterLimit = 0;



                toggleGroup.SetActive(false);
                inputField.SetActive(true);
            }
        }
        else
        {
            toggleGroup.SetActive(true);
            inputField.SetActive(false);
            //inputField.DeactivateInputField();
            for (int i = 0; i < 5; i++)
            {
                var child = toggleGroup.transform.GetChild(i);
                child.GetComponent<Toggle>().isOn = false;
                child.gameObject.SetActive(i < question.options);
                child.FindChild("Label").GetComponent<TextMeshProUGUI>().text = question.optionText[i];
            }
        }

	}

    public string GetAnswer()
    {
        if (question.options == 0)
        {
            return inputField.GetComponent<InputField>().text;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (toggleGroup.transform.GetChild(i).GetComponent<Toggle>().isOn)
                    return question.optionText[i];
            }
        }
        return null;
    }

    public void SetQuestion(Question q)
    {
        question = q;
        Start();
    }
}
