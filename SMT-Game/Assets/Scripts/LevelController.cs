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

public class LevelController : MonoBehaviour {

    [SerializeField]
    string levelToLoad;

    [SerializeField]
    SoundMode soundMode;

    [SerializeField]
    OverlayScript overlay;

    [SerializeField]
    ObjectiveScript timeObjective, bossObjective;

    [SerializeField]
    Rigidbody2D player;

    Level loadedLevel;

	// Use this for initialization
	void Start () {
        levelToLoad = State.CurrentLevel;
        soundMode = State.SoundMode;
        if (levelToLoad == "")
            return;

        if (levelToLoad.EndsWith(".lds"))
        {
            var level = gameObject.AddComponent<TextLevel>();
            level.LevelName = "Levels/" + levelToLoad;
            loadedLevel = level;
        }
        else
        {
            System.Type type = System.Type.GetType(levelToLoad);
            loadedLevel = gameObject.AddComponent(type) as Level;
        }
        overlay.CloseOverlay();

    }
	
    public void SetLevel(string level)
    {
        levelToLoad = level;
    }
	public void LoadLevel()
    {
        Start();
    }

    bool objectivesSet = false;
    void Update()
    {
        if(!objectivesSet && loadedLevel.HasLoaded)
        {
            State.CurrentLevelState.StartTime = Time.time;
            timeObjective.BindProgress(() => Time.time - State.CurrentLevelState.StartTime);
            timeObjective.SetMaxValue(State.CurrentLevelState.Length);
            bossObjective.BindProgress(() => State.CurrentLevelState.BossHealth);
            bossObjective.SetMaxValue(State.CurrentLevelState.BossMaxHealth);
            objectivesSet = true;
        }
        if(State.CurrentLevelState.Health <= 0)
        {
            Finish(false);
        }
    }

    public void Finish(bool success = true)
    {
        if (success)
            overlay.ShowLevelFinishedOverlay();
        else
            overlay.ShowGameOverOverlay();
        player.constraints |= RigidbodyConstraints2D.FreezeAll;
        loadedLevel.SendMessage("Stop");
    }
}
