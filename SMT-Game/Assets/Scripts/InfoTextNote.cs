using UnityEngine;
using System.Collections;
//https://forum.unity3d.com/threads/add-info-text-notes-into-the-inspector.265330/

public class InfoTextNote : MonoBehaviour
{

    public bool isReady = true;
    public string TextInfo = "Text here... " +
            "/n Press Lock when finish.";

    public void SwitchToggle()
    {
        isReady = !isReady;
    }

    void Start()
    {
        this.enabled = false; // Disable thi component when game start
    }
}
