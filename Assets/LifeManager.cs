using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

    public List<GameObject> lifeSymbols;
    public int lives;
    private GameObject deathDragon;
    
	void Start() {
		int count = transform.childCount;
        lifeSymbols = new List<GameObject>();
        for (int i = 0; i < count; i++){
            lifeSymbols.Add(transform.GetChild(i).gameObject);
        }
        deathDragon = Resources.Load("FlowerDeathDragon") as GameObject;
	}
    
    void Update(){
        lives = lifeSymbols.Count;
    }
    
    public void loseLife(){
        if (lifeSymbols.Count > 0){
            int randomIndex = Random.Range(0, lifeSymbols.Count);
            instantiateDeath(lifeSymbols[randomIndex]);
            Destroy(lifeSymbols[randomIndex], 4);
            lifeSymbols.RemoveAt(randomIndex);
        }
    }
    
    private void instantiateDeath(GameObject flower){
        GameObject summon = Instantiate(deathDragon) as GameObject;
        summon.transform.position = flower.transform.position + new Vector3(-0.1f, 0.2f, 0);
    }
}
