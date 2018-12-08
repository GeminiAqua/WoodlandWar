using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int currEnergy;
    public int startEnergy = 20;
    public int currLives;
    public int startLives = 10;
    public int currWave = 1;
    public int finalWave = 5;
    public GameObject activeTurretNode;
    public CameraScript mainCam;
    public LifeManager lifeManager;
    public GameObject startPanel; // set manually
    public GameObject winPanel; // set manually
    public GameObject losePanel; // set manually
    
    
    [System.Serializable]
    public class Texts{
        public Text energy;
        public Text wave;
        public Text life;
        public Text displayMessage;
        public float messageFadeTimer = 1f;
    }
    
    public Texts texts = new Texts();
    
	void Start () {
		resetGame();
        lifeManager = GameObject.Find("LifeManager").GetComponent<LifeManager>();
        mainCam = GameObject.Find("MainCamera").GetComponent<CameraScript>();
	}
	
	void Update () {
        checkGameStatus();
        updateEnergy();
        updateWave();
        updateLife();
        resetDisplayMessage();
	}
    
    private void resetGame(){
        currEnergy = startEnergy;
        currLives = startLives;
        currWave = 1;
    }
    
    private void updateEnergy(){
        texts.energy.text = currEnergy.ToString();
    }
    
    private void updateWave(){
        texts.wave.text = "Wave: " + currWave.ToString("0");
    }
    
    private void updateLife(){
        currLives = lifeManager.lives;
        texts.life.text = currLives.ToString("0");
    }
    
    private void resetDisplayMessage(){
        texts.displayMessage.CrossFadeAlpha(0f, texts.messageFadeTimer, false);
    }
    
    public void showDisplayMessage(string reason){
        texts.displayMessage.text = "Insufficient energy to " + reason;
        texts.displayMessage.color = new Color(1, 0, 0, 1);
        texts.displayMessage.CrossFadeAlpha(1f, 0f, true);
        resetDisplayMessage();
    }
    
    public void updateEnergy(int value){
        currEnergy -= value;
    }
    
    public void loseLife(){
        currLives--;
    }
    
    private void checkGameStatus(){
        if (currLives == 0){
            endGame("lose");
        }
    }
    
    public void showStartPanel(){
        startPanel.SetActive(true);
    }
    
    public void endGame(string result){
        if (result.Equals("win")){
            Invoke("showWin", 3f);
        } else if (result.Equals("lose")){
            mainCam.isGameOver = true;
            Invoke("showLose", 6f);
        }
    }
    
    private void showWin(){
        winPanel.SetActive(true);
    }
    
    private void showLose(){
        losePanel.SetActive(true);
    }
}