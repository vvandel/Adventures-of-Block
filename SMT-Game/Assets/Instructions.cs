using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour {

    public void OnButton()
    {
        {
            SceneManager.LoadScene("menu");
        }
    }
}