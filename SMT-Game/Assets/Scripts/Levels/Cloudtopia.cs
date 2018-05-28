﻿using UnityEngine;
using System.Collections;

public class Cloudtopia : Level
{
    
    protected override IEnumerator LevelScript()
    {
        //PlayBackground();
        yield return WaitTick(1);

        //0
        var cloud1 = spawnCloud(0);
        yield return WaitTick(4);

        //4
        spawnCloud(.5f);
        var star1 = spawnStar(0.9f);
        yield return WaitTick(2);

        //6
        ObjectPool.Despawn(cloud1, "cloud pool");
        spawnCloud(-.5f);
        var star2 = spawnStar(-0.8f);
        yield return WaitTick(2);

        //8
        yield return WaitTick(2);

        //10
        var star3 = spawnStar(0f);
        yield return WaitTick(2);

        //12
        StartFlood();
        yield return WaitTick(2);

        //14
        ObjectPool.Despawn(star2, "star pool");
        yield return WaitTick(3);

        //16
        EndFlood();
        spawnBomb(.4f);
        yield return WaitTick(1);

        //18
        spawnBomb(.0f);
        yield return WaitTick(1);

        //19
        spawnBomb(-.9f, 0.05f, 3);
        var star4 = spawnStar(0.9f);
        yield return WaitTick(5);

        //24
        spawnCloud(.4f);
        yield return WaitTick(2);

        //24
        ObjectPool.Despawn(star4, "star pool");
        var star5 = spawnStar(-0.9f, 1f);
        yield return WaitTick(2);

        //26
        ObjectPool.Despawn(star5, "star pool");
        var star6 = spawnStar(0f);
        spawnCloud(-0.4f);
        yield return WaitTick(2);

        //28
        var star7 = spawnStar(-0.9f);
        yield return WaitTick(2);

        //30
        var star8 = spawnStar(0.9f);
        ObjectPool.Despawn(star6, "star pool");
        yield return WaitTick(2);

        //32
        var star10 = spawnStar(-0.7f);
        yield return WaitTick(1);

        //33
        ObjectPool.Despawn(star8, "star pool");
        yield return WaitTick(3);

        //36
        ObjectPool.Despawn(star7, "star pool");
        var star9 = spawnStar(0.6f);
        StartFlood();
        yield return WaitTick(5);

        //40
        ObjectPool.Despawn(star9, "star pool");
        EndFlood();
        yield return WaitTick(2);

        //42
        spawnBomb(.5f, 0.05f, 4, 4);
        var star11 = spawnStar(-0.9f);
        yield return WaitTick(2);
        
        //44
        spawnBomb(-.5f, 0.05f, 4, 4);
        var star12 = spawnStar(0.9f);
        yield return WaitTick(2);

        //46
        spawnBomb(0f, 0.05f, 5.5f, 4);
        yield return WaitTick(2);

        //48
        var star13 = spawnStar(0.4f);

        yield return WaitTick(4);
        yield return base.LevelScript();
    }
}