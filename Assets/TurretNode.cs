using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretNode : MonoBehaviour {

    public bool isActive;
    public bool isHover;
    public GameManager gameManager;
    
    [System.Serializable]
    public class Mats{
        public Material defaultMat; // set manually
        public Material hoverMat;   // set manually
        public Material activeMat;  // set manually
        public Material[] grass;
        public Material[] hover;
        public Material[] active;
    }
    
    public Mats mats = new Mats();
    public Vector3 positionOffset;

    public GameObject turret;  // reference this for turret commands
    private Renderer rend;

    void Start(){
        turret = null;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rend = GetComponent<MeshRenderer>();
        mats.grass = rend.materials;
        mats.hover = new Material[]{mats.hoverMat};
        mats.active = new Material[]{mats.activeMat};
    }
    
    void Update(){
        checkActive();
        updateColor();
    }
    
    public void summonTurret(GameObject newTurret){
        turret = Instantiate(newTurret) as GameObject;
        turret.transform.position = gameObject.transform.position + new Vector3(0, 0.05f, 0);
    }
    
    public void growTurret(){
        turret.GetComponent<TurretController>().levelUp();
    }
    
    public void dismissTurret(){
        Destroy(turret);
        turret = null;
    }
    
    public void checkActive(){
        if (gameObject == gameManager.activeTurretNode){
            isActive = true;
        } else {
            isActive = false;
        }
    }
    
    private void updateColor(){
        if (isActive){
            rend.materials = mats.active;
        } else {
            if (isHover){
                rend.materials = mats.hover;
            } else {
                rend.materials = mats.grass;
            }
        }
    }

    void OnMouseDown(){
        if (gameManager.activeTurretNode == gameObject){
            gameManager.activeTurretNode = null;
        } else {
            gameManager.activeTurretNode = gameObject;
        }
    }

    void OnMouseEnter(){
        isHover = true;
    }

    void OnMouseExit(){
        isHover = false;
    }
    
    
    
}
