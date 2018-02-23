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

public class ObjectiveScript : MonoBehaviour {

    [SerializeField]
    bool isFulfilled = false;

    [SerializeField]
    float progressValue = 1;

    System.Func<float> getProgress;

    [SerializeField]
    Transform valueBar;

    [SerializeField]
    float maxProgValue = 1;

    [SerializeField]
    bool fulfillAtMaximum = false;
	
	// Update is called once per frame
	void Update () {
        isFulfilled = false;
        if(getProgress != null)
            progressValue = getProgress();
        if (progressValue <= 0)
        {
            if(!fulfillAtMaximum) isFulfilled = true;
            progressValue = 0;
        }
        if(progressValue >= maxProgValue)
        {
            if (fulfillAtMaximum) isFulfilled = true;
            progressValue = maxProgValue;
        }
        valueBar.localScale = new Vector3(progressValue / maxProgValue, valueBar.localScale.y);
	}

    /// <summary>
    /// Bind progress function.
    /// </summary>
    /// <param name="getProg">Function that obtains current progress</param>
    public void BindProgress(System.Func<float> getProg)
    {
        this.getProgress = getProg;
    }

    public void SetMaxValue(float value)
    {
        maxProgValue = value;
    }
}
