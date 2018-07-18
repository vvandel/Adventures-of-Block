using UnityEngine;
using System.Collections;

public class TestLevel : Level
{
    
    protected override IEnumerator LevelScript()
    {
        // Start by showing off each game element seperately
        rafts.Spawn(v2(-0.3f, -1f), true);
        rafts.Spawn(v2(0.35f, -1f), true);
        yield return WaitTick(1);
        spawnBomb(0, 0.05f, 4, 4, 2);
        yield return WaitTick(5);
        spawnCloud (- 0.5f, 1, 1, 2, 2);
        spawnCloud(0.5f, 1, 1, 2, 2);
        yield return WaitTick(9);
        var starnew = spawnStar(-0.3f);
        yield return WaitTick(3);
        StartFlood();
        yield return WaitTick(2);
        ObjectPool.Despawn(starnew, "star pool");
        yield return WaitTick(3);
        EndFlood();

        // Then 30 seconds of simultaneous game elements to prepare them for the real levels
        yield return WaitTick(1);
        spawnBomb(-0.5f, 0.05f, 4, 4, 2);
        var star1 = spawnStar(0.9f);
        var star2 = spawnStar(-0.9f);
        //PlayBackground();
        yield return WaitTick(4);
        //4
        var cloud1 = spawnCloud(-0.5f, 1, 1, 2, 2);
        yield return WaitTick(4);
        ObjectPool.Despawn(star1, "star pool");
        ObjectPool.Despawn(star2, "star pool");
        //8
        var star3 = spawnStar(-0.1f);
        yield return WaitTick(3);
        //11
        StartFlood();
        yield return WaitTick(1);
        //12
        ObjectPool.Despawn(cloud1, "cloud pool");
        ObjectPool.Despawn(star3, "star pool");
        yield return WaitTick(4);
        //16
        EndFlood();
        yield return WaitTick(1);
        //17
        var cloud2 = spawnCloud(0, 1, 1, 2, 2);
        var cloud3 = spawnCloud(-0.7f, 1, 1, 2, 2);
        yield return WaitTick(1);
        var star4 = spawnStar(-0.35f);
        yield return WaitTick(4);
        //21
        var star5 = spawnStar(0.4f);
        spawnBomb(0.8f, 0.05f, 2, 4, 2);
        ObjectPool.Despawn(star4, "star pool");
        yield return WaitTick(4);
        //23
        spawnBomb(-0.1f, 0.05f, 2, 2, 2);
        var star6 = spawnStar(-0.6f);
        yield return WaitTick(4);
        var star7 = spawnStar(0.3f);
        StartFlood();
        yield return WaitTick(2);
        //29
        ObjectPool.Despawn(star5, "star pool");
        ObjectPool.Despawn(star6, "star pool");
        ObjectPool.Despawn(star7, "star pool");
        yield return WaitTick(3);
        //32
        EndFlood();
        yield return WaitTick(2);
        //34
        yield return base.LevelScript();
    }
}
