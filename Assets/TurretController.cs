using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public bool hasFace;
    
    [System.Serializable]
    public class TurretCurrentStats{
        public float attack;
        public float attackCD;
        public float attackRange;
    }
    
    [System.Serializable]
    public class TurretBaseStats{
        public float baseAttack = 15f;
        public float baseAttackCD = 1f;
        public float baseAttackRange = 5f;
        public float upgradeAttackBonus = 10f;
        public float upgradeAttackCDBonus = 0.2f;
        public float upgradeAttackRangeBonus = 0.5f;
        public Vector3 level1Scale = new Vector3(0.5f, 0.5f, 0.5f);
        public Vector3 level2Scale = new Vector3(0.675f, 0.675f, 0.675f);
        public Vector3 level3Scale = new Vector3(0.75f, 0.75f, 0.75f);
    }
    
    [System.Serializable]
    public class TurretStatus{
        public bool isOnCooldown;
        public int level = 1;
    }
    
    [System.Serializable]
    public class Missiles{
        public GameObject one;
        public GameObject two;
        public GameObject three;
    }
    
    [System.Serializable]
    public class Face{
        public SkinnedMeshRenderer renderer;
        public Material idleFace;
        public Material attackFace;
        public Material whaleMat;
        public Material[] idle;
        public Material[] attack;
    }
    
    [System.Serializable]
    public class TurretCost{
        public int summonCost = 5;
        public int growTwoCost = 5;
        public int growThreeCost = 10;
        public int dismissCost = -3;
    }
    
    public AudioSource fireSound;
    
    public TurretCurrentStats turretCurrentStats = new TurretCurrentStats();
    public TurretBaseStats turretBaseStats = new TurretBaseStats();
    public TurretStatus turretStatus = new TurretStatus();
    public Missiles missiles = new Missiles();
    public TurretCost turretCost = new TurretCost();
    public Face face = new Face();
    
    public GameObject maxLvlParticles;
    private Animator anim;
    
    public GameObject[] allTargets; // public for debug
    public Transform target; // public for debug
    
	void Start () {
        anim = GetComponent<Animator>();
        initializeStats();
        target = null;
        
        // whale only
        if (hasFace){
            face.idle = new Material[]{face.idleFace, face.whaleMat};
            face.attack = new Material[]{face.attackFace, face.whaleMat};
        }
	}
    
    void Update(){
        updateSize();
        updateFace();
    }
	
	void FixedUpdate () {
        if ( target == null ){
            findClosestTarget();
        } else if (Vector3.Distance(transform.position, target.position) > turretCurrentStats.attackRange){
            target = null;
            findClosestTarget();
        } else if (Vector3.Distance(transform.position, target.position) <= turretCurrentStats.attackRange){
            transform.LookAt(target);
            Attack();
        }
	}
    
    void initializeStats(){
        turretCurrentStats.attack = turretBaseStats.baseAttack;
        turretCurrentStats.attackCD = turretBaseStats.baseAttackCD;
        turretCurrentStats.attackRange = turretBaseStats.baseAttackRange;
    }
    
    void findClosestTarget(){
        int closestTargetIndex = 0;
        int i = 0;
        float minDist = 100f;
        
        allTargets = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allTargets){
            if (Vector3.Distance(transform.position, enemy.transform.position) < minDist){
                minDist = Vector3.Distance(transform.position, enemy.transform.position);
                closestTargetIndex = i;
                i++;
            }
        }
        if (allTargets.Length > 0){
            target = allTargets[closestTargetIndex].transform;
        }
    }
    
    void Attack(){
        if (!turretStatus.isOnCooldown){
            turretStatus.isOnCooldown = true;
            Invoke("resetAttackCooldown", turretCurrentStats.attackCD);
            
            GameObject projectile = null;
            if (turretStatus.level == 1){
                projectile = Instantiate(missiles.one) as GameObject;
            } else if (turretStatus.level == 2){
                projectile = Instantiate(missiles.two) as GameObject;
            } else if (turretStatus.level == 3){
                projectile = Instantiate(missiles.three) as GameObject;
            }
            fireSound.Play(0);
            anim.SetTrigger("Attack");
            projectile.transform.position = transform.position + new Vector3(0, 0.5f, 0) + (transform.forward * 0.5f);
            projectile.GetComponent<MissileController>().setTarget(target);
            projectile.GetComponent<MissileController>().setDamage(turretCurrentStats.attack);
        }
    }
    
    void resetAttackCooldown(){
        turretStatus.isOnCooldown = false;
    }
    
    public void levelUp(){
        if (turretStatus.level < 3){
            turretStatus.level++;
            turretCurrentStats.attack += turretBaseStats.upgradeAttackBonus;
            turretCurrentStats.attackCD -= turretBaseStats.upgradeAttackCDBonus;
            turretCurrentStats.attackRange += turretBaseStats.upgradeAttackRangeBonus;
        }
    }
    
    void updateSize(){
        if (turretStatus.level == 1){
            transform.localScale = turretBaseStats.level1Scale;
            maxLvlParticles.SetActive(false);
        } else if (turretStatus.level == 2){
            transform.localScale = turretBaseStats.level2Scale;
            maxLvlParticles.SetActive(false);
        } else if (turretStatus.level == 3){
            transform.localScale = turretBaseStats.level3Scale;
            maxLvlParticles.SetActive(true);
        } 
    }
    
    // whale only
    void updateFace(){
        if (hasFace){
            if (!turretStatus.isOnCooldown){
                face.renderer.materials = face.idle;
            } else {
                face.renderer.materials = face.attack;
            }
        }
    }
}
