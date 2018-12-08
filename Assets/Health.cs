using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats{
        public float startingHealth = 100;
        public float currentHealth;
        public float minHealth = 0;
        public int energyDrop = 5;
        public bool isInvincible;
    }
    
    public EnemyStats enemyStats = new EnemyStats();
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyStats.currentHealth = enemyStats.startingHealth * gameManager.currWave;
    }

    void Update()
    {
        checkDead();
    }
    
    public void takeDamage(float amount)
    {
        if (!enemyStats.isInvincible){
            enemyStats.currentHealth -= amount;
        }
    }
    
    private void checkDead(){
        if (enemyStats.currentHealth <= enemyStats.minHealth){
            gameManager.updateEnergy(- (enemyStats.energyDrop + (2 * gameManager.currWave)));
            Destroy(gameObject);
            // isDead = true;
        }
    }

}
