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
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour {


    [SerializeField]
    float speed;

    [SerializeField]
    Text tickText;

    [SerializeField]
    TextMeshProUGUI scoreText;
        
    [SerializeField]
    TextMeshProUGUI groupText;

    int lastTick;
    int prevScore = 0;
    public int Score = 0;

    Vector3 initialPosition;

    Rigidbody2D rbody;
    public void SetDirection(float direction)
    {
        var y = rbody.velocity.y;
        if(direction != 0)
        rbody.velocity = new Vector2(direction * (speed*(1/Level.getTickDuration())), y);
    }

    public void Hit(string name)
    {
        int delta = 0;
        if (name == "bomb" || name == "lightning")
            delta = -1;
        else if (name == "water")
            delta = -5;
        else if (name == "star")
            delta = 1;
        else if (name == "falling star")
            delta = 2;
        Score += delta;
        string dstr = ((delta > 0) ? "+" : "") + delta;
        Log.WriteHit(dstr + " " + name + " " + transform.position, Time.time, Score);
    }

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        initialPosition = rbody.position;
        groupText.text = "Group: " + Log.UserGroup.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        if (Score != prevScore)
            scoreText.text = "Score: " + Score.ToString();
        if (Log.Tick != lastTick)
            tickText.text = Log.Tick.ToString();
        lastTick = Log.Tick;
        prevScore = Score;
	}
}
