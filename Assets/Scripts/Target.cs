using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float health = 50.0f;
    public GameObject explosion;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Player.killStreak++;
        EnemyAIManager.activeEnemies--;
        EnemyAIManager.amountOfEnemies--;
        Vector3 target = gameObject.transform.position;
        GameObject impactGO = Instantiate(explosion, target, Quaternion.LookRotation(target));
        Destroy(impactGO, 2f);
        Destroy(gameObject);
    }
}
