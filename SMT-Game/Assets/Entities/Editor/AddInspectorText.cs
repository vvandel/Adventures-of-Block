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