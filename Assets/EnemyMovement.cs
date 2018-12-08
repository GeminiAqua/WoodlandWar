using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    
    public bool isAir;
	public float speed = 10f; 
    public Transform target;
    private int wpIndex = 0;
    private LifeManager lifeManager;
    
    

void Start(){
    lifeManager = GameObject.Find("LifeManager").GetComponent<LifeManager>();
    if (!isAir){
        target = WaypointScript.points[0];
    } else {
        target = WaypointScriptDup.points[0];
    }
}

void Update(){
    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    transform.LookAt(target);
    
    if (Vector3.Distance(transform.position, target.position) <= 0.1f){
        GetNextWaypoint();
    }
}

void GetNextWaypoint(){
    if (!isAir){
        if (wpIndex >= (WaypointScript.points.Length - 1)){
            lifeManager.loseLife();
            Destroy(gameObject);
            return;
        }
        
        wpIndex++;
        target = WaypointScript.points[wpIndex];
    } else {
        if (wpIndex >= (WaypointScriptDup.points.Length - 1)){
            lifeManager.loseLife();
            Destroy(gameObject);
            return;
        }
        
        wpIndex++;
        target = WaypointScriptDup.points[wpIndex];
    }
}

}
