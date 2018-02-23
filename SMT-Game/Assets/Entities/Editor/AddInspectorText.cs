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
using System.Collections;
using UnityEditor;

//https://forum.unity3d.com/threads/add-info-text-notes-into-the-inspector.265330/
/* AlanMattano}}2014*/

[CustomEditor(typeof(InfoTextNote))]
public class AddInspectorText : Editor
{

    private int c = 0;
    private string buttonText = "Start typing";

    public override void OnInspectorGUI()
    {
        InfoTextNote inMyScript = (InfoTextNote)target;

        if (inMyScript.TextInfo == "Start writting text here... " +
           "/n Press Lock when finished.")
            ShowLogMessage();

        if (!inMyScript.isReady)
        {
            buttonText = "EDIT";
            if (GUILayout.Button(buttonText)) inMyScript.SwitchToggle();
            EditorGUILayout.HelpBox(inMyScript.TextInfo, MessageType.None);
        }
        else
        {
            buttonText = "DONE";
            // Button
            if (GUILayout.Button(buttonText)) inMyScript.SwitchToggle();

            // Text
            inMyScript.TextInfo = EditorGUILayout.TextArea(inMyScript.TextInfo, GUILayout.Height(50));

            // warning
            EditorGUILayout.HelpBox(" Press DONE at the top when finished. ", MessageType.Warning); // A Warning box
        }
    }

    void ShowLogMessage()
    {
        c++;
        if (c == 1)
        {

            Debug.Log(" Need to add text " + "\n");
        }
    }
}