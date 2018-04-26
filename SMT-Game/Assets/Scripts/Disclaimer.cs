using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Disclaimer : MonoBehaviour {

    public void OnButton()
    {
        Toggle agreeToggle = FindObjectOfType<Toggle>();
        if (agreeToggle.isOn){
            SceneManager.LoadScene("menu");
        }
    }
}