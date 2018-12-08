using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Vector3 startPosition = new Vector3(-0.5f, 57f, -5f);
    public Vector3 endPosition = new Vector3(5f, 55f, 2f);
    public bool isGameOver;
    
    void Start(){
        transform.position = startPosition;
    }
    
    void Update(){
        if (isGameOver){
            moveCamera();
        }
    }
    
	public void moveCamera(){
        transform.position = Vector3.MoveTowards(transform.position, endPosition, 0.08f);
    }
}
