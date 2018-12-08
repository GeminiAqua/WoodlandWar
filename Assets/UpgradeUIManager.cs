using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIManager : MonoBehaviour {

    [System.Serializable]
    public class Buttons{
        public Button summon;   // set manually
        public Button grow;     // set manually
        public Button dismiss;  // set manually
    }
    
    [System.Serializable]
    public class UpgradeTexts{
        public Text summon;   // set manually
        public Text grow;     // set manually
        public Text dismiss;  // set manually
    }
    
    [System.Serializable]
    public class LocalCosts{
        public int sumCost = 0;
        public int growTwo = 0;
        public int growThree = 0;
        public int disCost = 0;
    }
    
    
    public Buttons buttons = new Buttons();
    public UpgradeTexts upgradeTexts = new UpgradeTexts();
    public LocalCosts localCosts = new LocalCosts();
    public GameManager gameManager;
    public TurretUIManager turretManager;    // set manually
	
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void Update () {
		setInteractables();
        updateCosts();
	}
    
    public void summon(){
        if ( (gameManager.currEnergy - localCosts.sumCost) >= 0 ){
            gameManager.activeTurretNode.GetComponent<TurretNode>().summonTurret(turretManager.selectedTurret);
            gameManager.updateEnergy(localCosts.sumCost);
        } else {
            gameManager.showDisplayMessage("summon");
        }
    }
    
    public void grow(){
        if ( gameManager.activeTurretNode.GetComponent<TurretNode>().turret.GetComponent<TurretController>().turretStatus.level == 1){
            if ( (gameManager.currEnergy - localCosts.growTwo) >= 0 ){
                gameManager.activeTurretNode.GetComponent<TurretNode>().growTurret();
                gameManager.updateEnergy(localCosts.growTwo);
            } else {
                gameManager.showDisplayMessage("grow to level 2");
            }
        } else if ( gameManager.activeTurretNode.GetComponent<TurretNode>().turret.GetComponent<TurretController>().turretStatus.level == 2) {
            if ( (gameManager.currEnergy - localCosts.growThree) >= 0 ){
                gameManager.activeTurretNode.GetComponent<TurretNode>().growTurret();
                gameManager.updateEnergy(localCosts.growThree);
            } else {
                gameManager.showDisplayMessage("grow to level 3");
            }
        }
    }
    
    public void dismiss(){
        gameManager.activeTurretNode.GetComponent<TurretNode>().dismissTurret();
        gameManager.updateEnergy(localCosts.disCost);
    }
    
    private void setInteractables(){
        if ((gameManager.activeTurretNode == null) || (turretManager.selectedTurret == null)){
            setInactive(buttons.summon);
            setInactive(buttons.grow);
            setInactive(buttons.dismiss);
        } else {
            if (gameManager.activeTurretNode.GetComponent<TurretNode>().turret == null){
                setActive(buttons.summon);
                setInactive(buttons.grow);
                setInactive(buttons.dismiss);
            } else {
                setInactive(buttons.summon);
                
                int currLevel = gameManager.activeTurretNode.GetComponent<TurretNode>().turret.GetComponent<TurretController>().turretStatus.level;
                if (currLevel < 3){
                    setActive(buttons.grow);
                } else {
                    setInactive(buttons.grow);
                }
                
                setActive(buttons.dismiss);
            }
            
            
        }
    }
	
    private void updateCosts(){
        if ((gameManager.activeTurretNode == null) || (turretManager.selectedTurret == null)){
            upgradeTexts.summon.text = "Summon";
            upgradeTexts.grow.text = "Grow";
            upgradeTexts.dismiss.text = "Dismiss";
        } else {
            if (turretManager.selectedTurret != null){
                localCosts.sumCost = turretManager.selectedTurret.GetComponent<TurretController>().turretCost.summonCost;
                upgradeTexts.summon.text = "Summon: " + localCosts.sumCost.ToString();
            }
            if (gameManager.activeTurretNode.GetComponent<TurretNode>().turret != null){
                
                localCosts.growTwo = gameManager.activeTurretNode.GetComponent<TurretNode>().turret.GetComponent<TurretController>().turretCost.growTwoCost;
                localCosts.growThree = gameManager.activeTurretNode.GetComponent<TurretNode>().turret.GetComponent<TurretController>().turretCost.growThreeCost;
                localCosts.disCost = gameManager.activeTurretNode.GetComponent<TurretNode>().turret.GetComponent<TurretController>().turretCost.dismissCost;
                
                int currLevel = gameManager.activeTurretNode.GetComponent<TurretNode>().turret.GetComponent<TurretController>().turretStatus.level;
                if (currLevel == 1){
                    upgradeTexts.grow.text = "Grow: " + localCosts.growTwo.ToString();
                } else if (currLevel == 2){
                    upgradeTexts.grow.text = "Grow: " + localCosts.growThree.ToString();
                    upgradeTexts.dismiss.text = "Dismiss: ";
                } else if (currLevel == 3){
                    upgradeTexts.grow.text = "Grow";
                    upgradeTexts.dismiss.text = "Dismiss: ";
                }
                
                upgradeTexts.dismiss.text = "Dismiss: " + localCosts.disCost.ToString();
            }
        }
    }
    
    private void setActive(Button but){
        but.interactable = true;
    }
    
    private void setInactive(Button but){
        but.interactable = false;
    }
}
