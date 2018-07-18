using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level {
    int[] orientations = new int[] { 1, 1, 1, 1, 1 };

    protected override IEnumerator LevelScript()
    {
        //PlayBackground();
        int orientation = orientations[Log.Attempt-1];
        rafts.Spawn(v2(-0.3f, -1f), true);
        rafts.Spawn(v2(0.35f, -1f), true);
        yield return WaitTick(1);

        spawnBomb(0f, 0.05f, 3, 4, 1);
        var starNew1 = spawnStar(0.7f);
        yield return WaitTick(3);

        ObjectPool.Despawn(starNew1, "star pool");
        var cloudLeft = spawnCloud(-0.5f, 1, 1.4f, 2f, 2f);
        var cloudRight = spawnCloud(0.5f, 1, 1.4f, 2f, 2f);
        var starMiddle = spawnStar(0);
        yield return WaitTick(4);

        var starMiddle2 = spawnStar(-0.8f);
        yield return WaitTick(5);

        ObjectPool.Despawn(starMiddle, "star pool");
        spawnBomb(-0.4f * orientation, 0.05f, 2f, 2, 1);
        spawnBomb(-0.9f * orientation, 0.05f, 2f, 2, 1);
        spawnBomb(0.9f * orientation, 0.05f, 3.5f, 2, 1);
        yield return WaitTick(4);

        var starLeft = spawnStar(-0.75f * orientation);
        ObjectPool.Despawn(starMiddle2, "star pool");
        yield return WaitTick(2);

        spawnBomb(0f, 0.05f, 3, 2, 1);
        spawnBomb(-0.55f, 0.05f, 3, 2, 1);
        yield return WaitTick(5);

        ObjectPool.Despawn(starLeft, "star pool");
        spawnBomb(0.55f, 0.05f, 3, 2, 1);
        yield return WaitTick(2);

        cloudLeft = spawnCloud(-0.8f, 1, 1.4f, 2, 2);
        cloudRight = spawnCloud(0.8f, 1, 1.4f, 2f, 2f);
        var starLeft2 = spawnStar(-0.8f);
        var starRight = spawnStar(0.8f);
        var movingCloud = spawnCloud(orientation * 0.7f, 1, 1, 2f, 2f);
        Rigidbody2D mcRB = movingCloud.AddComponent<Rigidbody2D>();
        mcRB.velocity += new Vector2(-orientation * 2f, 0);
        mcRB.gravityScale = 0;
        yield return WaitTick(4);

        mcRB.velocity -= new Vector2(-orientation * 4f, 0);
        yield return WaitTick(2);

        ObjectPool.Despawn(starLeft2, "star pool");
        ObjectPool.Despawn(starRight, "star pool");
        spawnBomb(-0.7f, 0.05f, 1.5f, 4, 1);
        spawnBomb(0f, 0.05f, 1.7f, 4, 1);
        spawnBomb(0.7f, 0.05f, 1.5f, 4, 1);
        yield return WaitTick(4);

        var randomStar = spawnStar(0.1f);
        StartFlood();
        yield return WaitTick(5);
        ObjectPool.Despawn(randomStar, "star pool");
        EndFlood();
        yield return WaitTick(1);

        spawnCloud(-0.4f, 1, 1, 2f, 2f);
        spawnCloud(0.6f, 1, 1, 2f, 2f);
        yield return WaitTick(4);

        spawnBomb(0.1f, 0.05f, 2f, 2, 1);
        var randomStar2 = spawnStar(-0.7f);
        yield return WaitTick(1);

        yield return WaitTick(4);

        ObjectPool.Despawn(randomStar2, "star pool");
        var starNew2 = spawnStar(-0.5f);
        var starNew3 = spawnStar(0.5f);
        yield return WaitTick(1);
        StartFlood();
        yield return WaitTick(4);

        EndFlood();
        ObjectPool.Despawn(starNew2, "star pool");
        ObjectPool.Despawn(starNew3, "star pool");
        yield return WaitTick(2);

        yield return base.LevelScript();
    }
}
