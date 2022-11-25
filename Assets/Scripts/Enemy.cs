using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard;
    Transform parent;

    void Start() {
        parent = GameObject.FindWithTag("SpawnAtRuntime").transform;
        scoreBoard = FindObjectOfType<ScoreBoard>();
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProccedHit();
        print($"dbg: enemy was hit {hitPoints}");
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void ProccedHit()
    {
        hitPoints--;
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
    }

    void KillEnemy() 
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
