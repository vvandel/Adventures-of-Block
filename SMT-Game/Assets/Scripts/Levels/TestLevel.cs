using UnityEngine;
using System.Collections;

public class TestLevel : Level
{
    
    protected override IEnumerator LevelScript()
    {
        //0
        var bomb1 = spawnBomb(-0.5f, 0.05f, 4, 4, 2);
        var star1 = stars.Spawn(v2(0.9f, 1f), true);
        var star2 = stars.Spawn(v2(-0.9f, 1f), true);
        PlayBackground();
        yield return WaitTick(4);
        //4
        var cloud1 = spawnCloud(-0.5f, 0.05f, 1, 2, 2);
        yield return WaitTick(4);
        //8
        var star3 = stars.Spawn(v2(-0.2f, 1f), true);
        yield return WaitTick(3);
        //11
        ObjectPool.Despawn(star1, "star pool");
        ObjectPool.Despawn(star2, "star pool");
        StartFlood();
        yield return WaitTick(1);
        //12
        ObjectPool.Despawn(cloud1, "cloud pool");
        yield return WaitTick(4);
        //16
        EndFlood();
        ObjectPool.Despawn(star3, "star pool");
        yield return WaitTick(4);
        yield return base.LevelScript();
    }
}
