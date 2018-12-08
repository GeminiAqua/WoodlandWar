using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour {
    
    [System.Serializable]
    public class Enemies{
        public GameObject one;
        public GameObject two;
        public GameObject three;
    }
    
    public Enemies enemies = new Enemies();
    public float spawnCooldown = 1.5f;
    public float spawnDelay = 1f;
    public float levelDuration = 30f;
    private GameManager gameManager;
    
    private bool waitForMobs;
    private GameObject[] allTargets; // public for debug
    
	void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemies.one = Resources.Load("Enemy1") as GameObject;
        enemies.two = Resources.Load("Enemy2") as GameObject;
        enemies.three = Resources.Load("Enemy3") as GameObject;
	}
    
    void Update(){
        if (waitForMobs){
            allTargets = GameObject.FindGameObjectsWithTag("Enemy");
            if (allTargets.Length == 0){
                waitForMobs = false;
                gameManager.endGame("win");
            }
        }
    }
    
    public void spawnCondition(){
        if (gameManager.currWave == 1){
            levelOne();
        } else if (gameManager.currWave == 2){
            levelTwo();
        } else if (gameManager.currWave == 3){
            levelThree();
        } else if (gameManager.currWave == 4){
            levelFour();
        } else if (gameManager.currWave == 5){
            levelFive();
        }
        
        Invoke("endWave", levelDuration * gameManager.currWave);
    }
    
    private void levelOne(){
        InvokeRepeating("spawnOne", spawnDelay, spawnCooldown);
        InvokeRepeating("spawnTwo", spawnDelay * 15, spawnCooldown * 20);
        InvokeRepeating("spawnThree", spawnDelay * 5, spawnCooldown * 5); 
    }
    
    private void levelTwo(){
        InvokeRepeating("spawnOne", spawnDelay, spawnCooldown - 0.2f);
        InvokeRepeating("spawnTwo", spawnDelay * 20, (spawnCooldown - 0.2f )* 20);
        InvokeRepeating("spawnThree", spawnDelay * 5, (spawnCooldown - 0.2f ) * 5); 
    }
    
    private void levelThree(){
        InvokeRepeating("spawnOne", spawnDelay, spawnCooldown - 0.4f);
        InvokeRepeating("spawnTwo", spawnDelay * 15, (spawnCooldown - 0.4f )* 20);
        InvokeRepeating("spawnThree", spawnDelay * 4, (spawnCooldown - 0.4f ) * 5); 
    }
    
    private void levelFour(){
        InvokeRepeating("spawnOne", spawnDelay, spawnCooldown - 0.6f);
        InvokeRepeating("spawnTwo", spawnDelay * 10, (spawnCooldown - 0.6f )* 20);
        InvokeRepeating("spawnThree", spawnDelay * 3, (spawnCooldown - 0.6f ) * 5); 
    }
    
    private void levelFive(){
        InvokeRepeating("spawnOne", spawnDelay, spawnCooldown - 0.8f);
        InvokeRepeating("spawnTwo", spawnDelay * 5, (spawnCooldown - 0.8f )* 20);
        InvokeRepeating("spawnThree", spawnDelay * 2, (spawnCooldown - 0.8f ) * 5); 
    }
    
    private void spawnOne(){
        GameObject temp = Instantiate(enemies.one) as GameObject;
        temp.transform.position = gameObject.transform.position;
    }
    
    private void spawnTwo(){
        GameObject temp = Instantiate(enemies.two) as GameObject;
        temp.transform.position = gameObject.transform.position;
    }
    
    private void spawnThree(){
        GameObject temp = Instantiate(enemies.three) as GameObject;
        temp.transform.position = gameObject.transform.position;
    }
    
    private void endWave(){
        CancelInvoke();
        if (gameManager.currWave < 5){
            Invoke("showReady", 3f);
        } else {
            waitForMobs = true;
        }
    }
    
    private void showReady(){
        gameManager.showStartPanel();
        gameManager.currWave++;
    }
}
