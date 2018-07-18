using UnityEngine;
using System.Collections;

public class Cloudtopia : Level
{
    
    protected override IEnumerator LevelScript()
    {
        //PlayBackground();
        rafts.Spawn(v2(-0.3f, -1f), true);
        rafts.Spawn(v2(0.35f, -1f), true);
        yield return WaitTick(1);

        //0
        var cloud1 = spawnCloud(0);
        yield return WaitTick(2);

        var starNew1 = spawnStar(0.8f);
        yield return WaitTick(2);

        //4
        spawnCloud(.5f);
        yield return WaitTick(4);

        //6
        ObjectPool.Despawn(starNew1, "star pool");
        spawnCloud(-.5f);
        var star2 = spawnStar(-0.8f);
        yield return WaitTick(2);

        //8
        var star3 = spawnStar(0f);
        yield return WaitTick(2);

        //10
        var starNew2 = spawnStar(0.5f);
        yield return WaitTick(2);

        //12
        ObjectPool.Despawn(star2, "star pool");
        StartFlood();
        ObjectPool.Despawn(star3, "star pool");
        yield return WaitTick(2);

        //14
        ObjectPool.Despawn(starNew2, "star pool");
        yield return WaitTick(3);

        //16
        EndFlood();
        yield return WaitTick(1);
        spawnBomb(.35f, 0.05f, 2);
        spawnBomb(-.3f, 0.05f, 2);
        yield return WaitTick(1);

        //18
        var star4 = spawnStar(-0.8f);
        yield return WaitTick(4);

        //19
        spawnBomb(-.7f, 0.05f, 3);
        yield return WaitTick(2);

        //24
        ObjectPool.Despawn(star4, "star pool");
        spawnCloud(.4f);
        var starextra = spawnStar(0.8f);
        yield return WaitTick(4);

        //26
        var star6 = spawnStar(0f);
        spawnCloud(-0.4f);
        yield return WaitTick(2);

        //28
        ObjectPool.Despawn(starextra, "star pool");
        var star7 = spawnStar(-0.7f);
        yield return WaitTick(3);

        //30
        var star8 = spawnStar(0.7f);
        ObjectPool.Despawn(star6, "star pool");
        yield return WaitTick(3);

        //33
        ObjectPool.Despawn(star7, "star pool");
        ObjectPool.Despawn(star8, "star pool");
        yield return WaitTick(2);

        //36
        var star9 = spawnStar(0.5f);
        StartFlood();
        yield return WaitTick(5);

        //40
        ObjectPool.Despawn(star9, "star pool");
        EndFlood();
        yield return WaitTick(2);

        //42
        spawnBomb(.5f, 0.05f, 2, 4);
        spawnBomb(-.5f, 0.05f, 2, 4);
        yield return WaitTick(1);
        var star11 = spawnStar(0.9f);
        var star12 = spawnStar(-0.9f);
        yield return WaitTick(5);

        //46
        var star13 = spawnStar(-.1f);
        yield return WaitTick(2);
        spawnBomb(0f, 0.05f, 5f, 4);
        yield return WaitTick(5);

        //48
        ObjectPool.Despawn(star11, "star pool");
        ObjectPool.Despawn(star12, "star pool");
        ObjectPool.Despawn(star13, "star pool");

        yield return WaitTick(2);
        yield return base.LevelScript();
    }
}