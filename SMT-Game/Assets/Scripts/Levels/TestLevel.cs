using UnityEngine;
using System.Collections;

public class TestLevel : Level
{
    
    protected override IEnumerator LevelScript()
    {
        //0
        yield return WaitTick(1);
        spawnBomb(-0.5f, 0.05f, 4, 4, 2);
        var star1 = spawnStar(0.9f);
        var star2 = spawnStar(-0.9f);
        //PlayBackground();
        yield return WaitTick(4);
        //4
        var cloud1 = spawnCloud(-0.5f, 1, 1, 2, 2);
        yield return WaitTick(4);
        //8
        var star3 = spawnStar(-0.2f);
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
        yield return WaitTick(3);
        yield return base.LevelScript();
    }
}
