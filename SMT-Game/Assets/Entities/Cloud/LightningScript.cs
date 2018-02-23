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

[RequireComponent(typeof(BoxCollider2D))]
public class LightningScript : MonoBehaviour {

    [SerializeField]
    PlayerController player;

    [SerializeField]
    float duration;

    float endTime;

    BoxCollider2D coll;

    [SerializeField]
    Transform sprite1;

    float xoffset, spriteOffset;

    void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }
	// Use this for initialization
	void Start () {
        //xscale = 2.6 -> sprite1.x = 0.25, x = 0
        //xscale = 1.89 -> sprite1.x = 0.083, x = -0.23
        float xscale = transform.parent.localScale.x;
        xoffset = 0.23f * (xscale - 2.6f) / (2.6f - 1.89f);
        spriteOffset = (0.083f - 0.25f) * (xscale - 2.6f) / (2.6f - 1.89f);

        sprite1.localPosition += new Vector3(spriteOffset, 0, 0);
        transform.localPosition += new Vector3(xoffset, 0, 0);
        OnEnable();
	}
	
    void OnEnable()
    {
        coll.enabled = true;
        endTime = Time.time + duration;
    }

    void Update()
    {
        if(Time.time >= endTime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        sprite1.localPosition -= new Vector3(spriteOffset, 0, 0);
        transform.localPosition -= new Vector3(xoffset, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.Hit("lightning");
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1.7f), ForceMode2D.Impulse);
        }
    }
}
