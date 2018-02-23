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

public class BombControllerScript : MonoBehaviour
{
    CircleCollider2D bomb_collider;
    CircleDraw circle_drawer;
    GameObject sprite;
    GameObject explosion;

    [SerializeField]
    PlayerController player;

    [SerializeField]
    float radius = 1, explosionDelay = 3, telegraphDuration = 1;

    float spawn_time;
    bool exploded = false;

    public void SetProperties(float? radius, float? explosionDelay, float? telegraphDuration)
    {
        if (gameObject.activeSelf)
            Debug.LogWarning("Adjusting bomb properties while it's active, behaviour might not be as expected");

        if (radius.HasValue)
            this.radius = radius.Value;
        if (explosionDelay.HasValue)
            this.explosionDelay = explosionDelay.Value;
        if (telegraphDuration.HasValue)
            this.telegraphDuration = telegraphDuration.Value;
    }

    void Awake()
    {
        sprite = transform.FindChild("sprite").gameObject;
        explosion = transform.FindChild("explosion").gameObject;
        bomb_collider = GetComponent<CircleCollider2D>();
        circle_drawer = GetComponent<CircleDraw>();
    }
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        sprite.SetActive(true);
        circle_drawer.enabled = true;
        circle_drawer.SetRadius(radius);
        spawn_time = Time.time;
        bomb_collider.radius = radius;
	}
	
    void OnEnable()
    {
        Start();
    }
    void Update()
    {
        if(Time.time >= spawn_time + telegraphDuration)
        {
            circle_drawer.SetEnabled(false);
        }
    }
    void FixedUpdate()
    {
        if(Time.fixedTime >= spawn_time + explosionDelay)
        {
            bomb_collider.enabled = !exploded;
            sprite.SetActive(false);
            explosion.SetActive(true);
            exploded = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.Hit("bomb");
        }
    }
}
