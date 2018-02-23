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

public class LevelButtonScript : MonoBehaviour {

    int prev_score;
    int prev_stars;
    int prev_health;
    public int score, stars, health;

    [SerializeField]
    Transform starsObj;

    [SerializeField]
    UnityEngine.UI.Text scoreText, healthText;

    [SerializeField]
    string scoreTextBase;

    [SerializeField]
    Color activeStarColor, inactiveStarColor;

	// Use this for initialization
	void Start () {
        prev_score = -1;
        prev_stars = -1;
        prev_health = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if (stars != prev_stars)
        {
            for (int i = 0; i < starsObj.childCount; i++)
                starsObj.GetChild(i).GetComponent<UnityEngine.UI.Image>().color =
                    (i < stars) ? activeStarColor : inactiveStarColor;
        }

        if (score != prev_score)
            scoreText.text = scoreTextBase + score;
        if (health != prev_health)
            healthText.text = health.ToString();

        prev_health = health;
        prev_score = score;
        prev_stars = stars;
	}
}
