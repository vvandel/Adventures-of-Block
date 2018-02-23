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

public class PlayerController : MonoBehaviour {


    [SerializeField]
    float speed;

    [SerializeField]
    Text scoreText, tickText;

    int lastTick;
    int prevScore = 0;
    public int Score = 0;

    Vector3 initialPosition;

    Rigidbody2D rbody;
    public void SetDirection(float direction)
    {
        var y = rbody.velocity.y;
        rbody.velocity = new Vector2(direction * (speed*(1/Level.getTickDuration())), y);
    }

    public void Hit(string name)
    {
        int dHealth = 0, dScore = 0;
        if (name == "bomb" || name == "lightning")
            dHealth = -1;
        else if (name == "water")
            dHealth = -3;
        else if (name == "star")
            dScore = 1;
        else if (name == "falling star")
            dScore = 2;
        State.CurrentLevelState.Health += dHealth;
        State.CurrentLevelState.Score += dScore;
    }

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        initialPosition = rbody.position;
	}
	
	// Update is called once per frame
	void Update () {
        Score = State.CurrentLevelState.Score;
        if (Score != prevScore)
            scoreText.text = Score.ToString();
        if (State.CurrentLevelState.Tick != lastTick)
            tickText.text = State.CurrentLevelState.Tick.ToString();
        lastTick = State.CurrentLevelState.Tick;
        prevScore = Score;
	}
}
