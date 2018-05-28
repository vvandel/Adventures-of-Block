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

public class CloudScript : MonoBehaviour {

    GameObject lightning;
    protected Variation soundMode = Log.CurrentMode;

    [SerializeField]
    float size = 5, waitTime = 1, thunderTime = 3;

    [SerializeField]
    ParticleSystem rumbleParticles;

    float spawn_time;

    public void SetProperties(float? size, float? waitTime, float? thunderTime)
    {
        //if (gameObject.activeSelf)
            //Debug.LogWarning("Adjusting cloud properties while it's active, behaviour might not be as expected");

        if (size.HasValue)
            this.size = size.Value;
        if (waitTime.HasValue)
            this.waitTime = waitTime.Value;
        if (thunderTime.HasValue)
            this.thunderTime = thunderTime.Value;
    }

    // Use this for initialization
    void Start ()
    {
        lightning = transform.GetChild(0).gameObject;
        spawn_time = Time.time;
        PlayParticles();
    }

    public void PlayParticles()
    {
        if (soundMode == Variation.Video || soundMode == Variation.Both) // CHANGED FOR TESTING PURPOSES
        {
            rumbleParticles.Play();
        }
    }

    public void StopParticles()
    {
        if (rumbleParticles.isPlaying)
        {
            rumbleParticles.Stop();
        }
    }

    void OnEnable()
    {
        Start();
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
    void FixedUpdate()
    {
        if (Time.fixedTime >= spawn_time + 2 * waitTime + 2 * thunderTime)
        {
            lightning.SetActive(false);
            this.OnDisable();
            ObjectPool.Despawn(gameObject, "cloud pool");
        }
        else if (Time.fixedTime >= spawn_time + 2 * waitTime + thunderTime)
        {
            StopParticles();
            lightning.SetActive(true);
        }
        else if (Time.fixedTime >= spawn_time + waitTime + thunderTime)
        {
            lightning.SetActive(false);
        } 
        else if (Time.fixedTime >= spawn_time + waitTime)
        {
            StopParticles();
            lightning.SetActive(true);
        }
        else
        {
            lightning.SetActive(false);
        }
    }
}
