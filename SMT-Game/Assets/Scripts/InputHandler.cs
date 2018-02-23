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

public class InputHandler : MonoBehaviour {

    [SerializeField]
    PlayerController player;

    float startSpeed = 0.01333f;
    bool transitioning;
    bool speeding;
    float direction;
    float lastInputX;
    float inputX = 0;

    bool leftDown;
    public void LeftArrowPress()
    {
        leftDown = true;
    }

    public void LeftArrowRelease()
    {
        leftDown = false;
    }

    bool rightDown;
    public void RightArrowPress()
    {
        rightDown = true;
    }

    public void RightArrowRelease()
    {
        rightDown = false;
    }

	// Update is called once per frame
	void Update() {
        inputX = 0;
        inputX +=  Input.GetAxisRaw("Horizontal");
        inputX += leftDown ? -1 : 0;
        inputX += rightDown ? 1 : 0;
        inputX = Mathf.Clamp(inputX, -1, 1);

        if (inputX != 0)
            direction = inputX;
        if (inputX != lastInputX)
        {
            transitioning = true;
            speeding = inputX != 0;
        }
        player.SetDirection(inputX);
        lastInputX = inputX;

    }
}
