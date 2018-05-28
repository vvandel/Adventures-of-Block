﻿using UnityEngine;
using System.Collections;

public class Bombmania : Level
{

    protected override IEnumerator LevelScript()
    {
        //with one second per tick, 45 second levels require 45 ticks

        //PlayBackground();
        yield return WaitTick(1);

        //0
        var bomb1 = spawnBomb(-0.5f, 0.05f, 3, 4, 2);
        yield return WaitTick(1);

        //1
        var star1 = spawnStar(-0.9f);
        yield return WaitTick(1);

        //2
        var bomb2 = spawnBomb(0.5f, 0.05f, 3, 4, 2);
        yield return WaitTick(1);

        //3
        var star2 = spawnStar(0.9f);
        ObjectPool.Despawn(star1, "star pool");
        yield return WaitTick(1);

        //4
        var bomb3 = spawnBomb(-0.5f, 0.05f, 3, 4, 2);
        yield return WaitTick(1);

        //5
        ObjectPool.Despawn(star2, "star pool");
        var star3 = spawnStar(-0.7f);
        yield return WaitTick(1);

        //6
        var bomb4 = spawnBomb(0.5f, 0.05f, 3, 4, 2);
        yield return WaitTick(1);

        //7
        var star4 = spawnStar(0.9f);
        var star5 = spawnStar(-0.9f);
        ObjectPool.Despawn(star3, "star pool");
        yield return WaitTick(1);

        //8 
        ObjectPool.Despawn(star4, "star pool");
        yield return WaitTick(2);

        //10
        ObjectPool.Despawn(star5, "star pool");
        StartFlood();
        yield return WaitTick(3);

        //13
        var star6 = spawnStar(0.9f);
        var star7 = spawnStar(-0.9f);
        yield return WaitTick(2);

        //14
        EndFlood();
        spawnCloud(-0.52f);
        yield return WaitTick(2);

        //16
        spawnCloud(0.52f);
        yield return WaitTick(1);

        //17
        yield return WaitTick(1);

        //18
        ObjectPool.Despawn(star7, "star pool");
        yield return WaitTick(1);

        //19
        yield return WaitTick(3);

        //22
        var bomb5 = spawnBomb(-0.5f, 0.05f, 2, 2, 1);
        var bomb6 = spawnBomb(0.5f, 0.05f, 2, 2, 1);
        var star8 = spawnStar(0.9f);
        var star9 = spawnStar(0f);
        var star10 = spawnStar(-0.9f);
        yield return WaitTick(1);

        //23
        ObjectPool.Despawn(star6, "star pool");
        yield return WaitTick(1);

        //24
        var bomb7 = spawnBomb(-0.5f, 0.05f, 2, 2, 1);
        var bomb8 = spawnBomb(0.5f, 0.05f, 2, 2, 1);
        var cloud7 = spawnCloud(0f);
        yield return WaitTick(4);

        //28
        spawnBomb(-0.8f, 0.05f, 2, 2, 1);
        spawnBomb(0f, 0.05f, 2, 2, 1);
        spawnBomb(0.8f, 0.05f, 2, 2, 1);
        var star11 = spawnStar(0.5f);
        var star12 = spawnStar(-0.5f);
        yield return WaitTick(2);

        //30
        spawnBomb(-0.8f, 0.05f, 2, 2, 1);
        spawnBomb(0f, 0.05f, 2, 2, 1);
        spawnBomb(0.8f, 0.05f, 2, 2, 1);
        yield return WaitTick(2);

        //32
        yield return WaitTick(1);

        //33
        StartFlood();
        yield return WaitTick(3);

        //34
        ObjectPool.Despawn(star8, "star pool");
        ObjectPool.Despawn(star9, "star pool");
        ObjectPool.Despawn(star10, "star pool");
        ObjectPool.Despawn(star11, "star pool");
        ObjectPool.Despawn(star12, "star pool");
        yield return WaitTick(2);

        //36
        EndFlood();
        var star13 = spawnStar(0.9f);
        var cloud5 = spawnCloud(0.8f);
        yield return WaitTick(1);

        //37
        var cloud3 = spawnCloud(0);
        var cloud6 = spawnCloud(0.4f);
        var star14 = spawnStar(0f);
        yield return WaitTick(1);

        //38
        spawnCloud(-0.4f);
        var star15 = spawnStar(-0.4f);
        yield return WaitTick(3);

        //41
        var cloud4 = spawnCloud(-0.8f);
        var star16 = spawnStar(-0.8f);
        yield return WaitTick(2);

        //43
        ObjectPool.Despawn(cloud3, "cloud pool");
        StartFlood();
        yield return WaitTick(1);

        //44
        ObjectPool.Despawn(cloud6, "cloud pool");
        yield return WaitTick(1);
        
        //45
        ObjectPool.Despawn(cloud4, "cloud pool");

        yield return WaitTick(3);
        EndFlood();

        //END
        yield return WaitTick(2);
        yield return base.LevelScript();
    }
}