using UnityEngine;
using System.Collections;

public class Bombmania : Level
{

    protected override IEnumerator LevelScript()
    {
        //with one second per tick, 45 second levels require 45 ticks

        //PlayBackground();
        rafts.Spawn(v2(-0.3f, -1f), true);
        rafts.Spawn(v2(0.35f, -1f), true);
        yield return WaitTick(1);

        //0
        var bomb1 = spawnBomb(-0.5f, 0.05f, 2, 4, 2);
        yield return WaitTick(1);

        //1
        var star1 = spawnStar(-0.9f);
        yield return WaitTick(1);

        //2
        //var bomb2 = spawnBomb(0.5f, 0.05f, 3, 4, 2);
        var newcloud1 = spawnCloud(0f);
        yield return WaitTick(2);

        //3
        var star2 = spawnStar(0.9f);
        yield return WaitTick(3);

        //4
        //var bomb3 = spawnBomb(-0.5f, 0.05f, 3, 4, 2);
        yield return WaitTick(1);

        //5
        ObjectPool.Despawn(star1, "star pool");
        yield return WaitTick(1);

        //6
        var bomb4 = spawnBomb(0.5f, 0.05f, 2, 4, 2);
        yield return WaitTick(1);

        //7
        ObjectPool.Despawn(star2, "star pool");
        var star4 = spawnStar(0.9f);
        var star5 = spawnStar(-0.9f);
        yield return WaitTick(3);

        //10
        ObjectPool.Despawn(star4, "star pool");
        StartFlood();
        yield return WaitTick(2);
        ObjectPool.Despawn(star5, "star pool");
        yield return WaitTick(3);

        //14
        EndFlood();
        var star6 = spawnStar(0.8f);
        var star7 = spawnStar(-0.8f);
        spawnCloud(-0.5f);
        spawnCloud(0.5f);
        yield return WaitTick(2);

        //16
        yield return WaitTick(3);

        //18
        ObjectPool.Despawn(star6, "star pool");
        ObjectPool.Despawn(star7, "star pool");
        yield return WaitTick(2);

        var star8 = spawnStar(0.9f);
        yield return WaitTick(2);

        //22
        var bomb5 = spawnBomb(-0.5f, 0.05f, 2, 2, 1);
        var bomb6 = spawnBomb(0f, 0.05f, 2, 2, 1);
        var star10 = spawnStar(-0.9f);
        yield return WaitTick(5);

        //24
        ObjectPool.Despawn(star8, "star pool");
        var bomb7 = spawnBomb(-0.5f, 0.05f, 2, 2, 1);
        var cloud7 = spawnCloud(0f);
        var starNew1 = spawnStar(0.7f);
        yield return WaitTick(4);

        //28
        ObjectPool.Despawn(star10, "star pool");
        spawnBomb(-0.8f, 0.05f, 2.5f, 2, 1);
        spawnBomb(0.8f, 0.05f, 2.5f, 2, 1);
        yield return WaitTick(3);

        var star12 = spawnStar(-0.7f);
        yield return WaitTick(2);
        spawnBomb(0f, 0.05f, 2, 2, 1);
        yield return WaitTick(4);

        //30
        spawnBomb(-0.8f, 0.05f, 3, 2, 1);
        spawnBomb(0.8f, 0.05f, 3, 2, 1);
        var star15 = spawnStar(0);
        ObjectPool.Despawn(star12, "star pool");
        yield return WaitTick(4);

        //33
        StartFlood();
        yield return WaitTick(5);

        //36
        EndFlood();
        ObjectPool.Despawn(star15, "star pool");
        var star13 = spawnStar(0.9f);
        yield return WaitTick(1);

        //37

        var movingCloud = spawnCloud(0.9f, 1, 1, 2f, 2f);
        var movingCloud2 = spawnCloud(0.5f, 1, 1, 2f, 2f);
        yield return WaitTick(1);
        Rigidbody2D mcRB = movingCloud.AddComponent<Rigidbody2D>();
        mcRB.velocity += new Vector2(-1f, 0);
        mcRB.gravityScale = 0;
        Rigidbody2D mcRB2 = movingCloud2.AddComponent<Rigidbody2D>();
        mcRB2.velocity += new Vector2(-1f, 0);
        mcRB2.gravityScale = 0;

        var star14 = spawnStar(-.1f);
        yield return WaitTick(2);

        //38
        //spawnCloud(-0.4f);
        var star16 = spawnStar(-0.4f);
        yield return WaitTick(2);

        //41
        ObjectPool.Despawn(star13, "star pool");
        ObjectPool.Despawn(star14, "star pool");
        var star17 = spawnStar(-0.7f);
        yield return WaitTick(4);
        
        ObjectPool.Despawn(movingCloud, "cloud pool");
        ObjectPool.Despawn(movingCloud2, "cloud pool");
        StartFlood();
        yield return WaitTick(1);
        ObjectPool.Despawn(star15, "star pool");

        //44
        yield return WaitTick(4);
        EndFlood();
        ObjectPool.Despawn(star16, "star pool");
        ObjectPool.Despawn(star17, "star pool");

        //END
        yield return WaitTick(2);
        yield return base.LevelScript();
    }
}