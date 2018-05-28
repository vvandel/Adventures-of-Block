using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombAnimation : MonoBehaviour {

    protected Variation soundMode = Log.CurrentMode;

    [SerializeField]
    protected Animator bombAnimator;

    // Use this for initialization
    void Start () {
        if (soundMode != Variation.Video && soundMode != Variation.Both) // CHANGED FOR TESTING PURPOSES
        {
            bombAnimator.Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (soundMode != Variation.Video && soundMode != Variation.Both) // CHANGED FOR TESTING PURPOSES
        {
            bombAnimator.Stop();
        }
    }
}