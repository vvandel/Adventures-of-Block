using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level {
    int[] orientations = new int[] { 1, 1, 1, 1, 1 };

    protected override IEnumerator LevelScript()
    {
        PlayBackground();
        int orientation = orientations[Log.Attempt-1];
        rafts.Spawn(v2(-0.3f, -1f), true);
        spawnBomb(0, 0.05f, 3, 8, 1);
        yield return WaitTick(4);
        //4
        var cloudLeft = spawnCloud(-0.5f, 1, 1.4f, 2f, 2f);
        var cloudRight = spawnCloud(0.5f, 1, 1.4f, 2f, 2f);
        var starMiddle = stars.Spawn(v2(0, 1), true);
        yield return WaitTick(2);
        ObjectPool.Despawn(starMiddle, "star pool");
        yield return WaitTick(3);
        var starMiddle2 = stars.Spawn(v2(0, 1), true);
        yield return WaitTick(2);
        spawnBomb(-0.6f * orientation, 0.05f, 1.8f, 2, 1);
        spawnBomb(-0.9f * orientation, 0.05f, 1.8f, 2, 1);
        spawnBomb(0.9f * orientation, 0.05f, 3.5f, 2, 1);
        yield return WaitTick(2);
        var starLeft = stars.Spawn(v2(-0.5f * orientation, 1f), true);
        spawnBomb(-0.5f * orientation, 0.05f, 3, 2, 1);
        yield return WaitTick(2);
        spawnBomb(0f, 0.05f, 3, 2, 1);
        yield return WaitTick(2);
        ObjectPool.Despawn(starMiddle2, "star pool");
        spawnBomb(0.55f, 0.05f, 3, 2, 1);
        spawnBomb(-0.55f, 0.05f, 3, 2, 1);
        yield return WaitTick(2);
        ObjectPool.Despawn(starLeft, "star pool");
        yield return WaitTick(2);
        cloudLeft = spawnCloud(-0.8f, 1, 1.4f, 2, 2);
        cloudRight = spawnCloud(0.8f, 1, 1.4f, 2f, 2f);
        starLeft = stars.Spawn(v2(-0.8f, 1f), true);
        var starRight = stars.Spawn(v2(0.8f, 1f), true);
        var movingCloud = spawnCloud(orientation * 0.7f, 1, 1, 2f, 2f);
        Rigidbody2D mcRB = movingCloud.AddComponent<Rigidbody2D>();
        mcRB.velocity += new Vector2(-orientation * 2f, 0);
        mcRB.gravityScale = 0;
        yield return WaitTick(4);
        mcRB.velocity -= new Vector2(-orientation * 4f, 0);

        yield return WaitTick(2);
        ObjectPool.Despawn(starLeft, "star pool");
        ObjectPool.Despawn(starRight, "star pool");
        spawnBomb(-0.7f, 0.05f, 1.5f, 4, 1);
        spawnBomb(0f, 0.05f, 1.7f, 4, 1);
        spawnBomb(0.7f, 0.05f, 1.5f, 4, 1);
        yield return WaitTick(4);
        var randomStar = stars.Spawn(v2(orientation * 0.3f, 1f), true);
        StartFlood();
        yield return WaitTick(4);
        spawnCloud(-0.3f, 1, 1, 2f, 2f);
        spawnCloud(0.3f, 1, 1, 2f, 2f);
        EndFlood();
        ObjectPool.Despawn(randomStar, "star pool");
        yield return WaitTick(3);
        spawnBomb(orientation * 0.7f, 0.05f, 1f, 1, 0);
        spawnBomb(0f, 0.05f, 2, 2, 1);
        yield return WaitTick(1);

        movingCloud = spawnCloud(orientation * 0.7f, 1, 1, 2f, 2f, true);
        mcRB = movingCloud.AddComponent<Rigidbody2D>();
        mcRB.velocity += new Vector2(-orientation * 2f, 0);
        mcRB.gravityScale = 0;
        var movingCloud2 = spawnCloud(-orientation * 0.7f, 1, 1, 2f, 2f, true);
        Rigidbody2D mcRB2 = movingCloud2.AddComponent<Rigidbody2D>();
        mcRB2.velocity += new Vector2(orientation * 2f, 0);
        mcRB2.gravityScale = 0;
        yield return WaitTick(4);
        
        ObjectPool.Despawn(movingCloud, "cloud pool");
        ObjectPool.Despawn(movingCloud2, "cloud pool");
        StartFlood();
        yield return WaitTick(2);
        stars.Spawn(v2(orientation * 0.3f, 1f), true);
        yield return WaitTick(4);
        yield return base.LevelScript();
    }
}
