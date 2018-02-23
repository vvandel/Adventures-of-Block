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
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ObjectPool), true)]
public class SpawnInPoolEditor : Editor
{
    bool toggle = true;
    Vector2 loc = new Vector2(0,0);
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ObjectPool pool = target as ObjectPool;

        if(!(target is ObjectContainer))
            pool.clone = EditorGUILayout.Toggle(pool.clone, "Clone");

        EditorGUILayout.Space();

        

        string buttonText = "Spawn";
        string labelText = "Location";
        string label2Text = "Active spawn?";

        loc = EditorGUILayout.Vector2Field(labelText, loc);
        toggle = EditorGUILayout.ToggleLeft(label2Text, toggle);


        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        if (GUILayout.Button(buttonText))
        {
            pool.Spawn(loc, toggle);
        }
        EditorGUI.EndDisabledGroup();
    }
}
