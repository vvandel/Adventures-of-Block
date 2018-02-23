using UnityEngine;
using System.Collections;

public enum SoundMode
{
    None, Beat, Generated
}

public class Level : MonoBehaviour {

    public bool HasLoaded { get; protected set; }


    protected ObjectPool bombs, clouds, stars, rafts;
    protected WaterScript water;
    protected MusicController music;

    protected LevelState baseState = new LevelState();

    [SerializeField]
    //protected SoundMode soundMode = SoundMode.Generated;
    protected SoundMode soundMode = SoundMode.Generated;
    protected static float tickDuration = 1f; // 1 = 120bpm
    protected float nextTick;

    bool finished = false;
    bool bossDead = true; //if no boss, it's already dead at the start.
    public static float getTickDuration() {
        return tickDuration;
        }


    public void Stop()
    {
        StopAllCoroutines();
    }
    // Use this for initialization
    void Start () {
        HasLoaded = false;
        bombs = GameObject.Find("bomb pool").GetComponent<ObjectPool>();
        clouds = GameObject.Find("cloud pool").GetComponent<ObjectPool>();
        stars = GameObject.Find("star pool").GetComponent<ObjectPool>();
        rafts = GameObject.Find("raft pool").GetComponent<ObjectPool>();
        water = GameObject.Find("water").GetComponent<WaterScript>();
        music = GameObject.Find("MusicController").GetComponent<MusicController>();
        nextTick = Time.time + tickDuration;
        soundMode = State.SoundMode;
        State.CurrentLevelState = baseState;

        StartCoroutine(LevelScript());
	}

    void Update()
    {
        if(finished)
        {
            FindObjectOfType<LevelController>().Finish();
        }
    }



    protected GameObject spawnBomb(float x, float y = 0.05f, float? radius = 4, float explosionDelay = 4, float? teleDuration = 2)
    {
        var bomb = bombs.Spawn(v2(x, y), false);
        float delay = explosionDelay * tickDuration;
        bomb.GetComponent<BombControllerScript>().SetProperties(radius, delay, teleDuration * tickDuration);
        bomb.gameObject.SetActive(true);

        if (soundMode==SoundMode.Generated)
            music.StartBombCue(delay, x);
        
        return bomb;
    }


    protected GameObject spawnCloud(float x, float y = 1f, float? size = 1f, float? waitDuration = 2f, float? thunderDuration = 2f, bool halfDuration = false)
    {
        var cloud = clouds.Spawn(v2(x, y), true);
        float? waitTime = waitDuration * tickDuration;
        float? thunderTime = thunderDuration * tickDuration;
        cloud.GetComponent<CloudScript>().SetProperties(1, waitTime, thunderTime);
        cloud.gameObject.SetActive(true);

        if (soundMode == SoundMode.Generated)
            music.StartCloudCue((halfDuration) ? 4 * tickDuration : 8 * tickDuration, x);
        
        return cloud;
    }

    protected void StartFlood()
    {
        water.State = WaterState.Flood;
        if (soundMode == SoundMode.Generated)
        {
            music.StartFloodCue();
            music.FadeOutBGMusic();
        }
    }

    protected void EndFlood()
    {
        water.State = WaterState.Ebb;
        if (soundMode == SoundMode.Generated)
        {
            //music.EndFloodCue();
            music.FadeInBGMusic();
        }
    }


    protected void PlayBackground()
    {
        if (soundMode == SoundMode.Generated)
            music.PlayBackground();
        if (soundMode == SoundMode.Beat)
            music.PlayBeat();
    }


    private bool hasNextTickPassed()
    {
        if(Time.time >= nextTick)
        {
            nextTick += tickDuration;
            return true;
        }
        return false;
    }

   
    protected IEnumerator WaitTick(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitUntil(hasNextTickPassed);
            State.CurrentLevelState.Tick++;
        }
    }

    public static Vector2 v2(float x, float y)
    {
        //left floor = -6.38, -0.44
        //right floor = 6.38, -0.44
        //ebb level = -4
        //cloud height is around 3.
        //This provides a scaling method so everything on the screen can be defined from -1 to 1.
        return new Vector2(x * 6.38f, y * 3.56f - 0.44f);
    }

    protected virtual IEnumerator LevelScript()
    {
        HasLoaded = true;
        yield return new WaitUntil(() => State.CurrentLevelState.Tick >= State.CurrentLevelState.Length);
        finished = true;
        yield return null;
    }

}
