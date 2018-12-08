using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUIManager : MonoBehaviour {

    [System.Serializable]
    public class TurretPrefabs{
        public GameObject one;
        public GameObject two;
        public GameObject three;
    }
    
    [System.Serializable]
    public class Outline{
        public Image one;
        public Image two;
        public Image three;
    }
    
    public TurretPrefabs turretPrefabs = new TurretPrefabs();
    public Outline outline = new Outline();
    public GameObject selectedTurret = null;
    public Image activeImage;

	void Start () {
        activeImage = null;
		// turretPrefabs.one = Resources.Load("WhaleTurret") as GameObject;
        // turretPrefabs.two = Resources.Load("BunnyTurret") as GameObject;
        // turretPrefabs.three = Resources.Load("UnicornTurret") as GameObject;
	}
	
	void Update () {
		if (activeImage != null){
            updateActive();
        }
	}
    
    public void setActive(Image im){
        activeImage = im;
    }
    
    void updateActive(){
        if (activeImage == outline.one){
            showImage(outline.one);
            hideImage(outline.two);
            hideImage(outline.three);
            selectedTurret = turretPrefabs.one;
        } else if (activeImage == outline.two){
            hideImage(outline.one);
            showImage(outline.two);
            hideImage(outline.three);
            selectedTurret = turretPrefabs.two;
        } else if (activeImage == outline.three){
            hideImage(outline.one);
            hideImage(outline.two);
            showImage(outline.three);
            selectedTurret = turretPrefabs.three;
        }
    }
    
    private void showImage(Image im){
        im.color = new Color(1, 1, 1, 1);
    }
    
    private void hideImage(Image im){
        im.color = new Color(1, 1, 1, 0);
    }
}
